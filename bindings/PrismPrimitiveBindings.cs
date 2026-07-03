using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper
{
    public static class PrismPrimitiveBindings
    {
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPrismPrimitiveCreate")]
        public static extern IntPtr Create();

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPrismPrimitiveSetSize")]
        public static extern void SetSize(IntPtr primitive, Vector size);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPrismPrimitiveSetTransform")]
        public static extern unsafe void SetTransform(IntPtr primitive, ref Matrix transform);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPrismPrimitiveGetSize")]
        public static extern Vector GetSize(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPrismPrimitiveGetTransform")]
        public static extern unsafe Matrix* GetTransform(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPrismPrimitiveGetMaterial")]
        public static extern MaterialType GetMaterial(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPrismPrimitiveSetMaterial")]
        public static extern void SetMaterial(IntPtr primitive, MaterialType material);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPrismPrimitiveFree")]
        public static extern void Free(IntPtr primitive);
    }
}
