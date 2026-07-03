using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper
{
    public static class PlanePrimitiveBindings
    {
        private const string DllName = "vaudionative";

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPlanePrimitiveCreate")]
        public static extern IntPtr Create();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPlanePrimitiveSetWidth")]
        public static extern void SetWidth(IntPtr primitive, float width);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPlanePrimitiveSetHeight")]
        public static extern void SetHeight(IntPtr primitive, float height);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPlanePrimitiveSetTransform")]
        public static extern unsafe void SetTransform(IntPtr primitive, ref Matrix transform);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPlanePrimitiveGetWidth")]
        public static extern float GetWidth(IntPtr primitive);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPlanePrimitiveGetHeight")]
        public static extern float GetHeight(IntPtr primitive);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPlanePrimitiveGetTransform")]
        public static extern unsafe Matrix* GetTransform(IntPtr primitive);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPlanePrimitiveGetMaterial")]
        public static extern MaterialType GetMaterial(IntPtr primitive);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPlanePrimitiveSetMaterial")]
        public static extern void SetMaterial(IntPtr primitive, MaterialType material);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPlanePrimitiveFree")]
        public static extern void Free(IntPtr primitive);
    }
}
