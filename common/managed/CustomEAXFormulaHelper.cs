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
        public CustomEAXFormulas_CalculateHighCutDelegate CalculateHighCut;
        public CustomEAXFormulas_CalculateLowShelfGainDelegate CalculateLowShelfGain;
        public CustomEAXFormulas_CalculateWetLevelDelegate CalculateWetLevel;
        public CustomEAXFormulas_CalculateEarlyLateMixDelegate CalculateEarlyLateMix;
    }

    public unsafe class CustomEAXFormulaHelper
    {
        // Keyed by the native VACustomEAXFormulas* that native passes to every formula
        static readonly CallbackTable<CustomEAXFormulas> formulasTable = new CallbackTable<CustomEAXFormulas>();

        // The same static delegates are shared by every world, and live forever so native can always call them
        static readonly CustomEAXFormulaCallbacks callbacks = new CustomEAXFormulaCallbacks
        {
            Initialise = InitialiseNative,
            CalculateDiffusion = CalculateDiffusionNative,
            CalculateDensity = CalculateDensityNative,
            CalculateReflectionsDelay = CalculateReflectionsDelayNative,
            CalculateLateReverbDelay = CalculateLateReverbDelayNative,
            CalculateFrequencyGains = CalculateFrequencyGainsNative,
            CalculateReflectionsAndLateReverbGain = CalculateReflectionsAndLateReverbGainNative,
            CalculateRT60 = CalculateRT60Native,
            CalculateHighCut = CalculateHighCutNative,
            CalculateLowShelfGain = CalculateLowShelfGainNative,
            CalculateWetLevel = CalculateWetLevelNative,
            CalculateEarlyLateMix = CalculateEarlyLateMixNative,
        };

        static float[] ToManaged(float* data, int length)
        {
            float[] result = new float[length];
            Marshal.Copy((IntPtr)data, result, 0, length);

            return result;
        }

        static CustomEAXFormulas Get(vaudionativewrapper.CustomEAXFormulas* formulas) => formulasTable.Get((IntPtr)formulas);

        // Points the native formulas at the static trampolines, which invoke value
        internal static CustomEAXFormulaCallbacks Register(vaudionativewrapper.CustomEAXFormulas* nativeFormulas, CustomEAXFormulas value)
        {
            formulasTable.Set((IntPtr)nativeFormulas, value);

            nativeFormulas->initialise = Marshal.GetFunctionPointerForDelegate(callbacks.Initialise);
            nativeFormulas->calculateDiffusion = Marshal.GetFunctionPointerForDelegate(callbacks.CalculateDiffusion);
            nativeFormulas->calculateDensity = Marshal.GetFunctionPointerForDelegate(callbacks.CalculateDensity);
            nativeFormulas->calculateReflectionsDelay = Marshal.GetFunctionPointerForDelegate(callbacks.CalculateReflectionsDelay);
            nativeFormulas->calculateLateReverbDelay = Marshal.GetFunctionPointerForDelegate(callbacks.CalculateLateReverbDelay);
            nativeFormulas->calculateFrequencyGains = Marshal.GetFunctionPointerForDelegate(callbacks.CalculateFrequencyGains);
            nativeFormulas->calculateReflectionsAndLateReverbGain = Marshal.GetFunctionPointerForDelegate(callbacks.CalculateReflectionsAndLateReverbGain);
            nativeFormulas->calculateRT60 = Marshal.GetFunctionPointerForDelegate(callbacks.CalculateRT60);
            nativeFormulas->calculateHighCut = Marshal.GetFunctionPointerForDelegate(callbacks.CalculateHighCut);
            nativeFormulas->calculateLowShelfGain = Marshal.GetFunctionPointerForDelegate(callbacks.CalculateLowShelfGain);
            nativeFormulas->calculateWetLevel = Marshal.GetFunctionPointerForDelegate(callbacks.CalculateWetLevel);
            nativeFormulas->calculateEarlyLateMix = Marshal.GetFunctionPointerForDelegate(callbacks.CalculateEarlyLateMix);

            return callbacks;
        }

        // Call once native has destroyed the formulas
        internal static void Unregister(IntPtr nativeFormulas) => formulasTable.Remove(nativeFormulas);

        [AOT.MonoPInvokeCallback(typeof(CustomEAXFormulas_InitialiseDelegate))]
        static void InitialiseNative(vaudionativewrapper.CustomEAXFormulas* formulas, vaudionativewrapper.ProcessedReverb* processed, float totalReturnedEnergyLF, float totalReturnedEnergyHF, float echogramGranularity, float* echogramLF, float* echogramHF, float* echogramAverage, int echogramLength)
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

            Get(formulas).Initialise(processed, totalLF, totalHF, echogramGranularity, lfArray, hfArray, avgArray);
        }

        [AOT.MonoPInvokeCallback(typeof(CustomEAXFormulas_CalculateDiffusionDelegate))]
        static float CalculateDiffusionNative(vaudionativewrapper.CustomEAXFormulas* formulas) => Get(formulas).CalculateDiffusion();

        [AOT.MonoPInvokeCallback(typeof(CustomEAXFormulas_CalculateDensityDelegate))]
        static float CalculateDensityNative(vaudionativewrapper.CustomEAXFormulas* formulas) => Get(formulas).CalculateDensity();

        [AOT.MonoPInvokeCallback(typeof(CustomEAXFormulas_CalculateReflectionsDelayDelegate))]
        static float CalculateReflectionsDelayNative(vaudionativewrapper.CustomEAXFormulas* formulas, float energyThreshold) => Get(formulas).CalculateReflectionsDelay(energyThreshold);

        [AOT.MonoPInvokeCallback(typeof(CustomEAXFormulas_CalculateLateReverbDelayDelegate))]
        static float CalculateLateReverbDelayNative(vaudionativewrapper.CustomEAXFormulas* formulas) => Get(formulas).CalculateLateReverbDelay();

        [AOT.MonoPInvokeCallback(typeof(CustomEAXFormulas_CalculateFrequencyGainsDelegate))]
        static void CalculateFrequencyGainsNative(vaudionativewrapper.CustomEAXFormulas* formulas, float referenceEnergy, float* outLFGain, float* outHFGain)
        {
            Get(formulas).CalculateFrequencyGains(referenceEnergy, out float lfGain, out float hfGain);

            *outLFGain = lfGain;
            *outHFGain = hfGain;
        }

        [AOT.MonoPInvokeCallback(typeof(CustomEAXFormulas_CalculateReflectionsAndLateReverbGainDelegate))]
        static void CalculateReflectionsAndLateReverbGainNative(vaudionativewrapper.CustomEAXFormulas* formulas, float earlyLateTransitionMs, float referenceEnergy, float* outReflectionsGain, float* outLateReverbGain)
        {
            Get(formulas).CalculateReflectionsAndLateReverbGain(earlyLateTransitionMs, referenceEnergy, out float reflectionsGain, out float lateReverbGain);

            *outReflectionsGain = reflectionsGain;
            *outLateReverbGain = lateReverbGain;
        }

        [AOT.MonoPInvokeCallback(typeof(CustomEAXFormulas_CalculateRT60Delegate))]
        static float CalculateRT60Native(vaudionativewrapper.CustomEAXFormulas* formulas, float* echogram, int echogramLength) => Get(formulas).CalculateRT60(ToManaged(echogram, echogramLength));

        [AOT.MonoPInvokeCallback(typeof(CustomEAXFormulas_CalculateHighCutDelegate))]
        static float CalculateHighCutNative(vaudionativewrapper.CustomEAXFormulas* formulas, float hfReference) => Get(formulas).CalculateHighCut(hfReference);

        [AOT.MonoPInvokeCallback(typeof(CustomEAXFormulas_CalculateLowShelfGainDelegate))]
        static float CalculateLowShelfGainNative(vaudionativewrapper.CustomEAXFormulas* formulas) => Get(formulas).CalculateLowShelfGain();

        [AOT.MonoPInvokeCallback(typeof(CustomEAXFormulas_CalculateWetLevelDelegate))]
        static float CalculateWetLevelNative(vaudionativewrapper.CustomEAXFormulas* formulas, float earlyLateTransitionMs, float referenceEnergy, float gain) => Get(formulas).CalculateWetLevel(earlyLateTransitionMs, referenceEnergy, gain);

        [AOT.MonoPInvokeCallback(typeof(CustomEAXFormulas_CalculateEarlyLateMixDelegate))]
        static float CalculateEarlyLateMixNative(vaudionativewrapper.CustomEAXFormulas* formulas, float earlyLateTransitionMs) => Get(formulas).CalculateEarlyLateMix(earlyLateTransitionMs);
    }
}
