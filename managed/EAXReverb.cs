using System;

namespace vaudionativewrapper.managed
{
    public unsafe class EAXReverb
    {
        public vaudionativewrapper.EAXReverb* native;

        public EAXReverb(vaudionativewrapper.EAXReverb* native)
        {
            this.native = native;
        }

        public float AirAbsorptionGainHF => native->airAbsorptionGainHF;
        public float DecayHFRatio => native->decayHFRatio;
        public float DecayLFRatio => native->decayLFRatio;
        public float DecayTime => native->decayTime;
        public float Density => native->density;
        public float Diffusion => native->diffusion;
        public float EchoDepth => native->echoDepth;
        public float EchoTime => native->echoTime;
        public float Gain => native->gain;
        public float GainHF => native->gainHF;
        public float GainLF => native->gainLF;
        public float HFReference => native->hfReference;
        public float LateReverbDelay => native->lateReverbDelay;
        public float LateReverbGain => native->lateReverbGain;
        public float LFReference => native->lfReference;
        public float ModulationDepth => native->modulationDepth;
        public float ModulationTime => native->modulationTime;
        public float ReflectionsDelay => native->reflectionsDelay;
        public float ReflectionsGain => native->reflectionsGain;
        public float RoomRolloffFactor => native->roomRolloffFactor;
        public int DecayHFLimit => native->decayHFLimit;

        public Vector? GetRelativeDirection(Emitter emitter)
        {
            var ptr = EAXReverbResultsBindings.GetRelativeDirection((IntPtr)native, emitter.native);

            if (ptr != null)
                return *ptr;

            return null;
        }

        public float? GetRelativeGain(Emitter emitter)
        {
            var ptr = EAXReverbResultsBindings.GetRelativeGain((IntPtr)native, emitter.native);

            if (ptr != null)
                return *ptr;

            return null;
        }

        /// <summary>
        /// Computes a similarity score between this reverb preset and another, in the range [0, 1] where 1 means identical.
        /// </summary>
        public float GetSimilarity(EAXReverb other) => EAXUtilsBindings.GetSimilarity(native, other.native);

        /// <summary>
        /// Gets the number of seconds this reverb's tail remains audible after the emitter stops emitting, used to delay removal from the world.
        /// maxVolume is the loudest linear volume (0-1) the emitter's dry source is ever played at (see Emitter.GetMaxVolume).
        /// </summary>
        public float GetEffectiveTailSeconds(float maxVolume) => EAXUtilsBindings.GetEffectiveTailSeconds(native, maxVolume);

        /// <summary>
        /// Finds the candidate most similar to target.
        /// Returns the best match, or null if target or candidates is null/empty.
        /// </summary>
        public static EAXReverb FindBestMatch(EAXReverb target, EAXReverb[] candidates)
        {
            if (target == null)
                throw new InvalidArgumentException("target cannot be null");

            if (candidates == null)
                throw new InvalidArgumentException("candidates cannot be null");

            if (candidates.Length == 0)
                throw new InvalidArgumentException("candidates cannot be empty");

            var nativeCandidates = stackalloc vaudionativewrapper.EAXReverb*[candidates.Length];

            for (int i = 0; i < candidates.Length; i++)
                nativeCandidates[i] = candidates[i].native;

            vaudionativewrapper.EAXReverb* outBest;
            int index = EAXUtilsBindings.FindBestMatch(target.native, nativeCandidates, candidates.Length, &outBest);

            if (index < 0 || outBest == null)
                return null;

            return candidates[index];
        }
    }
}