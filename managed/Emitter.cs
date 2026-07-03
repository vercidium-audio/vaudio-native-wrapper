using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper.managed
{
    public unsafe class Emitter
    {
        public IntPtr native;

        public Emitter(IntPtr native)
        {
            this.native = native;
        }

        public Emitter()
        {
            native = EmitterBindings.Create();
        }

        public void Destroy()
        {
            EmitterBindings.Destroy(native);
        }

        public Vector Position
        {
            get => EmitterBindings.GetPosition(native);
            set => EmitterBindings.SetPosition(native, value);
        }

        public bool Initialising
        {
            get => EmitterBindings.GetInitialising(native);
        }

        public bool AffectsGroupedEAX
        {
            get => EmitterBindings.GetAffectsGroupedEax(native);
            set => EmitterBindings.SetAffectsGroupedEax(native, value);
        }

        public int GroupedEAXIndex => EmitterBindings.GetGroupedEaxIndex(native);

        public float OutsidePercent => EmitterBindings.GetOutsidePercent(native);

        public bool HasRelativeReverb
        {
            get => EmitterBindings.GetHasRelativeReverb(native);
            set => EmitterBindings.SetHasRelativeReverb(native, value);
        }

        public float RelativeReverbInnerThreshold
        {
            get => EmitterBindings.GetRelativeReverbInnerThreshold(native);
            set => EmitterBindings.SetRelativeReverbInnerThreshold(native, value);
        }

        public float RelativeReverbOuterThreshold
        {
            get => EmitterBindings.GetRelativeReverbOuterThreshold(native);
            set => EmitterBindings.SetRelativeReverbOuterThreshold(native, value);
        }

        public bool ClampPosition
        {
            get => EmitterBindings.GetClampPosition(native);
            set => EmitterBindings.SetClampPosition(native, value);
        }

        public string Name
        {
            get => EmitterBindings.GetName(native);
            set => EmitterBindings.SetName(native, value);
        }

        public int Type
        {
            get => EmitterBindings.GetType(native);
            set => EmitterBindings.SetType(native, value);
        }

        public EAXReverb EAX => new EAXReverb(EmitterBindings.GetEAX(native));

        public ProcessedReverb* ProcessedReverb => EmitterBindings.GetProcessedReverb(native);

        public LowPassFilter* AmbientFilter => EmitterBindings.GetAmbientFilter(native);

        public int ReverbRayCount
        {
            get => EmitterBindings.GetReverbRayCount(native);
            set => EmitterBindings.SetReverbRayCount(native, value);
        }

        public int ReverbBounceCount
        {
            get => EmitterBindings.GetReverbBounceCount(native);
            set => EmitterBindings.SetReverbBounceCount(native, value);
        }

        public int OcclusionRayCount
        {
            get => EmitterBindings.GetOcclusionRayCount(native);
            set => EmitterBindings.SetOcclusionRayCount(native, value);
        }

        public int OcclusionBounceCount
        {
            get => EmitterBindings.GetOcclusionBounceCount(native);
            set => EmitterBindings.SetOcclusionBounceCount(native, value);
        }

        public int PermeationRayCount
        {
            get => EmitterBindings.GetPermeationRayCount(native);
            set => EmitterBindings.SetPermeationRayCount(native, value);
        }

        public int PermeationBounceCount
        {
            get => EmitterBindings.GetPermeationBounceCount(native);
            set => EmitterBindings.SetPermeationBounceCount(native, value);
        }

        public int AmbientOcclusionRayCount
        {
            get => EmitterBindings.GetAmbientOcclusionRayCount(native);
            set => EmitterBindings.SetAmbientOcclusionRayCount(native, value);
        }

        public int AmbientOcclusionBounceCount
        {
            get => EmitterBindings.GetAmbientOcclusionBounceCount(native);
            set => EmitterBindings.SetAmbientOcclusionBounceCount(native, value);
        }

        public int AmbientPermeationRayCount
        {
            get => EmitterBindings.GetAmbientPermeationRayCount(native);
            set => EmitterBindings.SetAmbientPermeationRayCount(native, value);
        }

        public int AmbientPermeationBounceCount
        {
            get => EmitterBindings.GetAmbientPermeationBounceCount(native);
            set => EmitterBindings.SetAmbientPermeationBounceCount(native, value);
        }

        public int VisualisationRayCount
        {
            get => EmitterBindings.GetVisualisationRayCount(native);
            set => EmitterBindings.SetVisualisationRayCount(native, value);
        }

        public int VisualisationBounceCount
        {
            get => EmitterBindings.GetVisualisationBounceCount(native);
            set => EmitterBindings.SetVisualisationBounceCount(native, value);
        }

        public int VisualisationUpdateFrequency
        {
            get => EmitterBindings.GetVisualisationUpdateFrequency(native);
            set => EmitterBindings.SetVisualisationUpdateFrequency(native, value);
        }

        public bool CastsRays => EmitterBindings.CastsRays(native);

        public bool WithinWorldBounds => EmitterBindings.WithinWorldBounds(native);

        public int TrailCount => EmitterBindings.GetTrailCount(native);
        public int TrailBounceCount => EmitterBindings.GetTrailBounceCount(native);

        public bool ReverbEnabled => EmitterBindings.ReverbEnabled(native);
        public bool OcclusionEnabled => EmitterBindings.OcclusionEnabled(native);
        public bool PermeationEnabled => EmitterBindings.PermeationEnabled(native);
        public bool AmbientOcclusionEnabled => EmitterBindings.AmbientOcclusionEnabled(native);
        public bool AmbientPermeationEnabled => EmitterBindings.AmbientPermeationEnabled(native);
        public bool VisualisationEnabled => EmitterBindings.VisualisationEnabled(native);

        public int MaxEchogramTime
        {
            get => EmitterBindings.GetMaxEchogramTime(native);
            set => EmitterBindings.SetMaxEchogramTime(native, value);
        }

        public int EchogramGranularity
        {
            get => EmitterBindings.GetEchogramGranularity(native);
            set => EmitterBindings.SetEchogramGranularity(native, value);
        }

        public int RefreshRayCount
        {
            get => EmitterBindings.GetRefreshRayCount(native);
            set => EmitterBindings.SetRefreshRayCount(native, value);
        }

        public float RefreshDistanceThreshold
        {
            get => EmitterBindings.GetRefreshDistanceThreshold(native);
            set => EmitterBindings.SetRefreshDistanceThreshold(native, value);
        }

        public float ReverbEnergyCap
        {
            get => EmitterBindings.GetReverbEnergyCap(native);
            set => EmitterBindings.SetReverbEnergyCap(native, value);
        }

        public int ReservedEmitterTargets
        {
            get => EmitterBindings.GetReservedEmitterCount(native);
            set => EmitterBindings.SetReservedEmitterCount(native, value);
        }

        public float MinimumPermeationEnergy
        {
            get => EmitterBindings.GetMinimumPermeationEnergy(native);
            set => EmitterBindings.SetMinimumPermeationEnergy(native, value);
        }

        public int ScatteringSeed
        {
            get => EmitterBindings.GetScatteringSeed(native);
            set => EmitterBindings.SetScatteringSeed(native, value);
        }

        public void AddTarget(Emitter target) => EmitterBindings.AddTarget(native, target.native);

        public void RemoveTarget(Emitter target) => EmitterBindings.RemoveTarget(native, target.native);

        public bool HasTarget(Emitter target) => EmitterBindings.HasTarget(native, target.native);
        public bool HasRaytracedTarget(Emitter target) => EmitterBindings.HasRaytracedTarget(native, target.native);
        public LowPassFilter* GetTargetFilter(Emitter target) => EmitterBindings.GetTargetFilter(native, target.native);

        public void ResetTrails() => EmitterBindings.ResetTrails(native);

        public Vector[] OverridePositions
        {
            set
            {
                fixed (Vector* ptr = value)
                {
                    EmitterBindings.SetOverridePositions(native, ptr, value.Length);
                }
            }
        }

        public Vector[] OverrideRayDirections
        {
            set
            {
                fixed (Vector* ptr = value)
                {
                    EmitterBindings.SetOverrideRayDirections(native, ptr, value.Length);
                }
            }
        }

        private GCHandle _onRaytracingCompleteHandle;
        private GCHandle _onRaytracedByAnotherEmitterHandle;
        private GCHandle _visualisationCallbackHandle;
        private GCHandle _gainFormulaHandle;
        private GCHandle _ambientGainFormulaHandle;
        private GCHandle _logCallbackHandle;
        private GCHandle _logErrorCallbackHandle;

        public Action OnRaytracingComplete
        {
            set
            {
                if (_onRaytracingCompleteHandle.IsAllocated)
                    _onRaytracingCompleteHandle.Free();

                if (value != null)
                {
                    OnRaytracingCompleteFn callback = () => value.Invoke();

                    _onRaytracingCompleteHandle = GCHandle.Alloc(callback);
                    EmitterBindings.SetOnRaytracingCompleteCallback(native, callback);
                }
                else
                {
                    EmitterBindings.SetOnRaytracingCompleteCallback(native, null);
                }
            }
        }

        public Action<Emitter> OnRaytracedByAnotherEmitter
        {
            set
            {
                if (_onRaytracedByAnotherEmitterHandle.IsAllocated)
                    _onRaytracedByAnotherEmitterHandle.Free();

                if (value != null)
                {
                    vaudionativewrapper.OnRaytracedByAnotherEmitterFn callback = (source, target) => value.Invoke(source == IntPtr.Zero ? null : new Emitter(source));
                    _onRaytracedByAnotherEmitterHandle = GCHandle.Alloc(callback);
                    EmitterBindings.SetOnRaytracedByAnotherEmitterCallback(native, callback);
                }
                else
                {
                    EmitterBindings.SetOnRaytracedByAnotherEmitterCallback(native, null);
                }
            }
        }

        public Action<VisualisationData[]> VisualisationCallback
        {
            set
            {
                if (_visualisationCallbackHandle.IsAllocated)
                    _visualisationCallbackHandle.Free();

                if (value != null)
                {
                    // TODO - just return raw VisualisationData* pointer?
                    VisualisationCallbackFn callback = (dataPtr, count) =>
                    {
                        var arr = new VisualisationData[count];

                        for (int i = 0; i < count; i++)
                            arr[i] = new VisualisationData { position = dataPtr[i].position, normal = dataPtr[i].normal };

                        value.Invoke(arr);
                    };

                    _visualisationCallbackHandle = GCHandle.Alloc(callback);
                    EmitterBindings.SetVisualisationCallback(native, callback);
                }
                else
                {
                    EmitterBindings.SetVisualisationCallback(native, null);
                }
            }
        }

        public Func<bool, int, int, int, float, float, float> GainFormula
        {
            set
            {
                if (_gainFormulaHandle.IsAllocated)
                    _gainFormulaHandle.Free();

                if (value != null)
                {
                    GainFormulaDelegate callback = (lf, ocRay, permRay, permBounce, ocEnergy, permEnergy) =>
                        value(lf, ocRay, permRay, permBounce, ocEnergy, permEnergy);
                    _gainFormulaHandle = GCHandle.Alloc(callback);
                    EmitterBindings.SetGainFormula(native, Marshal.GetFunctionPointerForDelegate(callback));
                }
                else
                {
                    EmitterBindings.SetGainFormula(native, IntPtr.Zero);
                }
            }
        }

        public Func<bool, int, int, int, float, float, float> AmbientGainFormula
        {
            set
            {
                if (_ambientGainFormulaHandle.IsAllocated)
                    _ambientGainFormulaHandle.Free();

                if (value != null)
                {
                    GainFormulaDelegate callback = (lf, ocRay, permRay, permBounce, ocEnergy, permEnergy) =>
                        value(lf, ocRay, permRay, permBounce, ocEnergy, permEnergy);
                    _ambientGainFormulaHandle = GCHandle.Alloc(callback);
                    EmitterBindings.SetAmbientGainFormula(native, Marshal.GetFunctionPointerForDelegate(callback));
                }
                else
                {
                    EmitterBindings.SetAmbientGainFormula(native, IntPtr.Zero);
                }
            }
        }

        public Action<string> LogCallback
        {
            set
            {
                if (_logCallbackHandle.IsAllocated)
                    _logCallbackHandle.Free();

                if (value != null)
                {
                    LogCallbackFn callback = (msg) => value(msg);
                    _logCallbackHandle = GCHandle.Alloc(callback);
                    EmitterBindings.SetLogCallback(native, callback);
                }
                else
                {
                    EmitterBindings.SetLogCallback(native, null);
                }
            }
        }

        public Action<string> LogErrorCallback
        {
            set
            {
                if (_logErrorCallbackHandle.IsAllocated)
                    _logErrorCallbackHandle.Free();

                if (value != null)
                {
                    LogCallbackFn callback = (msg) => value(msg);
                    _logErrorCallbackHandle = GCHandle.Alloc(callback);
                    EmitterBindings.SetLogErrorCallback(native, callback);
                }
                else
                {
                    EmitterBindings.SetLogErrorCallback(native, null);                    
                }
            }
        }
    }
}