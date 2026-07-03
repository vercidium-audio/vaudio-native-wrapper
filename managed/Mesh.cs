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
            fixed (Vector* ptr = vertices)
            {
                native = MeshBindings.Create(ptr, vertices.Length, minBounds, maxBounds);
                Debug.Assert(native != IntPtr.Zero);
            }
        }

        public Mesh(bool isNative, List<Vector> vertices, Vector minBounds, Vector maxBounds)
        {
            Vector[] copy = vertices.ToArray();

            fixed (Vector* ptr = copy)
            {
                native = MeshBindings.Create(ptr, copy.Length, minBounds, maxBounds);
                Debug.Assert(native != IntPtr.Zero);
            }
        }

        public void Destroy()
        {
            MeshBindings.Destroy(native);
        }
    }
}
