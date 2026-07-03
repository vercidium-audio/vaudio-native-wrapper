using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper
{
    public static class CylinderPrimitiveBindings
    {
        private const string DllName = "vaudionative";

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCylinderPrimitiveCreate")]
        public static extern IntPtr Create();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCylinderPrimitiveSetRadius")]
        public static extern void SetRadius(IntPtr primitive, float radius);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCylinderPrimitiveSetLength")]
        public static extern void SetLength(IntPtr primitive, float length);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCylinderPrimitiveSetTransform")]
        public static extern unsafe void SetTransform(IntPtr primitive, ref Matrix transform);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCylinderPrimitiveGetRadius")]
        public static extern float GetRadius(IntPtr primitive);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCylinderPrimitiveGetLength")]
        public static extern float GetLength(IntPtr primitive);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCylinderPrimitiveGetTransform")]
        public static extern unsafe Matrix* GetTransform(IntPtr primitive);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCylinderPrimitiveGetMaterial")]
        public static extern MaterialType GetMaterial(IntPtr primitive);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCylinderPrimitiveSetMaterial")]
        public static extern void SetMaterial(IntPtr primitive, MaterialType material);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCylinderPrimitiveFree")]
        public static extern void Free(IntPtr primitive);
    }
}
