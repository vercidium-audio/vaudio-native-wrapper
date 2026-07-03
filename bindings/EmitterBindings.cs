using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void OnRaytracingCompleteFn();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void OnRaytracedByAnotherEmitterFn(IntPtr source, IntPtr target);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void VisualisationCallbackFn(VisualisationData* data, int count);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate float GainFormulaDelegate(bool lowFrequency, int occlusionRayCount, int permeationRayCount, int permeationBounceCount, float occlusionEnergy, float permeationEnergy);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void LogCallbackFn([MarshalAs(UnmanagedType.LPStr)] string message);

    public static class EmitterBindings
    {
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterCreate")]
        public static extern IntPtr Create();

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterDestroy")]
        public static extern VAResult Destroy(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterAddTarget")]
        public static extern VAResult AddTarget(IntPtr emitter, IntPtr target);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterRemoveTarget")]
        public static extern void RemoveTarget(IntPtr emitter, IntPtr target);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterHasTarget")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool HasTarget(IntPtr emitter, IntPtr target);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterHasRaytracedTarget")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool HasRaytracedTarget(IntPtr emitter, IntPtr target);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetTargetFilter")]
        public static extern unsafe LowPassFilter* GetTargetFilter(IntPtr emitter, IntPtr target);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterResetTrails")]
        public static extern void ResetTrails(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetReverbRayCount")]
        public static extern int GetReverbRayCount(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetReverbRayCount")]
        public static extern void SetReverbRayCount(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetReverbBounceCount")]
        public static extern int GetReverbBounceCount(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetReverbBounceCount")]
        public static extern void SetReverbBounceCount(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetOcclusionRayCount")]
        public static extern int GetOcclusionRayCount(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetOcclusionRayCount")]
        public static extern void SetOcclusionRayCount(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetOcclusionBounceCount")]
        public static extern int GetOcclusionBounceCount(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetOcclusionBounceCount")]
        public static extern void SetOcclusionBounceCount(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetPermeationRayCount")]
        public static extern int GetPermeationRayCount(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetPermeationRayCount")]
        public static extern void SetPermeationRayCount(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetPermeationBounceCount")]
        public static extern int GetPermeationBounceCount(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetPermeationBounceCount")]
        public static extern void SetPermeationBounceCount(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetAmbientOcclusionRayCount")]
        public static extern int GetAmbientOcclusionRayCount(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetAmbientOcclusionRayCount")]
        public static extern void SetAmbientOcclusionRayCount(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetAmbientOcclusionBounceCount")]
        public static extern int GetAmbientOcclusionBounceCount(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetAmbientOcclusionBounceCount")]
        public static extern void SetAmbientOcclusionBounceCount(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetAmbientPermeationRayCount")]
        public static extern int GetAmbientPermeationRayCount(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetAmbientPermeationRayCount")]
        public static extern void SetAmbientPermeationRayCount(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetAmbientPermeationBounceCount")]
        public static extern int GetAmbientPermeationBounceCount(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetAmbientPermeationBounceCount")]
        public static extern void SetAmbientPermeationBounceCount(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetVisualisationRayCount")]
        public static extern int GetVisualisationRayCount(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetVisualisationRayCount")]
        public static extern void SetVisualisationRayCount(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetVisualisationBounceCount")]
        public static extern int GetVisualisationBounceCount(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetVisualisationBounceCount")]
        public static extern void SetVisualisationBounceCount(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetVisualisationUpdateFrequency")]
        public static extern int GetVisualisationUpdateFrequency(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetVisualisationUpdateFrequency")]
        public static extern void SetVisualisationUpdateFrequency(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetTrailCount")]
        public static extern int GetTrailCount(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetTrailBounceCount")]
        public static extern int GetTrailBounceCount(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetMaxEchogramTime")]
        public static extern int GetMaxEchogramTime(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetMaxEchogramTime")]
        public static extern void SetMaxEchogramTime(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetEchogramGranularity")]
        public static extern int GetEchogramGranularity(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetEchogramGranularity")]
        public static extern void SetEchogramGranularity(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetRefreshRayCount")]
        public static extern int GetRefreshRayCount(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetRefreshRayCount")]
        public static extern void SetRefreshRayCount(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetRefreshDistanceThreshold")]
        public static extern float GetRefreshDistanceThreshold(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetRefreshDistanceThreshold")]
        public static extern void SetRefreshDistanceThreshold(IntPtr emitter, float value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetReverbEnergyCap")]
        public static extern float GetReverbEnergyCap(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetReverbEnergyCap")]
        public static extern VAResult SetReverbEnergyCap(IntPtr emitter, float value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetOcclusionEnergyCap")]
        public static extern float GetOcclusionEnergyCap(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetOcclusionEnergyCap")]
        public static extern VAResult SetOcclusionEnergyCap(IntPtr emitter, float value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetPermeationEnergyCap")]
        public static extern float GetPermeationEnergyCap(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetPermeationEnergyCap")]
        public static extern VAResult SetPermeationEnergyCap(IntPtr emitter, float value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetAmbientOcclusionEnergyCap")]
        public static extern float GetAmbientOcclusionEnergyCap(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetAmbientOcclusionEnergyCap")]
        public static extern VAResult SetAmbientOcclusionEnergyCap(IntPtr emitter, float value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetAmbientPermeationEnergyCap")]
        public static extern float GetAmbientPermeationEnergyCap(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetAmbientPermeationEnergyCap")]
        public static extern VAResult SetAmbientPermeationEnergyCap(IntPtr emitter, float value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetReservedEmitterCount")]
        public static extern int GetReservedEmitterCount(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetReservedEmitterCount")]
        public static extern void SetReservedEmitterCount(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetMinimumPermeationEnergy")]
        public static extern float GetMinimumPermeationEnergy(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetMinimumPermeationEnergy")]
        public static extern void SetMinimumPermeationEnergy(IntPtr emitter, float value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterReverbEnabled")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool ReverbEnabled(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterOcclusionEnabled")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool OcclusionEnabled(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterPermeationEnabled")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool PermeationEnabled(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterAmbientOcclusionEnabled")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool AmbientOcclusionEnabled(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterAmbientPermeationEnabled")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool AmbientPermeationEnabled(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterVisualisationEnabled")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool VisualisationEnabled(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterCastsRays")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool CastsRays(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterWithinWorldBounds")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool WithinWorldBounds(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetOverridePositions")]
        public static extern unsafe int SetOverridePositions(IntPtr emitter, Vector* positions, int count);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetOverrideRayDirections")]
        public static extern unsafe int SetOverrideRayDirections(IntPtr emitter, Vector* directions, int count);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetGainFormula")]
        public static extern VAResult SetGainFormula(IntPtr emitter, IntPtr formula);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetAmbientGainFormula")]
        public static extern VAResult SetAmbientGainFormula(IntPtr emitter, IntPtr formula);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetLogCallback")]
        public static extern void SetLogCallback(IntPtr emitter, LogCallbackFn callback);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetLogErrorCallback")]
        public static extern void SetLogErrorCallback(IntPtr emitter, LogCallbackFn callback);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetOnRaytracingCompleteCallback")]
        public static extern void SetOnRaytracingCompleteCallback(IntPtr emitter, OnRaytracingCompleteFn callback);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetOnRaytracedByAnotherEmitterCallback")]
        public static extern void SetOnRaytracedByAnotherEmitterCallback(IntPtr emitter, OnRaytracedByAnotherEmitterFn callback);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetVisualisationCallback")]
        public static extern void SetVisualisationCallback(IntPtr emitter, VisualisationCallbackFn callback);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetPosition")]
        public static extern void SetPosition(IntPtr emitter, Vector position);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetPosition")]
        public static extern Vector GetPosition(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetInitialising")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool GetInitialising(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetAffectsGroupedEax")]
        public static extern void SetAffectsGroupedEax(IntPtr emitter, bool value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetAffectsGroupedEax")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool GetAffectsGroupedEax(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetGroupedEaxIndex")]
        public static extern int GetGroupedEaxIndex(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetOutsidePercent")]
        public static extern float GetOutsidePercent(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetHasRelativeReverb")]
        public static extern void SetHasRelativeReverb(IntPtr emitter, bool value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetHasRelativeReverb")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool GetHasRelativeReverb(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetRelativeReverbInnerThreshold")]
        public static extern void SetRelativeReverbInnerThreshold(IntPtr emitter, float value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetRelativeReverbInnerThreshold")]
        public static extern float GetRelativeReverbInnerThreshold(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetRelativeReverbOuterThreshold")]
        public static extern void SetRelativeReverbOuterThreshold(IntPtr emitter, float value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetRelativeReverbOuterThreshold")]
        public static extern float GetRelativeReverbOuterThreshold(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetClampPosition")]
        public static extern void SetClampPosition(IntPtr emitter, bool value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetClampPosition")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool GetClampPosition(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetScatteringSeed")]
        public static extern void SetScatteringSeed(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetScatteringSeed")]
        public static extern int GetScatteringSeed(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetName")]
        public static extern void SetName(IntPtr emitter, [MarshalAs(UnmanagedType.LPStr)] string name);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetName")]
        private static extern IntPtr GetNameRaw(IntPtr emitter);
        public static string GetName(IntPtr emitter) => Marshal.PtrToStringAnsi(GetNameRaw(emitter));

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterSetType")]
        public static extern void SetType(IntPtr emitter, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetType")]
        public static extern int GetType(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetProcessedReverb")]
        public static extern unsafe ProcessedReverb* GetProcessedReverb(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetEAX")]
        public static extern unsafe EAXReverb* GetEAX(IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEmitterGetAmbientFilter")]
        public static extern unsafe LowPassFilter* GetAmbientFilter(IntPtr emitter);
    }
}
