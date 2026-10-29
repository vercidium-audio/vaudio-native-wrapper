using System;
using System.Runtime.InteropServices;

#if VA_DOUBLE_PRECISION
using VAFloat = System.Double;
#else
using VAFloat = System.Single;
#endif

namespace vaudionativewrapper
{
    public static class CirclePrimitiveBindings
    {
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCirclePrimitiveCreate")]
        public static extern IntPtr Create();

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCirclePrimitiveSetCenter")]
        public static extern VAResult SetCenter(IntPtr primitive, Vector center);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCirclePrimitiveSetRadius")]
        public static extern VAResult SetRadius(IntPtr primitive, VAFloat radius);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCirclePrimitiveGetCenter")]
        public static extern Vector GetCenter(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCirclePrimitiveGetRadius")]
        public static extern VAFloat GetRadius(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCirclePrimitiveDestroy")]
        public static extern VAResult Destroy(IntPtr primitive);
    }
}
