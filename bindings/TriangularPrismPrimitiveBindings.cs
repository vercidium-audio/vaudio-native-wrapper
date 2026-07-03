using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper
{
    public static class TriangularPrismPrimitiveBindings
    {
        private const string DllName = "vaudionative";

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaTriangularPrismPrimitiveCreate")]
        public static extern IntPtr Create();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaTriangularPrismPrimitiveSetRadius")]
        public static extern void SetRadius(IntPtr primitive, float radius);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaTriangularPrismPrimitiveSetLength")]
        public static extern void SetLength(IntPtr primitive, float length);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaTriangularPrismPrimitiveSetTransform")]
        public static extern unsafe void SetTransform(IntPtr primitive, ref Matrix transform);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaTriangularPrismPrimitiveGetRadius")]
        public static extern float GetRadius(IntPtr primitive);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaTriangularPrismPrimitiveGetLength")]
        public static extern float GetLength(IntPtr primitive);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaTriangularPrismPrimitiveGetTransform")]
        public static extern unsafe Matrix* GetTransform(IntPtr primitive);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaTriangularPrismPrimitiveGetMaterial")]
        public static extern MaterialType GetMaterial(IntPtr primitive);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaTriangularPrismPrimitiveSetMaterial")]
        public static extern void SetMaterial(IntPtr primitive, MaterialType material);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaTriangularPrismPrimitiveFree")]
        public static extern void Free(IntPtr primitive);
    }
}
