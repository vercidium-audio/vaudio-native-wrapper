using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper
{
    public static class DiskPrimitiveBindings
    {
        private const string DllName = "vaudionative";

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaDiskPrimitiveCreate")]
        public static extern IntPtr Create();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaDiskPrimitiveSetRadius")]
        public static extern void SetRadius(IntPtr primitive, float radius);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaDiskPrimitiveSetTransform")]
        public static extern unsafe void SetTransform(IntPtr primitive, ref Matrix transform);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaDiskPrimitiveGetRadius")]
        public static extern float GetRadius(IntPtr primitive);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaDiskPrimitiveGetTransform")]
        public static extern unsafe Matrix* GetTransform(IntPtr primitive);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaDiskPrimitiveGetMaterial")]
        public static extern MaterialType GetMaterial(IntPtr primitive);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaDiskPrimitiveSetMaterial")]
        public static extern void SetMaterial(IntPtr primitive, MaterialType material);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaDiskPrimitiveFree")]
        public static extern void Free(IntPtr primitive);
    }
}
