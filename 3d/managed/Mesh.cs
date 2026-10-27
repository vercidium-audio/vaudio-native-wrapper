using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace vaudionativewrapper.managed
{
    /// <summary>A triangulated mesh that is built once and can be shared across multiple MeshPrimitive instances. Must use clockwise winding.</summary>
    public class Mesh
    {
        public IntPtr native;

        // False for a mesh created internally by a vertex MeshPrimitive, which is freed automatically once every MeshPrimitive using it has been destroyed
        readonly bool owns;

        /// <summary>Create a mesh from a list of vertices</summary>
        public Mesh(List<Vector> vertices, Vector minBounds, Vector maxBounds) : this(vertices.ToArray(), minBounds, maxBounds) { }

        /// <summary>Create a mesh from an array or span of vertices</summary>
        public Mesh(ReadOnlySpan<Vector> vertices, Vector minBounds, Vector maxBounds)
        {
            MeshBindings.Create(in MemoryMarshal.GetReference(vertices), vertices.Length, minBounds, maxBounds, out native).ThrowIfError();
            owns = true;
        }

        internal Mesh(IntPtr native)
        {
            this.native = native;
        }

        /// <summary>Destroy the native mesh. If a world's background threads are still using it, it is freed automatically once they finish.<br/>
        /// Returns <see cref="VAResult.ErrorInUse"/> if a MeshPrimitive using this mesh has not been destroyed yet.<br/>
        /// Does nothing for a mesh returned by <see cref="MeshPrimitive.mesh"/> on a MeshPrimitive created from vertices, as it's freed automatically once every MeshPrimitive using it has been destroyed.</summary>
        public VAResult Destroy()
        {
            if (!owns || native == IntPtr.Zero)
                return VAResult.Success;

            var result = MeshBindings.Destroy(native);

            if (result == VAResult.Success)
                native = IntPtr.Zero;

            return result;
        }

        ~Mesh()
        {
            if (owns && native != IntPtr.Zero)
                LogSettings.Warn("Mesh was garbage collected without calling Destroy() first.");
        }
    }
}
