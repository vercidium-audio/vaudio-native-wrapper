using System;
using System.Runtime.InteropServices;

#if VA_DOUBLE_PRECISION
using VAFloat = System.Double;
#else
using VAFloat = System.Single;
#endif

namespace vaudionativewrapper
{
    public static class CylinderPrimitiveBindings
    {
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCylinderPrimitiveCreate")]
        public static extern IntPtr Create();

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCylinderPrimitiveSetRadius")]
        public static extern VAResult SetRadius(IntPtr primitive, VAFloat radius);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCylinderPrimitiveSetLength")]
        public static extern VAResult SetLength(IntPtr primitive, VAFloat length);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCylinderPrimitiveSetTransform")]
        public static extern VAResult SetTransform(IntPtr primitive, ref Matrix transform);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCylinderPrimitiveGetRadius")]
        public static extern VAFloat GetRadius(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCylinderPrimitiveGetLength")]
        public static extern VAFloat GetLength(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCylinderPrimitiveGetTransform")]
        public static extern unsafe Matrix* GetTransform(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCylinderPrimitiveDestroy")]
        public static extern VAResult Destroy(IntPtr primitive);
    }
}
