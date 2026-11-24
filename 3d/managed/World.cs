// Human-created

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#if VA_DOUBLE_PRECISION
using VAFloat = System.Double;
#else
using VAFloat = System.Single;
#endif

namespace vaudionativewrapper.managed
{
    /// <summary>A standalone world with its own primitives, emitters, materials and settings. Manages its own raytracing and multithreading.</summary>
    public unsafe partial class World
    {
        UnsafeVoxelMaterialMap unsafeVoxelMaterialMap;

        // Static so IL2CPP can marshal them. Native passes back userData, a GCHandle to the map
        static readonly UnsafeVoxelMaterialCallback getMaterialNative = GetMaterialNative;
        static readonly UnsafeVoxelIsSolidCallback isSolidNative = IsSolidNative;
        static readonly IntPtr getMaterialNativePtr = Marshal.GetFunctionPointerForDelegate(getMaterialNative);
        static readonly IntPtr isSolidNativePtr = Marshal.GetFunctionPointerForDelegate(isSolidNative);

        [AOT.MonoPInvokeCallback(typeof(UnsafeVoxelMaterialCallback))]
        static MaterialType GetMaterialNative(void* voxel, IntPtr userData) => ((UnsafeVoxelMaterialMap)GCHandle.FromIntPtr(userData).Target).GetMaterial(voxel);

        [AOT.MonoPInvokeCallback(typeof(UnsafeVoxelIsSolidCallback))]
        static bool IsSolidNative(void* voxel, IntPtr userData) => ((UnsafeVoxelMaterialMap)GCHandle.FromIntPtr(userData).Target).IsSolid(voxel);

        // One handle per map assigned to this world. In-flight raytracing threads may still use a replaced map's handle, so they're only freed once the world is destroyed
        readonly Dictionary<UnsafeVoxelMaterialMap, GCHandle> unsafeVoxelMapHandles = new Dictionary<UnsafeVoxelMaterialMap, GCHandle>();

        /// <summary>
        /// Converts a voxel in a UnsafeVoxelPrimitive to a MaterialType.<br/>
        /// If the mapping changes, assign it here again, and all rays that intersect with UnsafeVoxelPrimitives will be re-cast.
        /// </summary>
        public unsafe UnsafeVoxelMaterialMap UnsafeVoxelMaterialMap
        {
            get => unsafeVoxelMaterialMap;
            set
            {
                if (value == null)
                {
                    WorldBindings.SetUnsafeVoxelMaterialCallbacks(native, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero).ThrowIfError();
                    unsafeVoxelMaterialMap = null;
                    return;
                }

                if (!unsafeVoxelMapHandles.TryGetValue(value, out var handle))
                {
                    handle = GCHandle.Alloc(value);
                    unsafeVoxelMapHandles[value] = handle;
                }

                // Re-sending the same map re-casts rays that hit UnsafeVoxelPrimitives
                WorldBindings.SetUnsafeVoxelMaterialCallbacks(native, getMaterialNativePtr, isSolidNativePtr, GCHandle.ToIntPtr(handle)).ThrowIfError();
                unsafeVoxelMaterialMap = value;
            }
        }

        partial void OnDestroyDimensional()
        {
            foreach (var handle in unsafeVoxelMapHandles.Values)
                handle.Free();

            unsafeVoxelMapHandles.Clear();
        }

        /// <summary>The pitch of the camera in the debug window (dev build only)</summary>
        public VAFloat CameraPitch
        {
            get => WorldBindings.GetCameraPitch(native);
            set => WorldBindings.SetCameraPitch(native, value).ThrowIfError();
        }

        /// <summary>The yaw of the camera in the debug window (dev build only)</summary>
        public VAFloat CameraYaw
        {
            get => WorldBindings.GetCameraYaw(native);
            set => WorldBindings.SetCameraYaw(native, value).ThrowIfError();
        }

        /// <summary>The vertical field of view (in radians) of the camera in the debug window (dev build only)</summary>
        public float FieldOfView
        {
            get => WorldBindings.GetFieldOfView(native);
            set => WorldBindings.SetFieldOfView(native, value).ThrowIfError();
        }

        /// <summary>Convert a world-space direction (e.g. RelativeDirections) to a listener-space direction, for use as EFX reflections/late reverb pan. Same as ConvertWorldToListenerDirection.</summary>
        public Vector CalculateListenerRelativePan(Vector worldVector, VAFloat listenerPitch, VAFloat listenerYaw)
        {
            WorldBindings.CalculateListenerRelativePan(native, worldVector, listenerPitch, listenerYaw, out var pan).ThrowIfError();
            return pan;
        }

        /// <summary>Convert a world-space direction (in the world's CoordinateSystem) to a listener-space direction, based on the listener's pitch and yaw. Listener space is always X+ right, Y+ up and Z+ forward relative to the listener, regardless of CoordinateSystem, matching the EFX reflections/late reverb pan convention.</summary>
        public Vector ConvertWorldToListenerDirection(Vector worldDirection, VAFloat listenerPitch, VAFloat listenerYaw)
        {
            WorldBindings.ConvertWorldToListenerDirection(native, worldDirection, listenerPitch, listenerYaw, out var direction).ThrowIfError();
            return direction;
        }

        /// <summary>Convert a listener-space direction (X+ right, Y+ up, Z+ forward relative to the listener) to a world-space direction (in the world's CoordinateSystem), based on the listener's pitch and yaw.</summary>
        public Vector ConvertListenerToWorldDirection(Vector listenerDirection, VAFloat listenerPitch, VAFloat listenerYaw)
        {
            WorldBindings.ConvertListenerToWorldDirection(native, listenerDirection, listenerPitch, listenerYaw, out var direction).ThrowIfError();
            return direction;
        }

    }
}
