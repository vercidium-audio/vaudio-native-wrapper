using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper
{
    public static class SpherePrimitiveBindings
    {
        private const string DllName = "vaudionative";

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaSpherePrimitiveCreate")]
        public static extern IntPtr Create();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaSpherePrimitiveSetCenter")]
        public static extern void SetCenter(IntPtr primitive, Vector center);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaSpherePrimitiveSetRadius")]
        public static extern void SetRadius(IntPtr primitive, float radius);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaSpherePrimitiveGetCenter")]
        public static extern Vector GetCenter(IntPtr primitive);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaSpherePrimitiveGetRadius")]
        public static extern float GetRadius(IntPtr primitive);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaSpherePrimitiveGetMaterial")]
        public static extern MaterialType GetMaterial(IntPtr primitive);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaSpherePrimitiveSetMaterial")]
        public static extern void SetMaterial(IntPtr primitive, MaterialType material);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaSpherePrimitiveFree")]
        public static extern void Free(IntPtr primitive);
    }
}
