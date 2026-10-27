using System;
using System.Runtime.InteropServices;

#if VA_DOUBLE_PRECISION
using VAFloat = System.Double;
#else
using VAFloat = System.Single;
#endif

namespace vaudionativewrapper
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate MaterialType UnsafeVoxelMaterialCallback(void* voxel, IntPtr userData);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    public unsafe delegate bool UnsafeVoxelIsSolidCallback(void* voxel, IntPtr userData);

    public static partial class WorldBindings
    {
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetUnsafeVoxelMaterialCallbacks")]
        public static extern VAResult SetUnsafeVoxelMaterialCallbacks(IntPtr world, IntPtr getMaterial, IntPtr isSolid, IntPtr userData);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetUnsafeVoxelMaterialCallback")]
        public static extern IntPtr GetUnsafeVoxelMaterialCallback(IntPtr world);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetUnsafeVoxelIsSolidCallback")]
        public static extern IntPtr GetUnsafeVoxelIsSolidCallback(IntPtr world);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetCameraPitch")]
        public static extern VAFloat GetCameraPitch(IntPtr world);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetCameraPitch")]
        public static extern VAResult SetCameraPitch(IntPtr world, VAFloat pitch);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetCameraYaw")]
        public static extern VAFloat GetCameraYaw(IntPtr world);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetCameraYaw")]
        public static extern VAResult SetCameraYaw(IntPtr world, VAFloat yaw);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetFieldOfView")]
        public static extern float GetFieldOfView(IntPtr world);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetFieldOfView")]
        public static extern VAResult SetFieldOfView(IntPtr world, float fieldOfView);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldCalculateListenerRelativePan")]
        public static extern VAResult CalculateListenerRelativePan(IntPtr ctx, Vector worldVector, VAFloat listenerPitch, VAFloat listenerYaw, out Vector pan);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldConvertWorldToListenerDirection")]
        public static extern VAResult ConvertWorldToListenerDirection(IntPtr ctx, Vector worldDirection, VAFloat listenerPitch, VAFloat listenerYaw, out Vector direction);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldConvertListenerToWorldDirection")]
        public static extern VAResult ConvertListenerToWorldDirection(IntPtr ctx, Vector listenerDirection, VAFloat listenerPitch, VAFloat listenerYaw, out Vector direction);
    }
}
