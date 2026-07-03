using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper
{
    public static class CapsulePrimitiveBindings
    {
        private const string DllName = "vaudionative";

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCapsulePrimitiveCreate")]
        public static extern IntPtr Create();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCapsulePrimitiveSetRadius")]
        public static extern void SetRadius(IntPtr primitive, float radius);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCapsulePrimitiveSetLength")]
        public static extern void SetLength(IntPtr primitive, float length);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCapsulePrimitiveSetTransform")]
        public static extern unsafe void SetTransform(IntPtr primitive, ref Matrix transform);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCapsulePrimitiveGetRadius")]
        public static extern float GetRadius(IntPtr primitive);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCapsulePrimitiveGetLength")]
        public static extern float GetLength(IntPtr primitive);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCapsulePrimitiveGetTransform")]
        public static extern unsafe Matrix* GetTransform(IntPtr primitive);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCapsulePrimitiveGetMaterial")]
        public static extern MaterialType GetMaterial(IntPtr primitive);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCapsulePrimitiveSetMaterial")]
        public static extern void SetMaterial(IntPtr primitive, MaterialType material);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCapsulePrimitiveFree")]
        public static extern void Free(IntPtr primitive);
    }
}
