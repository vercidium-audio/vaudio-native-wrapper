using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper
{
    public static class MeshBindings
    {
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaMeshCreate")]
        public static extern unsafe IntPtr Create(Vector* vertices, int vertexCount, Vector minBounds, Vector maxBounds);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaMeshDestroy")]
        public static extern VAResult Destroy(IntPtr mesh);
    }
}
