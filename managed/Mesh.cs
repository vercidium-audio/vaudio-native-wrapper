using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace vaudionativewrapper.managed
{
    public unsafe class Mesh
    {
        internal readonly IntPtr native;

        public Mesh(Vector[] vertices, Vector minBounds, Vector maxBounds)
        {
            IntPtr outMesh;

            fixed (Vector* ptr = vertices)
            {
                var result = MeshBindings.Create(ptr, vertices.Length, minBounds, maxBounds, &outMesh);
                Debug.Assert(result == VAResult.Success);
            }

            native = outMesh;
            Debug.Assert(native != IntPtr.Zero);
        }

        public Mesh(List<Vector> vertices, Vector minBounds, Vector maxBounds)
        {
            Vector[] copy = vertices.ToArray();
            IntPtr outMesh;

            fixed (Vector* ptr = copy)
            {
                var result = MeshBindings.Create(ptr, copy.Length, minBounds, maxBounds, &outMesh);
                Debug.Assert(result == VAResult.Success);
            }

            native = outMesh;
            Debug.Assert(native != IntPtr.Zero);
        }

        public void Destroy()
        {
            MeshBindings.Destroy(native);
        }
    }
}
