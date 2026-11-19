// Human-created

using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper.managed
{
    public class CustomEAXFormulaCallbacks
    {
        public CustomEAXFormulas_InitialiseDelegate Initialise;
        public CustomEAXFormulas_CalculateDiffusionDelegate CalculateDiffusion;
        public CustomEAXFormulas_CalculateDensityDelegate CalculateDensity;
        public CustomEAXFormulas_CalculateReflectionsDelayDelegate CalculateReflectionsDelay;
        public CustomEAXFormulas_CalculateLateReverbDelayDelegate CalculateLateReverbDelay;
        public CustomEAXFormulas_CalculateFrequencyGainsDelegate CalculateFrequencyGains;
        public CustomEAXFormulas_CalculateReflectionsAndLateReverbGainDelegate CalculateReflectionsAndLateReverbGain;
        public CustomEAXFormulas_CalculateRT60Delegate CalculateRT60;
    }

    public unsafe class CustomEAXFormulaHelper
    {
        static float[] ToManaged(float* data, int length)
        {
            float[] result = new float[length];
            Marshal.Copy((IntPtr)data, result, 0, length);

            return result;
        }

        public static CustomEAXFormulaCallbacks CreateCustomEAXFormulaCallbacks(CustomEAXFormulas value)
        {
            var callbacks = new CustomEAXFormulaCallbacks();

            callbacks.Initialise = (formulas, processed, totalReturnedEnergyLF, totalReturnedEnergyHF, echogramGranularity, echogramLF, echogramHF, echogramAverage, echogramLength) =>
            {
                float[] lfArray = ToManaged(echogramLF, echogramLength);
                float[] hfArray = ToManaged(echogramHF, echogramLength);
                float[] avgArray = ToManaged(echogramAverage, echogramLength);

                float totalLF = 0f, totalHF = 0f;
                for (int i = 0; i < echogramLength; i++)
                {
                    totalLF += lfArray[i];
                    totalHF += hfArray[i];
                }

                value.Initialise(processed, totalLF, totalHF, echogramGranularity, lfArray, hfArray, avgArray);
            };

            callbacks.CalculateDiffusion = (formulas) => value.CalculateDiffusion();
            callbacks.CalculateDensity = (formulas) => value.CalculateDensity();
            callbacks.CalculateReflectionsDelay = (formulas, energyThreshold) => value.CalculateReflectionsDelay(energyThreshold);
            callbacks.CalculateLateReverbDelay = (formulas) => value.CalculateLateReverbDelay();

            callbacks.CalculateFrequencyGains = (formulas, referenceEnergy, outLFGain, outHFGain) =>
            {
                value.CalculateFrequencyGains(referenceEnergy, out float lfGain, out float hfGain);

                *outLFGain = lfGain;
                *outHFGain = hfGain;
            };

            callbacks.CalculateReflectionsAndLateReverbGain = (formulas, earlyLateTransitionMs, referenceEnergy, outReflectionsGain, outLateReverbGain) =>
            {
                value.CalculateReflectionsAndLateReverbGain(earlyLateTransitionMs, referenceEnergy, out float reflectionsGain, out float lateReverbGain);
                *outReflectionsGain = reflectionsGain;
                *outLateReverbGain = lateReverbGain;
            };

            callbacks.CalculateRT60 = (formulas, echogram, echogramLength) =>
            {
                float[] echogramArray = ToManaged(echogram, echogramLength);
                return value.CalculateRT60(echogramArray);
            };

            return callbacks;
        }
    }
}
