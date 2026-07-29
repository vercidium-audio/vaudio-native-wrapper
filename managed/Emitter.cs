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
            EmitterBindings.Destroy(native).ThrowIfError();
        }

        public Vector Position
        {
            get => EmitterBindings.GetPosition(native);
            set => EmitterBindings.SetPosition(native, value).ThrowIfError();
        }

        public bool Initialising
        {
            get => EmitterBindings.GetInitialising(native);
        }

        public bool PendingRemoval
        {
            get => EmitterBindings.GetPendingRemoval(native);
        }

        public bool AffectsGroupedEAX
        {
            get => EmitterBindings.GetAffectsGroupedEAX(native);
            set => EmitterBindings.SetAffectsGroupedEAX(native, value).ThrowIfError();
        }

        public int GroupedEAXIndex => EmitterBindings.GetGroupedEAXIndex(native);

        public float OutsidePercent => EmitterBindings.GetOutsidePercent(native);

        public bool HasRelativeReverb
        {
            get => EmitterBindings.GetHasRelativeReverb(native);
            set => EmitterBindings.SetHasRelativeReverb(native, value).ThrowIfError();
        }

        public float RelativeReverbInnerThreshold
        {
            get => EmitterBindings.GetRelativeReverbInnerThreshold(native);
            set => EmitterBindings.SetRelativeReverbInnerThreshold(native, value).ThrowIfError();
        }

        public float RelativeReverbOuterThreshold
        {
            get => EmitterBindings.GetRelativeReverbOuterThreshold(native);
            set => EmitterBindings.SetRelativeReverbOuterThreshold(native, value).ThrowIfError();
        }

        public bool ClampPosition
        {
            get => EmitterBindings.GetClampPosition(native);
            set => EmitterBindings.SetClampPosition(native, value).ThrowIfError();
        }

        public string Name
        {
            get => EmitterBindings.GetName(native);
            set => EmitterBindings.SetName(native, value).ThrowIfError();
        }

        public int Type
        {
            get => EmitterBindings.GetType(native);
            set => EmitterBindings.SetType(native, value).ThrowIfError();
        }

        public IntPtr UserData
        {
            get => EmitterBindings.GetUserData(native);
            set => EmitterBindings.SetUserData(native, value).ThrowIfError();
        }

        public EAXReverb EAX => new EAXReverb(EmitterBindings.GetEAX(native));

        public ProcessedReverb ProcessedReverb => new ProcessedReverb(EmitterBindings.GetProcessedReverb(native));

        public LowPassFilter* AmbientFilter => EmitterBindings.GetAmbientFilter(native);

        public int ReverbRayCount
        {
            get => EmitterBindings.GetReverbRayCount(native);
            set => EmitterBindings.SetReverbRayCount(native, value).ThrowIfError();
        }

        public int ReverbBounceCount
        {
            get => EmitterBindings.GetReverbBounceCount(native);
            set => EmitterBindings.SetReverbBounceCount(native, value).ThrowIfError();
        }

        public int OcclusionRayCount
        {
            get => EmitterBindings.GetOcclusionRayCount(native);
            set => EmitterBindings.SetOcclusionRayCount(native, value).ThrowIfError();
        }

        public int OcclusionBounceCount
        {
            get => EmitterBindings.GetOcclusionBounceCount(native);
            set => EmitterBindings.SetOcclusionBounceCount(native, value).ThrowIfError();
        }

        public int PermeationRayCount
        {
            get => EmitterBindings.GetPermeationRayCount(native);
            set => EmitterBindings.SetPermeationRayCount(native, value).ThrowIfError();
        }

        public int PermeationBounceCount
        {
            get => EmitterBindings.GetPermeationBounceCount(native);
            set => EmitterBindings.SetPermeationBounceCount(native, value).ThrowIfError();
        }

        public int AmbientOcclusionRayCount
        {
            get => EmitterBindings.GetAmbientOcclusionRayCount(native);
            set => EmitterBindings.SetAmbientOcclusionRayCount(native, value).ThrowIfError();
        }

        public int AmbientOcclusionBounceCount
        {
            get => EmitterBindings.GetAmbientOcclusionBounceCount(native);
            set => EmitterBindings.SetAmbientOcclusionBounceCount(native, value).ThrowIfError();
        }

        public int AmbientPermeationRayCount
        {
            get => EmitterBindings.GetAmbientPermeationRayCount(native);
            set => EmitterBindings.SetAmbientPermeationRayCount(native, value).ThrowIfError();
        }

        public int AmbientPermeationBounceCount
        {
            get => EmitterBindings.GetAmbientPermeationBounceCount(native);
            set => EmitterBindings.SetAmbientPermeationBounceCount(native, value).ThrowIfError();
        }

        public int VisualisationRayCount
        {
            get => EmitterBindings.GetVisualisationRayCount(native);
            set => EmitterBindings.SetVisualisationRayCount(native, value).ThrowIfError();
        }

        public int VisualisationBounceCount
        {
            get => EmitterBindings.GetVisualisationBounceCount(native);
            set => EmitterBindings.SetVisualisationBounceCount(native, value).ThrowIfError();
        }

        public int VisualisationUpdateFrequency
        {
            get => EmitterBindings.GetVisualisationUpdateFrequency(native);
            set => EmitterBindings.SetVisualisationUpdateFrequency(native, value).ThrowIfError();
        }

        public bool CastsRays => EmitterBindings.CastsAnyRays(native);

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
            set => EmitterBindings.SetMaxEchogramTime(native, value).ThrowIfError();
        }

        public int EchogramGranularity
        {
            get => EmitterBindings.GetEchogramGranularity(native);
            set => EmitterBindings.SetEchogramGranularity(native, value).ThrowIfError();
        }

        public int RefreshRayCount
        {
            get => EmitterBindings.GetRefreshRayCount(native);
            set => EmitterBindings.SetRefreshRayCount(native, value).ThrowIfError();
        }

        public float RefreshDistanceThreshold
        {
            get => EmitterBindings.GetRefreshDistanceThreshold(native);
            set => EmitterBindings.SetRefreshDistanceThreshold(native, value).ThrowIfError();
        }

        public float ReverbEnergyCap
        {
            get => EmitterBindings.GetReverbEnergyCap(native);
            set => EmitterBindings.SetReverbEnergyCap(native, value).ThrowIfError();
        }

        public float OcclusionEnergyCap
        {
            get => EmitterBindings.GetOcclusionEnergyCap(native);
            set => EmitterBindings.SetOcclusionEnergyCap(native, value).ThrowIfError();
        }

        public float PermeationEnergyCap
        {
            get => EmitterBindings.GetPermeationEnergyCap(native);
            set => EmitterBindings.SetPermeationEnergyCap(native, value).ThrowIfError();
        }

        public float AmbientOcclusionEnergyCap
        {
            get => EmitterBindings.GetAmbientOcclusionEnergyCap(native);
            set => EmitterBindings.SetAmbientOcclusionEnergyCap(native, value).ThrowIfError();
        }

        public float AmbientPermeationEnergyCap
        {
            get => EmitterBindings.GetAmbientPermeationEnergyCap(native);
            set => EmitterBindings.SetAmbientPermeationEnergyCap(native, value).ThrowIfError();
        }

        public float MaxVolume
        {
            get => EmitterBindings.GetMaxVolume(native);
            set => EmitterBindings.SetMaxVolume(native, value).ThrowIfError();
        }

        public float MinimumPermeationEnergy
        {
            get => EmitterBindings.GetMinimumPermeationEnergy(native);
            set => EmitterBindings.SetMinimumPermeationEnergy(native, value).ThrowIfError();
        }

        public int ScatteringSeed
        {
            get => EmitterBindings.GetScatteringSeed(native);
            set => EmitterBindings.SetScatteringSeed(native, value).ThrowIfError();
        }

        public void AddTarget(Emitter target) => EmitterBindings.AddTarget(native, target.native);

        public void RemoveTarget(Emitter target) => EmitterBindings.RemoveTarget(native, target.native).ThrowIfError();

        public bool HasTarget(Emitter target) => EmitterBindings.HasTarget(native, target.native);
        public bool HasRaytracedTarget(Emitter target) => EmitterBindings.HasRaytracedTarget(native, target.native);
        public LowPassFilter* GetTargetFilter(Emitter target) => EmitterBindings.GetTargetFilter(native, target.native);

        public void ResetTrails() => EmitterBindings.ResetTrails(native).ThrowIfError();

        public Vector[] OverridePositions
        {
            set
            {
                fixed (Vector* ptr = value)
                {
                    EmitterBindings.SetOverridePositions(native, ptr, value.Length).ThrowIfError();
                }
            }
        }

        public Vector[] OverrideRayDirections
        {
            set
            {
                fixed (Vector* ptr = value)
                {
                    EmitterBindings.SetOverrideRayDirections(native, ptr, value.Length).ThrowIfError();
                }
            }
        }

        private GCHandle _onRaytracingCompleteHandle;
        private GCHandle _onRaytracedByAnotherEmitterHandle;
        private GCHandle _onRemovedHandle;
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
                    OnRaytracingCompleteFn callback = (emitter) => value.Invoke();

                    _onRaytracingCompleteHandle = GCHandle.Alloc(callback);
                    EmitterBindings.SetOnRaytracingCompleteCallback(native, callback).ThrowIfError();
                }
                else
                {
                    EmitterBindings.SetOnRaytracingCompleteCallback(native, null).ThrowIfError();
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
                    EmitterBindings.SetOnRaytracedByAnotherEmitterCallback(native, callback).ThrowIfError();
                }
                else
                {
                    EmitterBindings.SetOnRaytracedByAnotherEmitterCallback(native, null).ThrowIfError();
                }
            }
        }

        public Action OnRemoved
        {
            set
            {
                if (_onRemovedHandle.IsAllocated)
                    _onRemovedHandle.Free();

                if (value != null)
                {
                    OnRemovedFn callback = () => value.Invoke();

                    _onRemovedHandle = GCHandle.Alloc(callback);
                    EmitterBindings.SetOnRemovedCallback(native, callback).ThrowIfError();
                }
                else
                {
                    EmitterBindings.SetOnRemovedCallback(native, null).ThrowIfError();
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
                    VisualisationCallbackFn callback = (dataPtr, count) =>
                    {
                        var arr = new VisualisationData[count];

                        for (int i = 0; i < count; i++)
                            arr[i] = new VisualisationData { position = dataPtr[i].position, normal = dataPtr[i].normal };

                        value.Invoke(arr);
                    };

                    _visualisationCallbackHandle = GCHandle.Alloc(callback);
                    EmitterBindings.SetVisualisationCallback(native, callback).ThrowIfError();
                }
                else
                {
                    EmitterBindings.SetVisualisationCallback(native, null).ThrowIfError();
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
                    EmitterBindings.SetGainFormula(native, Marshal.GetFunctionPointerForDelegate(callback)).ThrowIfError();
                }
                else
                {
                    EmitterBindings.SetGainFormula(native, IntPtr.Zero).ThrowIfError();
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
                    EmitterBindings.SetAmbientGainFormula(native, Marshal.GetFunctionPointerForDelegate(callback)).ThrowIfError();
                }
                else
                {
                    EmitterBindings.SetAmbientGainFormula(native, IntPtr.Zero).ThrowIfError();
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
                    EmitterBindings.SetLogCallback(native, callback).ThrowIfError();
                }
                else
                {
                    EmitterBindings.SetLogCallback(native, null).ThrowIfError();
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
                    EmitterBindings.SetLogErrorCallback(native, callback).ThrowIfError();
                }
                else
                {
                    EmitterBindings.SetLogErrorCallback(native, null).ThrowIfError();
                }
            }
        }
    }
}