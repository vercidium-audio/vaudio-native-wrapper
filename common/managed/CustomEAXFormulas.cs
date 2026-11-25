// Human-reviewed

using System;
using System.Diagnostics;

namespace vaudionativewrapper.managed
{
    /// <summary>Class containing functions that can be overriden to manually calculate each EAX parameter</summary>
    public unsafe class CustomEAXFormulas
    {
        protected float[] tempDiffusionCumulative = new float[1];
        protected float[] echogramDb = new float[1];
        protected float binDurationMs;

        protected vaudionativewrapper.ProcessedReverb* processed;
        protected float totalReturnedEnergyLF;
        protected float totalReturnedEnergyHF;
        protected float totalReturnedEnergyAverage;
        protected float totalReturnedEnergy;
        protected float[] echogramLF;
        protected float[] echogramHF;
        protected float[] echogramAverage;

        protected float maxEnergy;

        /// <summary>Initialises the formula calculator with echogram data from a processed reverb result</summary>
        /// <param name="processed">The processed reverb results containing material and geometry data</param>
        /// <param name="totalReturnedEnergyLF">Total LF energy that returned to the emitter</param>
        /// <param name="totalReturnedEnergyHF">Total HF energy that returned to the emitter</param>
        /// <param name="echogramGranularity">The duration (in milliseconds) of each entry in the echogram arrays</param>
        /// <param name="echogramLF">Low-frequency echogram energy bins</param>
        /// <param name="echogramHF">High-frequency echogram energy bins</param>
        /// <param name="echogramAverage">Average echogram energy bins used for most calculations</param>
        public virtual void Initialise(vaudionativewrapper.ProcessedReverb* processed, float totalReturnedEnergyLF, float totalReturnedEnergyHF, float echogramGranularity, float[] echogramLF, float[] echogramHF, float[] echogramAverage)
        {
            Debug.Assert(echogramGranularity > 0);

            this.binDurationMs = echogramGranularity;
            this.processed = processed;
            this.totalReturnedEnergyLF = totalReturnedEnergyLF;
            this.totalReturnedEnergyHF = totalReturnedEnergyHF;
            this.totalReturnedEnergyAverage = (totalReturnedEnergyLF + totalReturnedEnergyHF) / 2;
            this.totalReturnedEnergy = (totalReturnedEnergyLF + totalReturnedEnergyHF);
            this.echogramLF = echogramLF;
            this.echogramAverage = echogramAverage;
            this.echogramHF = echogramHF;

            // Precalculate
            maxEnergy = 0f;

            foreach (var value in echogramAverage)
            {
                if (value > maxEnergy)
                    maxEnergy = value;
            }
        }

        /// <summary>Calculates the EAX diffusion parameter based on how smoothly energy accumulates over time</summary>
        public virtual float CalculateDiffusion()
        {
            if (tempDiffusionCumulative.Length < echogramAverage.Length)
                Array.Resize(ref tempDiffusionCumulative, echogramAverage.Length);

            // Fallback value
            if (totalReturnedEnergy <= 0f)
                return 0.5f;

            // Calculate how smoothly energy accumulates
            tempDiffusionCumulative[0] = echogramAverage[0];

            for (int i = 1; i < echogramAverage.Length; i++)
                tempDiffusionCumulative[i] = tempDiffusionCumulative[i - 1] + echogramAverage[i];

            // Measure deviation from ideal smooth buildup. Higher variance = less diffuse (more irregular). Lower variance = more diffuse (smoother)
            float variance = 0f;
            for (int i = 1; i < echogramAverage.Length; i++)
            {
                float expected = totalReturnedEnergy * (i / (float)echogramAverage.Length);
                float actual = tempDiffusionCumulative[i];
                float diff = actual - expected;
                variance += diff * diff;
            }

            variance /= echogramAverage.Length;
            float normalizedVariance = variance / (totalReturnedEnergy * totalReturnedEnergy);

            // Convert variance to diffusion (inverse relationship)
            // Low variance = high diffusion
            float baseDiffusion = 1.0f - Math.Min(normalizedVariance * 10f, 1.0f);

            // Material roughness directly affects diffusion
            float roughnessBonus = processed->materialRoughness * 0.3f;

            // Outdoor spaces have lower diffusion (sound escapes)
            float outdoorPenalty = processed->outsidePercent * 0.25f;

            float diffusion = baseDiffusion + roughnessBonus - outdoorPenalty;
            return diffusion;
        }

        /// <summary>Calculates the EAX density parameter based on the number of energy peaks in the early echogram</summary>
        public virtual float CalculateDensity()
        {
            // Count significant energy peaks in the first 150ms
            int analysisLength = Math.Clamp((int)(150f / binDurationMs), 1, echogramAverage.Length);

            // Fallback
            if (maxEnergy <= 0f)
                return 0.5f;

            // Count the number of peaks above a threshold
            float threshold = maxEnergy * 0.1f;
            int peakCount = 0;

            for (int i = 1; i < analysisLength - 1; i++)
            {
                if (echogramAverage[i] > threshold &&
                    echogramAverage[i] > echogramAverage[i - 1] &&
                    echogramAverage[i] > echogramAverage[i + 1])
                {
                    peakCount++;
                }
            }

            // Normalize peak count to density value. More peaks per unit time = higher density. Typical range is 10-100 peaks per second
            float peaksPerSecond = peakCount / (analysisLength * binDurationMs / 1000f);

            // Map to 0.0-1.0 range
            float baseDensity = MathF.Max(0, peaksPerSecond - 10f) / 90f;

            // Calculat eextra bonuses/penalties
            float returnedBonus = processed->returnedPercent * 0.3f;
            float outdoorPenalty = processed->outsidePercent * 0.3f;

            float density = baseDensity + returnedBonus - outdoorPenalty;
            return density;
        }

        /// <summary>Calculates the delay in seconds before the first significant reflection is detected</summary>
        public virtual float CalculateReflectionsDelay(float energyThreshold)
        {
            if (maxEnergy <= 0f)
                return 0.0f;

            // Find the first bin with significant energy
            float threshold = maxEnergy * energyThreshold;

            for (int i = 0; i < echogramAverage.Length; i++)
            {
                if (echogramAverage[i] >= threshold)
                {
                    float delayMs = i * binDurationMs;
                    float delaySeconds = delayMs / 1000f;

                    return delaySeconds;
                }
            }

            // No reflections found
            return 0.0f;
        }

        /// <summary>Calculates the delay in seconds before late reverb begins, based on the 25% energy accumulation point</summary>
        public virtual float CalculateLateReverbDelay()
        {
            if (totalReturnedEnergy <= 0f)
                return 0f;

            // Find where we've accumulated 25% of total energy
            // This marks the transition from sparse early reflections to dense late reverb
            float targetEnergy = totalReturnedEnergy * 0.25f;
            float accumulatedEnergy = 0f;
            int transitionBin = 0;

            for (int i = 0; i < echogramAverage.Length; i++)
            {
                accumulatedEnergy += echogramAverage[i];
                if (accumulatedEnergy >= targetEnergy)
                {
                    transitionBin = i;
                    break;
                }
            }

            // Convert to seconds
            float delayMs = transitionBin * binDurationMs;

            // Material roughness delays the transition to late reverb
            float roughnessDelay = processed->materialRoughness * 15f; // Add up to 15ms for fully rough surfaces

            delayMs += roughnessDelay;
            delayMs = MathF.Max(0f, delayMs);

            float delaySeconds = delayMs / 1000f;
            return delaySeconds;
        }

        /// <summary>Calculates the low and high frequency gain values relative to a reference energy level</summary>
        public virtual void CalculateFrequencyGains(float referenceEnergy, out float lfGain, out float hfGain)
        {
            if (totalReturnedEnergy <= 0 || referenceEnergy <= 0)
            {
                lfGain = 0.1f;  // Low default for outdoor/empty
                hfGain = 0.1f;
                return;
            }

            // Scale by reference energy
            float energyScale = MathF.Sqrt(totalReturnedEnergyAverage / referenceEnergy);

            // Calculate relative frequency balance
            float lfRatio = totalReturnedEnergyLF / totalReturnedEnergy;
            float hfRatio = totalReturnedEnergyHF / totalReturnedEnergy;

            // Should add up to 1
            Debug.Assert(MathF.Abs(lfRatio + hfRatio - 1) < 0.001f);

            // Combine absolute level with relative balance
            lfGain = lfRatio * energyScale;
            hfGain = hfRatio * energyScale;
        }

        /// <summary>Calculates the gain for early reflections and late reverb by splitting the echogram at a transition point</summary>
        public virtual void CalculateReflectionsAndLateReverbGain(float earlyLateTransitionMs, float referenceEnergy, out float reflectionsGain, out float lateReverbGain)
        {
            GetEarlyAndLateEnergy(earlyLateTransitionMs, out float earlyEnergyLF, out float earlyEnergyHF, out float lateEnergyLF, out float lateEnergyHF);

            // Average both frequency bands
            float earlyEnergy = (earlyEnergyLF + earlyEnergyHF) * 0.5f;
            float lateEnergy = (lateEnergyLF + lateEnergyHF) * 0.5f;

            if (referenceEnergy <= 0f)
            {
                reflectionsGain = 0.05f;
                lateReverbGain = 0.1f;
                return;
            }

            // Calculate gains relative to reference energy
            reflectionsGain = MathF.Sqrt(earlyEnergy / referenceEnergy);
            lateReverbGain = MathF.Sqrt(lateEnergy / referenceEnergy);
        }

        // The HF and LF amplitudes of the returned energy, relative to the louder of the two. 1 when nothing returned
        void GetRelativeBandGains(out float lf, out float hf)
        {
            float loudest = MathF.Max(totalReturnedEnergyLF, totalReturnedEnergyHF);

            if (loudest <= 0f)
            {
                lf = 1f;
                hf = 1f;
                return;
            }

            lf = MathF.Sqrt(totalReturnedEnergyLF / loudest);
            hf = MathF.Sqrt(totalReturnedEnergyHF / loudest);
        }

        /// <summary>Calculates the cutoff frequency of a low-pass filter that attenuates the reverb at <paramref name="hfReference"/> by as much as its high frequencies were absorbed, relative to its low frequencies</summary>
        /// <param name="hfReference">The reference frequency of the high-frequency band, in Hz</param>
        /// <returns>Cutoff frequency in Hz, or 20000 if the high frequencies aren't quieter</returns>
        public virtual float CalculateHighCut(float hfReference)
        {
            GetRelativeBandGains(out _, out float gain);

            if (gain >= 1f)
                return HIGH_CUT_MAX;

            // Fitted to FMOD's SFX reverb presets, which match their EFX equivalents' GainHF at 5kHz to within 1.3%
            return hfReference * MathF.Sqrt(gain / (1f - gain));
        }

        /// <summary>Calculates the gain of a low-shelf filter, for how much more the reverb's low frequencies were absorbed than its high frequencies</summary>
        /// <returns>Gain in decibels, 0 if the low frequencies aren't quieter</returns>
        public virtual float CalculateLowShelfGain()
        {
            GetRelativeBandGains(out float gain, out _);
            return GainToDecibels(gain);
        }

        /// <summary>Calculates the level of the reverb signal, from the louder of its early reflections and late reverb in the louder frequency band, so <see cref="CalculateHighCut"/> and <see cref="CalculateLowShelfGain"/> only ever cut from it</summary>
        /// <param name="earlyLateTransitionMs">Time in milliseconds marking the boundary between early and late energy</param>
        /// <param name="referenceEnergy">Reference energy used to normalise the level</param>
        /// <param name="gain">Overall linear gain of the reverb, i.e. EAXReverb.Gain</param>
        /// <returns>Level in decibels</returns>
        public virtual float CalculateWetLevel(float earlyLateTransitionMs, float referenceEnergy, float gain)
        {
            // Same fallback level as CalculateReflectionsAndLateReverbGain's late reverb
            if (referenceEnergy <= 0f)
                return GainToDecibels(gain * 0.1f);

            GetLouderBandEarlyAndLateEnergy(earlyLateTransitionMs, out float early, out float late);
            float energy = MathF.Max(early, late);

            if (energy <= 0f)
                return WET_LEVEL_MIN;

            return GainToDecibels(gain * MathF.Sqrt(energy / referenceEnergy));
        }

        /// <summary>Calculates the late reverb amplitude's share of the early reflections + late reverb amplitudes. 0 is only early reflections, 100 is only late reverb</summary>
        /// <param name="earlyLateTransitionMs">Time in milliseconds marking the boundary between early and late energy</param>
        /// <returns>Mix in percent, or 50 if nothing returned</returns>
        public virtual float CalculateEarlyLateMix(float earlyLateTransitionMs)
        {
            GetLouderBandEarlyAndLateEnergy(earlyLateTransitionMs, out float early, out float late);

            float earlyGain = MathF.Sqrt(early);
            float lateGain = MathF.Sqrt(late);
            float total = earlyGain + lateGain;

            if (total <= 0f)
                return 50f;

            return lateGain / total * 100f;
        }

        const float HIGH_CUT_MAX = 20000f;
        const float WET_LEVEL_MIN = -80f;

        // The early and late energy of whichever band returned more energy, which WetLevel and EarlyLateMix describe so HighCut and LowShelfGain only cut from it
        void GetLouderBandEarlyAndLateEnergy(float earlyLateTransitionMs, out float early, out float late)
        {
            GetEarlyAndLateEnergy(earlyLateTransitionMs, out float earlyEnergyLF, out float earlyEnergyHF, out float lateEnergyLF, out float lateEnergyHF);

            bool lfLouder = earlyEnergyLF + lateEnergyLF >= earlyEnergyHF + lateEnergyHF;
            early = lfLouder ? earlyEnergyLF : earlyEnergyHF;
            late = lfLouder ? lateEnergyLF : lateEnergyHF;
        }

        /// <summary>Sums each band's echogram energy before and after <paramref name="earlyLateTransitionMs"/></summary>
        protected void GetEarlyAndLateEnergy(float earlyLateTransitionMs, out float earlyEnergyLF, out float earlyEnergyHF, out float lateEnergyLF, out float lateEnergyHF)
        {
            int transitionBin = (int)(earlyLateTransitionMs / binDurationMs);

            earlyEnergyLF = 0f;
            earlyEnergyHF = 0f;

            for (int i = 0; i < Math.Min(transitionBin, echogramLF.Length); i++)
            {
                earlyEnergyLF += echogramLF[i];
                earlyEnergyHF += echogramHF[i];
            }

            lateEnergyLF = 0f;
            lateEnergyHF = 0f;

            for (int i = transitionBin; i < echogramLF.Length; i++)
            {
                lateEnergyLF += echogramLF[i];
                lateEnergyHF += echogramHF[i];
            }
        }

        /// <summary>Converts a linear amplitude gain to decibels, flooring silence at -80dB</summary>
        protected static float GainToDecibels(float gain)
        {
            const float minGain = 0.0001f; // -80dB
            return 20f * MathF.Log10(MathF.Max(gain, minGain));
        }

        /// <summary>Calculates the RT60 reverberation time in seconds for a given echogram using the Schroeder backward-integrated decay curve</summary>
        public virtual float CalculateRT60(float[] echogram)
        {
            if (echogramDb.Length < echogram.Length)
                Array.Resize(ref echogramDb, echogram.Length);

            // Find the global peak
            int globalPeakIndex = 0;
            for (int i = 1; i < echogram.Length; i++)
            {
                if (echogram[i] > echogram[globalPeakIndex])
                    globalPeakIndex = i;
            }

            if (echogram[globalPeakIndex] <= 0f)
                return 0f;

            // Truncate at the first zero-energy bin after the global peak — stray late bins beyond a gap are noise
            // and must not be used as a peak or as decay data
            int echogramLength = echogram.Length;
            for (int i = globalPeakIndex + 1; i < echogram.Length; i++)
            {
                if (echogram[i] <= 0f)
                {
                    echogramLength = i;
                    break;
                }
            }

            int maxSkipBins = Math.Max(1, (int)(echogram.Length * 0.15f));

            for (int skipBins = 0; skipBins < maxSkipBins; skipBins++)
            {
                int searchStart = globalPeakIndex + skipBins;
                if (searchStart >= echogramLength)
                    break;

                // Find the peak energy point within the contiguous decay, ignoring the first skipBins bins after the global peak
                int peakIndex = searchStart;
                for (int i = searchStart + 1; i < echogramLength; i++)
                {
                    if (echogram[i] > echogram[peakIndex])
                        peakIndex = i;
                }

                // Schroeder backward integration: the energy remaining from each bin onwards, in dB relative to the total from the peak.
                // Unlike raw bin levels this is monotonic, and it falls away when the energy runs out rather than sitting on a plateau
                float remaining = 0f;
                for (int i = echogramLength - 1; i >= peakIndex; i--)
                {
                    remaining += echogram[i];
                    echogramDb[i] = remaining;
                }

                for (int i = peakIndex; i < echogramLength; i++)
                    echogramDb[i] = 10f * MathF.Log10(echogramDb[i] / remaining);

                // Try T20 first, then T30, then T60
                float rt60 = TryCalculateDecayTime(echogramDb, peakIndex, 0f, binDurationMs, echogramLength, 20f, 3f);

                if (rt60 <= 0f)
                {
                    rt60 = TryCalculateDecayTime(echogramDb, peakIndex, 0f, binDurationMs, echogramLength, 30f, 2f);
                }

                if (rt60 <= 0f)
                {
                    rt60 = TryCalculateDecayTime(echogramDb, peakIndex, 0f, binDurationMs, echogramLength, 60f, 1f);
                }

                if (rt60 > 0f)
                    return rt60;
            }

            return 0f;
        }

        /// <summary>Attempts to estimate RT60 from the time the decay curve takes to fall from -5 dB to <paramref name="targetDecayDb"/> below the peak</summary>
        /// <param name="echogramDb">Schroeder decay curve in decibels, valid from <paramref name="peakIndex"/> up to <paramref name="echogramLength"/></param>
        /// <param name="peakIndex">Index of the peak energy bin</param>
        /// <param name="peakDb">Decay curve level at the peak in dB</param>
        /// <param name="binDurationMs">Duration of each echogram bin in milliseconds</param>
        /// <param name="echogramLength">The number of valid bins. The energy is treated as having run out after this bin</param>
        /// <param name="targetDecayDb">The dB drop range to measure (e.g. 20, 30, or 60)</param>
        /// <param name="scaleFactor">Multiplier applied when extrapolating to full RT60</param>
        /// <returns>Estimated RT60 in seconds, or 0 if the measured span is too short or the curve never decays</returns>
        protected virtual float TryCalculateDecayTime(float[] echogramDb, int peakIndex, float peakDb, float binDurationMs, int echogramLength, float targetDecayDb, float scaleFactor)
        {
            float startDb = peakDb - 5f;
            float endDb = peakDb - targetDecayDb;

            float startMs = FindDecayCrossingMs(echogramDb, peakIndex, binDurationMs, echogramLength, startDb);
            float endMs = FindDecayCrossingMs(echogramDb, peakIndex, binDurationMs, echogramLength, endDb);

            // Too short to measure reliably
            const float minSpanMs = 100f;
            if (endMs - startMs < minSpanMs)
                return 0f;

            // Extrapolate the measured drop to a 60dB decay
            float rt60Ms = (endMs - startMs) * 60f / (targetDecayDb - 5f);
            return rt60Ms / 1000f; // Convert to seconds
        }

        /// <summary>Finds the time (relative to the peak) at which the decay curve first falls below <paramref name="levelDb"/>, interpolating between bins</summary>
        protected static float FindDecayCrossingMs(float[] echogramDb, int peakIndex, float binDurationMs, int echogramLength, float levelDb)
        {
            for (int i = peakIndex; i < echogramLength; i++)
            {
                float a = echogramDb[i];
                float timeMs = (i - peakIndex) * binDurationMs;

                // The energy runs out after the last bin, so the curve drops through every level by the end of it
                if (i + 1 >= echogramLength)
                    return timeMs + binDurationMs;

                float b = echogramDb[i + 1];
                if (b < levelDb)
                    return timeMs + binDurationMs * (a - levelDb) / (a - b);
            }

            return 0f;
        }
    }
}
