using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper
{
    public static class PrimitiveBindings
    {
        private const string DllName = "vaudionative";

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPrimitiveSetMaterial")]
        public static extern void SetMaterial(IntPtr primitive, MaterialType materialType);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPrimitiveGetMaterial")]
        public static extern MaterialType GetMaterial(IntPtr primitive);
    }
}
