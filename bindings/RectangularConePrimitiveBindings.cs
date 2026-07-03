using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper
{
    public static class RectangularConePrimitiveBindings
    {
        private const string DllName = "vaudionative";

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaRectangularConePrimitiveCreate")]
        public static extern IntPtr Create();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaRectangularConePrimitiveSetWidth")]
        public static extern void SetWidth(IntPtr primitive, float width);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaRectangularConePrimitiveSetLength")]
        public static extern void SetLength(IntPtr primitive, float length);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaRectangularConePrimitiveSetHeight")]
        public static extern void SetHeight(IntPtr primitive, float height);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaRectangularConePrimitiveSetTransform")]
        public static extern unsafe void SetTransform(IntPtr primitive, ref Matrix transform);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaRectangularConePrimitiveGetWidth")]
        public static extern float GetWidth(IntPtr primitive);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaRectangularConePrimitiveGetLength")]
        public static extern float GetLength(IntPtr primitive);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaRectangularConePrimitiveGetHeight")]
        public static extern float GetHeight(IntPtr primitive);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaRectangularConePrimitiveGetTransform")]
        public static extern unsafe Matrix* GetTransform(IntPtr primitive);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaRectangularConePrimitiveGetMaterial")]
        public static extern MaterialType GetMaterial(IntPtr primitive);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaRectangularConePrimitiveSetMaterial")]
        public static extern void SetMaterial(IntPtr primitive, MaterialType material);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaRectangularConePrimitiveFree")]
        public static extern void Free(IntPtr primitive);
    }
}
