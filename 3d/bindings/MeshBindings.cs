// Human-created

using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper
{
    public static class MeshBindings
    {
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaMeshCreate")]
        public static extern VAResult Create(in Vector vertices, int vertexCount, Vector minBounds, Vector maxBounds, out IntPtr outMesh);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaMeshCreateWithMaterials")]
        public static extern VAResult CreateWithMaterials(in Vector vertices, in MaterialType materials, int vertexCount, Vector minBounds, Vector maxBounds, out IntPtr outMesh);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaMeshDestroy")]
        public static extern VAResult Destroy(IntPtr mesh);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaMeshSetPermeationMaterial")]
        public static extern VAResult SetPermeationMaterial(IntPtr mesh, MaterialType material);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaMeshGetPermeationMaterial")]
        public static extern MaterialType GetPermeationMaterial(IntPtr mesh);
    }
}
