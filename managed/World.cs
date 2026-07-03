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

        public void AddMaterial(MaterialType type, MaterialProperties material)
        {
            var id = (int)type;

            WorldBindings.CreateMaterial(native, id);
            WorldBindings.SetMaterialAbsorptionLF(native, id, material.AbsorptionLF);
            WorldBindings.SetMaterialAbsorptionHF(native, id, material.AbsorptionHF);
            WorldBindings.SetMaterialScattering(native, id, material.Scattering);
            WorldBindings.SetMaterialTransmissionLF(native, id, material.TransmissionLF);
            WorldBindings.SetMaterialTransmissionHF(native, id, material.TransmissionHF);
            WorldBindings.SetMaterialPlaneTransmissionLF(native, id, material.PlaneTransmissionLF);
            WorldBindings.SetMaterialPlaneTransmissionHF(native, id, material.PlaneTransmissionHF);
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
            Debug.Assert(result == 0);
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
            get
            {
                return vaudionativewrapper.WorldBindings.GetSize(native);
            }
            set
            {
                WorldBindings.SetSize(native, value);
            }
        }

        public Vector Position
        {
            get
            {
                return WorldBindings.GetPosition(native);
            }
            set
            {
                WorldBindings.SetPosition(native, value);
            }
        }

        public Vector MaxBounds
        {
            get
            {
                return WorldBindings.GetMaxBounds(native);
            }
            set
            {
                WorldBindings.SetMaxBounds(native, value);
            }
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

        public List<vaudionativewrapper.managed.EAXReverb> GroupedEAX
        {
            get
            {
                List<vaudionativewrapper.managed.EAXReverb> results = new List<vaudionativewrapper.managed.EAXReverb>();

                var listPtr = WorldBindings.GetGroupedEAX(native);
                var count = WorldBindings.GetCurrentGroupedEAXCount(native);

                for (int i = 0; i < count; i++)
                    results.Add(new vaudionativewrapper.managed.EAXReverb() { native = listPtr[i] });

                return results;
            }
        }

        public bool EmittersOutsideTheWorldAreMuffled
        {
            get => WorldBindings.GetEmittersOutsideTheWorldAreMuffled(native);
            set
            {
                WorldBindings.SetEmittersOutsideTheWorldAreMuffled(native, value);
            }
        }

        public bool ReverbCalculated
        {
            get => WorldBindings.GetReverbCalculated(native);
        }

        public int TotalPossibleRayCount
        {
            get
            {
                return WorldBindings.GetTotalPossibleRayCount(native);
            }
        }

        public float RayCachePercent
        {
            get
            {
                return WorldBindings.GetRayCachePercent(native);
            }
        }

        public int RaysCastThisFrame
        {
            get
            {
                return WorldBindings.GetRaysCastThisFrame(native);
            }
        }

        public int MaximumGroupedEAXCount
        {
            get
            {
                return WorldBindings.GetMaximumGroupedEAXCount(native);
            }
            set
            {
                WorldBindings.SetMaximumGroupedEAXCount(native, value);
            }
        }

        public int WorkItemCount
        {
            get
            {
                return WorldBindings.GetWorkItemCount(native);
            }
            set
            {
                WorldBindings.SetWorkItemCount(native, value);
            }
        }

        public int MaximumConcurrencyLevel
        {
            get
            {
                return WorldBindings.GetMaximumConcurrencyLevel(native);
            }
            set
            {
                WorldBindings.SetMaximumConcurrencyLevel(native, value);
            }
        }

        public float MetersPerUnit
        {
            get
            {
                return WorldBindings.GetMetersPerUnit(native);
            }
            set
            {
                WorldBindings.SetMetersPerUnit(native, value);
            }
        }

        public float InverseSpeedOfSound
        {
            get
            {
                return WorldBindings.GetInverseSpeedOfSound(native);
            }
            set
            {
                WorldBindings.SetInverseSpeedOfSound(native, value);
            }
        }

        public float ReferenceFrequencyLF
        {
            get
            {
                return WorldBindings.GetReferenceFrequencyLF(native);
            }
            set
            {
                WorldBindings.SetReferenceFrequencyLF(native, value);
            }
        }

        public float ReferenceFrequencyHF
        {
            get
            {
                return WorldBindings.GetReferenceFrequencyHF(native);
            }
            set
            {
                WorldBindings.SetReferenceFrequencyHF(native, value);
            }
        }

        public managed.AirAbsorptionSettings AirAbsorption
        {
            get
            {
                return new managed.AirAbsorptionSettings(vaudionativewrapper.WorldBindings.GetAirAbsorption(native));
            }
            set
            {
                WorldBindings.SetAirAbsorption(native, value.native);
            }
        }

        public CustomEAXFormulaCallbacks UpdateCustomEAXFormulas(managed.CustomEAXFormulas formulas)
        {
            var callbacks = CustomEAXFormulaHelper.CreateCustomEAXFormulaCallbacks(formulas);

            var nativeFormulas = vaudionativewrapper.CustomEAXFormulasBindings.Create();

            nativeFormulas->initialise = Marshal.GetFunctionPointerForDelegate(callbacks.Initialise);
            nativeFormulas->calculateDiffusion = Marshal.GetFunctionPointerForDelegate(callbacks.CalculateDiffusion);
            nativeFormulas->calculateDensity = Marshal.GetFunctionPointerForDelegate(callbacks.CalculateDensity);
            nativeFormulas->calculateReflectionsDelay = Marshal.GetFunctionPointerForDelegate(callbacks.CalculateReflectionsDelay);
            nativeFormulas->calculateLateReverbDelay = Marshal.GetFunctionPointerForDelegate(callbacks.CalculateLateReverbDelay);
            nativeFormulas->calculateFrequencyGains = Marshal.GetFunctionPointerForDelegate(callbacks.CalculateFrequencyGains);
            nativeFormulas->calculateReflectionsAndLateReverbGain = Marshal.GetFunctionPointerForDelegate(callbacks.CalculateReflectionsAndLateReverbGain);
            nativeFormulas->calculateRT60 = Marshal.GetFunctionPointerForDelegate(callbacks.CalculateRT60);

            vaudionativewrapper.WorldBindings.SetCustomEAXFormulas(native, nativeFormulas);

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
            set
            {
                WorldBindings.SetLogMemoryAllocationWarnings(native, value);
            }
        }

        public static Vector CalculateListenerRelativePan(Vector worldVector, float listenerPitch, float listenerYaw)
        {
            return WorldBindings.CalculateListenerRelativePan(worldVector, listenerPitch, listenerYaw);
        }
    }
}