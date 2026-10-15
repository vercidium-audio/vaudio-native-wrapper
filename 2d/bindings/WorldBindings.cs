using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper
{
    public static partial class WorldBindings
    {
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetCameraRotation")]
        public static extern float GetCameraRotation(IntPtr world);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetCameraRotation")]
        public static extern VAResult SetCameraRotation(IntPtr world, float rotation);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldGetCameraZoom")]
        public static extern float GetCameraZoom(IntPtr world);
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldSetCameraZoom")]
        public static extern VAResult SetCameraZoom(IntPtr world, float zoom);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldCalculateListenerRelativePan")]
        public static extern VAResult CalculateListenerRelativePan(IntPtr ctx, Vector worldVector, float listenerRotation, out Vector pan);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldConvertWorldToListenerDirection")]
        public static extern VAResult ConvertWorldToListenerDirection(IntPtr ctx, Vector worldDirection, float listenerRotation, out Vector direction);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaWorldConvertListenerToWorldDirection")]
        public static extern VAResult ConvertListenerToWorldDirection(IntPtr ctx, Vector listenerDirection, float listenerRotation, out Vector direction);

    }
}
