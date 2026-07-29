using System;
using System.Collections.Generic;

namespace vaudionativewrapper.managed
{
    public unsafe class Mesh
    {
        internal readonly IntPtr native;

        public Mesh(Vector[] vertices, Vector minBounds, Vector maxBounds)
        {
            IntPtr outMesh;

            fixed (Vector* ptr = vertices)
                MeshBindings.Create(ptr, vertices.Length, minBounds, maxBounds, &outMesh).ThrowIfError();

            native = outMesh;
        }

        public Mesh(List<Vector> vertices, Vector minBounds, Vector maxBounds)
        {
            Vector[] copy = vertices.ToArray();
            IntPtr outMesh;

            fixed (Vector* ptr = copy)
                MeshBindings.Create(ptr, copy.Length, minBounds, maxBounds, &outMesh).ThrowIfError();

            native = outMesh;
        }

        public VAResult Destroy() => MeshBindings.Destroy(native);
    }
}
