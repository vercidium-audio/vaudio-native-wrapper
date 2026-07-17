using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace vaudionativewrapper.managed
{
    public unsafe class World
    {
        public IntPtr native;

        public World()
        {
            native = WorldBindings.Create();
        }

        public World(IntPtr native)
        {
            this.native = native;
        }

        public void Dispose()
        {
            if (native == null)
                return;

            WorldBindings.Destroy(native);
            native = IntPtr.Zero;
        }

        public void Update()
        {
            WorldBindings.Update(native);
        }

        public void Wait()
        {
            WorldBindings.Wait(native);
        }

        public MaterialProperties CreateMaterial(MaterialType type)
        {
            var id = (int)type;

            WorldBindings.CreateMaterial(native, id);

            return new MaterialProperties(native, id);
        }

        public void AddPrimitive(Primitive primitive)
        {
            WorldBindings.AddPrimitive(native, primitive.native);
        }

        public void RemovePrimitive(Primitive primitive)
        {
            WorldBindings.RemovePrimitive(native, primitive.native);
        }

        public void AddEmitter(Emitter emitter)
        {
            VAResult result = WorldBindings.AddEmitter(native, emitter.native);
            Debug.Assert(result == VAResult.Success);
        }

        public void RemoveEmitter(Emitter emitter)
        {
            WorldBindings.RemoveEmitter(native, emitter.native);
        }

        public int GetEmitterCount()
        {
            return WorldBindings.GetEmitterCount(native);
        }

        public Vector Size
        {
            get => WorldBindings.GetSize(native);
            set => WorldBindings.SetSize(native, value);
        }

        public Vector Position
        {
            get => WorldBindings.GetPosition(native);
            set => WorldBindings.SetPosition(native, value);
        }

        public Vector MaxBounds
        {
            get => WorldBindings.GetMaximumBounds(native);
            set => WorldBindings.SetMaximumBounds(native, value);
        }

        public void UpdateWorldPosition(Vector position)
        {
            WorldBindings.SetPosition(native, position);
        }

        public void UpdateWorldSize(Vector size)
        {
            WorldBindings.SetSize(native, size);
        }

        public void UpdateWorldBounds(Vector position, Vector size)
        {
            UpdateWorldPosition(position);
            UpdateWorldSize(size);
        }

        public double MainThreadTime => WorldBindings.GetMainThreadTime(native);
        public double PreparationTime => WorldBindings.GetPreparationTime(native);
        public double RaytracingTime => WorldBindings.GetRaytracingTime(native);
        public double AnalysisTime => WorldBindings.GetAnalysisTime(native);

        public List<EAXReverb> GroupedEAX
        {
            get
            {
                List<EAXReverb> results = new List<EAXReverb>();

                var listPtr = WorldBindings.GetGroupedEAX(native);
                var count = WorldBindings.GetCurrentGroupedEAXCount(native);

                for (int i = 0; i < count; i++)
                    results.Add(new EAXReverb(listPtr[i]));

                return results;
            }
        }

        public bool EmittersOutsideTheWorldAreMuffled
        {
            get => WorldBindings.GetEmittersOutsideTheWorldAreMuffled(native);
            set => WorldBindings.SetEmittersOutsideTheWorldAreMuffled(native, value);
        }

        public bool WorldIsIndoors
        {
            get => WorldBindings.GetWorldIsIndoors(native);
            set => WorldBindings.SetWorldIsIndoors(native, value);
        }

        public bool Initialising => WorldBindings.GetInitialising(native);

        public int RaysCastThisFrame => WorldBindings.GetRaysCastThisFrame(native);

        public int MaximumGroupedEAXCount
        {
            get => WorldBindings.GetMaximumGroupedEAXCount(native);
            set => WorldBindings.SetMaximumGroupedEAXCount(native, value);
        }

        public int WorkItemCount
        {
            get => WorldBindings.GetWorkItemCount(native);
            set => WorldBindings.SetWorkItemCount(native, value);
        }

        public int MaximumConcurrencyLevel
        {
            get => WorldBindings.GetMaximumConcurrencyLevel(native);
            set => WorldBindings.SetMaximumConcurrencyLevel(native, value);
        }

        public float MetersPerUnit
        {
            get => WorldBindings.GetMetersPerUnit(native);
            set => WorldBindings.SetMetersPerUnit(native, value);
        }

        public float InverseSpeedOfSound
        {
            get => WorldBindings.GetInverseSpeedOfSound(native);
            set => WorldBindings.SetInverseSpeedOfSound(native, value);
        }

        public float ReferenceFrequencyLF
        {
            get => WorldBindings.GetReferenceFrequencyLF(native);
            set => WorldBindings.SetReferenceFrequencyLF(native, value);
        }

        public float ReferenceFrequencyHF
        {
            get => WorldBindings.GetReferenceFrequencyHF(native);
            set => WorldBindings.SetReferenceFrequencyHF(native, value);
        }

        public AirAbsorptionSettings AirAbsorption
        {
            get => new AirAbsorptionSettings(WorldBindings.GetAirAbsorption(native));
            set => WorldBindings.SetAirAbsorption(native, value.native);
        }

        private GCHandle[] _customEAXFormulaHandles;

        public CustomEAXFormulaCallbacks UpdateCustomEAXFormulas(managed.CustomEAXFormulas formulas)
        {
            var callbacks = CustomEAXFormulaHelper.CreateCustomEAXFormulaCallbacks(formulas);

            var nativeFormulas = CustomEAXFormulasBindings.Create();

            nativeFormulas->initialise = Marshal.GetFunctionPointerForDelegate(callbacks.Initialise);
            nativeFormulas->calculateDiffusion = Marshal.GetFunctionPointerForDelegate(callbacks.CalculateDiffusion);
            nativeFormulas->calculateDensity = Marshal.GetFunctionPointerForDelegate(callbacks.CalculateDensity);
            nativeFormulas->calculateReflectionsDelay = Marshal.GetFunctionPointerForDelegate(callbacks.CalculateReflectionsDelay);
            nativeFormulas->calculateLateReverbDelay = Marshal.GetFunctionPointerForDelegate(callbacks.CalculateLateReverbDelay);
            nativeFormulas->calculateFrequencyGains = Marshal.GetFunctionPointerForDelegate(callbacks.CalculateFrequencyGains);
            nativeFormulas->calculateReflectionsAndLateReverbGain = Marshal.GetFunctionPointerForDelegate(callbacks.CalculateReflectionsAndLateReverbGain);
            nativeFormulas->calculateRT60 = Marshal.GetFunctionPointerForDelegate(callbacks.CalculateRT60);

            WorldBindings.SetCustomEAXFormulas(native, nativeFormulas);

            // Native holds raw function pointers into these 8 delegates, invoked from native worker
            // threads. A managed reference via the returned CustomEAXFormulaCallbacks isn't a reliable
            // guarantee against collection for that pattern (see AirAbsorptionSettings), so pin each
            // delegate explicitly for as long as native might call back into it.
            if (_customEAXFormulaHandles != null)
                foreach (var handle in _customEAXFormulaHandles)
                    if (handle.IsAllocated)
                        handle.Free();

            _customEAXFormulaHandles = new[]
            {
                GCHandle.Alloc(callbacks.Initialise),
                GCHandle.Alloc(callbacks.CalculateDiffusion),
                GCHandle.Alloc(callbacks.CalculateDensity),
                GCHandle.Alloc(callbacks.CalculateReflectionsDelay),
                GCHandle.Alloc(callbacks.CalculateLateReverbDelay),
                GCHandle.Alloc(callbacks.CalculateFrequencyGains),
                GCHandle.Alloc(callbacks.CalculateReflectionsAndLateReverbGain),
                GCHandle.Alloc(callbacks.CalculateRT60),
            };

            return callbacks;
        }

        public MaterialProperties GetMaterial(MaterialType type)
        {
            return new MaterialProperties(native, (int)type);
        }

        private GCHandle _onReverbUpdatedHandle;
        private GCHandle _logCallbackHandle;

        public Action OnReverbUpdated
        {
            set
            {
                if (_onReverbUpdatedHandle.IsAllocated)
                    _onReverbUpdatedHandle.Free();

                if (value != null)
                {
                    OnRaytracingCompleteFn fn = () => value();
                    _onReverbUpdatedHandle = GCHandle.Alloc(fn);

                    WorldBindings.SetOnReverbUpdated(native, Marshal.GetFunctionPointerForDelegate(fn));
                }
                else
                {
                    WorldBindings.SetOnReverbUpdated(native, IntPtr.Zero);
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
                    LogCallbackFn fn = (msg) => value(msg);
                    _logCallbackHandle = GCHandle.Alloc(fn);

                    WorldBindings.SetLogCallback(native, Marshal.GetFunctionPointerForDelegate(fn));
                }
                else
                {
                    WorldBindings.SetLogCallback(native, IntPtr.Zero);
                }
            }
        }

        public bool LogMemoryAllocationWarnings
        {
            get => WorldBindings.GetLogMemoryAllocationWarnings(native);
            set => WorldBindings.SetLogMemoryAllocationWarnings(native, value);
        }

        /// <summary>
        /// Coordinate system used when calculating listener-relative reverb directionality (<see cref="CalculateListenerRelativePan"/>).
        /// </summary>
        public CoordinateSystem CoordinateSystem
        {
            get => WorldBindings.GetCoordinateSystem(native);
            set => WorldBindings.SetCoordinateSystem(native, value);
        }

        public Vector CalculateListenerRelativePan(Vector worldVector, float listenerPitch, float listenerYaw)
        {
            return WorldBindings.CalculateListenerRelativePan(native, worldVector, listenerPitch, listenerYaw);
        }

        public bool PendingShutdown
        {
            get => WorldBindings.GetPendingShutdown(native);
            set => WorldBindings.SetPendingShutdown(native, value);
        }

        public bool ThreadsRunning => WorldBindings.GetThreadsRunning(native);
    }
}