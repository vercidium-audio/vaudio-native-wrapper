using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper
{
    public static class WorldBindings
    {
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldCreate")]
        public static extern IntPtr Create();
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldDestroy")]
        public static extern void Destroy(IntPtr world);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldAddPrimitive_")]
        public static extern void AddPrimitive(IntPtr world, IntPtr primitive);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldRemovePrimitive_")]
        public static extern void RemovePrimitive(IntPtr world, IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldAddEmitter")]
        public static extern VAResult AddEmitter(IntPtr ctx, IntPtr emitter);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldRemoveEmitter")]
        public static extern void RemoveEmitter(IntPtr ctx, IntPtr emitter);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldUpdate")]
        public static extern void Update(IntPtr world);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldWait")]
        public static extern void Wait(IntPtr world);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetPendingShutdown")]
        public static extern void SetPendingShutdown(IntPtr world, bool value);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetPendingShutdown")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool GetPendingShutdown(IntPtr world);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetThreadsRunning")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool GetThreadsRunning(IntPtr world);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetMainThreadTime")]
        public static extern double GetMainThreadTime(IntPtr world);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetRaytracingTime")]
        public static extern double GetRaytracingTime(IntPtr world);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetPreparationTime")]
        public static extern double GetPreparationTime(IntPtr world);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetAnalysisTime")]
        public static extern double GetAnalysisTime(IntPtr world);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetPosition")]
        public static extern Vector GetPosition(IntPtr world);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetPosition")]
        public static extern VAResult SetPosition(IntPtr world, Vector position);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetSize")]
        public static extern Vector GetSize(IntPtr world);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetSize")]
        public static extern VAResult SetSize(IntPtr world, Vector size);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetMaxBounds")]
        public static extern Vector GetMaxBounds(IntPtr world);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetMaxBounds")]
        public static extern VAResult SetMaxBounds(IntPtr world, Vector maxBounds);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetGroupedEAX")]
        public static extern unsafe EAXReverb** GetGroupedEAX(IntPtr world);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetGroupedEAXCount")]
        public static extern int GetCurrentGroupedEAXCount(IntPtr world);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldCreateMaterial")]
        public static extern void CreateMaterial(IntPtr world, int materialId);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetMaterialAbsorptionLF")]
        public static extern float GetMaterialAbsorptionLF(IntPtr world, int materialId);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetMaterialAbsorptionHF")]
        public static extern float GetMaterialAbsorptionHF(IntPtr world, int materialId);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetMaterialScattering")]
        public static extern float GetMaterialScattering(IntPtr world, int materialId);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetMaterialTransmissionLF")]
        public static extern float GetMaterialTransmissionLF(IntPtr world, int materialId);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetMaterialTransmissionHF")]
        public static extern float GetMaterialTransmissionHF(IntPtr world, int materialId);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetMaterialPlaneTransmissionLF")]
        public static extern float GetMaterialPlaneTransmissionLF(IntPtr world, int materialId);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetMaterialPlaneTransmissionHF")]
        public static extern float GetMaterialPlaneTransmissionHF(IntPtr world, int materialId);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetMaterialAbsorptionLF")]
        public static extern VAResult SetMaterialAbsorptionLF(IntPtr world, int materialId, float value);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetMaterialAbsorptionHF")]
        public static extern VAResult SetMaterialAbsorptionHF(IntPtr world, int materialId, float value);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetMaterialScattering")]
        public static extern VAResult SetMaterialScattering(IntPtr world, int materialId, float value);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetMaterialTransmissionLF")]
        public static extern VAResult SetMaterialTransmissionLF(IntPtr world, int materialId, float value);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetMaterialTransmissionHF")]
        public static extern VAResult SetMaterialTransmissionHF(IntPtr world, int materialId, float value);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetMaterialPlaneTransmissionLF")]
        public static extern VAResult SetMaterialPlaneTransmissionLF(IntPtr world, int materialId, float value);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetMaterialPlaneTransmissionHF")]
        public static extern VAResult SetMaterialPlaneTransmissionHF(IntPtr world, int materialId, float value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetMaximumGroupedEAXCount")]
        public static extern int GetMaximumGroupedEAXCount(IntPtr world);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetMaximumGroupedEAXCount")]
        public static extern VAResult SetMaximumGroupedEAXCount(IntPtr world, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetWorkItemCount")]
        public static extern int GetWorkItemCount(IntPtr world);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetWorkItemCount")]
        public static extern VAResult SetWorkItemCount(IntPtr world, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetMaximumConcurrencyLevel")]
        public static extern int GetMaximumConcurrencyLevel(IntPtr world);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetMaximumConcurrencyLevel")]
        public static extern VAResult SetMaximumConcurrencyLevel(IntPtr world, int value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetSingleThreaded")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool GetSingleThreaded(IntPtr world);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetSingleThreaded")]
        public static extern void SetSingleThreaded(IntPtr world, bool value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetMetersPerUnit")]
        public static extern float GetMetersPerUnit(IntPtr world);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetMetersPerUnit")]
        public static extern VAResult SetMetersPerUnit(IntPtr world, float value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetInverseSpeedOfSound")]
        public static extern float GetInverseSpeedOfSound(IntPtr world);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetInverseSpeedOfSound")]
        public static extern VAResult SetInverseSpeedOfSound(IntPtr world, float value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetReferenceFrequencyLF")]
        public static extern float GetReferenceFrequencyLF(IntPtr world);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetReferenceFrequencyLF")]
        public static extern VAResult SetReferenceFrequencyLF(IntPtr world, float value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetReferenceFrequencyHF")]
        public static extern float GetReferenceFrequencyHF(IntPtr world);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetReferenceFrequencyHF")]
        public static extern VAResult SetReferenceFrequencyHF(IntPtr world, float value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetAirAbsorption")]
        public static extern IntPtr GetAirAbsorption(IntPtr world);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetAirAbsorption")]
        public static extern VAResult SetAirAbsorption(IntPtr world, IntPtr settings);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetAirAbsorptionHumidity")]
        public static extern void SetAirAbsorptionHumidity(IntPtr world, float value);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetAirAbsorptionTemperature")]
        public static extern void SetAirAbsorptionTemperature(IntPtr world, float value);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetAirAbsorptionPressure")]
        public static extern void SetAirAbsorptionPressure(IntPtr world, float value);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetAirAbsorptionCustomFormulaLF")]
        public static extern void SetAirAbsorptionCustomFormulaLF(IntPtr world, IntPtr formula);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetAirAbsorptionCustomFormulaHF")]
        public static extern void SetAirAbsorptionCustomFormulaHF(IntPtr world, IntPtr formula);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetCustomEAXFormulas")]
        public static extern unsafe void SetCustomEAXFormulas(IntPtr world, CustomEAXFormulas* formulas);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetEmittersOutsideTheWorldAreMuffled")]
        public static extern void SetEmittersOutsideTheWorldAreMuffled(IntPtr ctx, bool value);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetEmittersOutsideTheWorldAreMuffled")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool GetEmittersOutsideTheWorldAreMuffled(IntPtr ctx);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetWorldIsIndoors")]
        public static extern void SetWorldIsIndoors(IntPtr ctx, bool value);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetWorldIsIndoors")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool GetWorldIsIndoors(IntPtr ctx);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetEmitterCount")]
        public static extern int GetEmitterCount(IntPtr ctx);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetReverbCalculated")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool GetReverbCalculated(IntPtr ctx);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetOnReverbUpdatedCallback")]
        public static extern void SetOnReverbUpdated(IntPtr ctx, IntPtr callback);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetLogCallback")]
        public static extern void SetLogCallback(IntPtr ctx, IntPtr callback);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetLogMemoryAllocationWarnings")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool GetLogMemoryAllocationWarnings(IntPtr ctx);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetLogMemoryAllocationWarnings")]
        public static extern void SetLogMemoryAllocationWarnings(IntPtr ctx, bool value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetCoordinateSystem")]
        public static extern CoordinateSystem GetCoordinateSystem(IntPtr ctx);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetCoordinateSystem")]
        public static extern void SetCoordinateSystem(IntPtr ctx, CoordinateSystem value);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldCalculateListenerRelativePan")]
        public static extern Vector CalculateListenerRelativePan(IntPtr ctx, Vector worldVector, float listenerPitch, float listenerYaw);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetTotalPossibleRayCount")]
        public static extern int GetTotalPossibleRayCount(IntPtr ctx);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetRayCachePercent")]
        public static extern float GetRayCachePercent(IntPtr ctx);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetRaysCastThisFrame")]
        public static extern int GetRaysCastThisFrame(IntPtr ctx);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldExport")]
        public static extern VAResult Export(IntPtr world, [MarshalAs(UnmanagedType.LPStr)] string fileName);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldImport")]
        public static extern unsafe int Import(IntPtr world, [MarshalAs(UnmanagedType.LPStr)] string fileName, IntPtr** outEmitters, int* outEmitterCount);
    }
}
