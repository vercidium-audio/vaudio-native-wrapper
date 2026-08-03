using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper
{
    public static class MeshPrimitiveBindings
    {
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaMeshPrimitiveCreate")]
        public static extern unsafe VAResult Create(MaterialType material, Vector* vertices, int vertexCount, Vector minBounds, Vector maxBounds, ref Matrix transform, IntPtr* outPrimitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaMeshPrimitiveDestroy")]
        public static extern VAResult Destroy(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaMeshPrimitiveGetTransform")]
        public static extern unsafe Matrix* GetTransform(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaMeshPrimitiveSetTransform")]
        public static extern VAResult SetTransform(IntPtr primitive, ref Matrix transform);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaMeshPrimitiveGetMaterial")]
        public static extern MaterialType GetMaterial(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaMeshPrimitiveSetMaterial")]
        public static extern VAResult SetMaterial(IntPtr primitive, MaterialType material);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaMeshPrimitiveGetSupports3DPermeation")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool GetSupports3DPermeation(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaMeshPrimitiveSetSupports3DPermeation")]
        public static extern VAResult SetSupports3DPermeation(IntPtr primitive, bool supports3DPermeation);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaMeshPrimitiveCreateFromMesh")]
        public static extern unsafe VAResult CreatePrimitiveFromMesh(MaterialType material, IntPtr mesh, ref Matrix transform, IntPtr* outPrimitive);
    }
}
