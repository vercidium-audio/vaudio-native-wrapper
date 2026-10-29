using System;
using System.Runtime.InteropServices;

#if VA_DOUBLE_PRECISION
using VAFloat = System.Double;
#else
using VAFloat = System.Single;
#endif

namespace vaudionativewrapper
{
    public static class OvalPrimitiveBindings
    {
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaOvalPrimitiveCreate")]
        public static extern IntPtr Create();

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaOvalPrimitiveSetCenter")]
        public static extern VAResult SetCenter(IntPtr primitive, Vector center);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaOvalPrimitiveSetRadiusX")]
        public static extern VAResult SetRadiusX(IntPtr primitive, VAFloat radiusX);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaOvalPrimitiveSetRadiusY")]
        public static extern VAResult SetRadiusY(IntPtr primitive, VAFloat radiusY);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaOvalPrimitiveSetRotation")]
        public static extern VAResult SetRotation(IntPtr primitive, VAFloat rotation);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaOvalPrimitiveGetCenter")]
        public static extern Vector GetCenter(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaOvalPrimitiveGetRadiusX")]
        public static extern VAFloat GetRadiusX(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaOvalPrimitiveGetRadiusY")]
        public static extern VAFloat GetRadiusY(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaOvalPrimitiveGetRotation")]
        public static extern VAFloat GetRotation(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaOvalPrimitiveDestroy")]
        public static extern VAResult Destroy(IntPtr primitive);
    }
}
