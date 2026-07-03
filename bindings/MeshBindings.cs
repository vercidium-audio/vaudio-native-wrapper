using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper
{
    public static class MeshBindings
    {
        private const string DllName = "vaudionative";

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaMeshCreate")]
        public static extern unsafe IntPtr Create(Vector* vertices, int vertexCount, Vector minBounds, Vector maxBounds);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaMeshDestroy")]
        public static extern int Destroy(IntPtr mesh);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaMeshPrimitiveCreateFromMesh")]
        public static extern IntPtr CreatePrimitiveFromMesh(MaterialType material, IntPtr mesh, ref Matrix transform);
    }
}
