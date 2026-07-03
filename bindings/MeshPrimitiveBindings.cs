using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper
{
    public static class MeshPrimitiveBindings
    {
        private const string DllName = "vaudionative";

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaMeshPrimitiveCreate")]
        public static extern unsafe IntPtr Create(MaterialType material, Vector* vertices, int vertexCount, Vector minBounds, Vector maxBounds, ref Matrix transform);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaMeshPrimitiveFree")]
        public static extern void Free(IntPtr primitive);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaMeshPrimitiveGetTransform")]
        public static extern unsafe Matrix* GetTransform(IntPtr primitive);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaMeshPrimitiveSetTransform")]
        public static extern unsafe void SetTransform(IntPtr primitive, ref Matrix transform);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaMeshPrimitiveGetMaterial")]
        public static extern MaterialType GetMaterial(IntPtr primitive);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaMeshPrimitiveSetMaterial")]
        public static extern void SetMaterial(IntPtr primitive, MaterialType material);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaMeshPrimitiveGetSupports3DPermeation")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool GetSupports3DPermeation(IntPtr primitive);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaMeshPrimitiveSetSupports3DPermeation")]
        public static extern void SetSupports3DPermeation(IntPtr primitive, bool supports3DPermeation);
    }
}
