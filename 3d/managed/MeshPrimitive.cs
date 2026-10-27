using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace vaudionativewrapper.managed
{
    /// <summary>An audio primitive defined by an arbitrary triangle mesh. Must use clockwise winding.</summary>
    public unsafe class MeshPrimitive : Primitive
    {
        /// <summary>Create a mesh primitive from a list of vertices</summary>
        public MeshPrimitive(MaterialType material, List<Vector> vertices, Vector minBounds, Vector maxBounds, Matrix transform) : this(material, vertices.ToArray(), minBounds, maxBounds, transform) { }

        /// <summary>Create a mesh primitive from an array of vertices</summary>
        public MeshPrimitive(MaterialType material, Vector[] vertices, Vector minBounds, Vector maxBounds, Matrix transform) : this(material, new ReadOnlySpan<Vector>(vertices), minBounds, maxBounds, transform) { }

        /// <summary>Create a mesh primitive from a span of vertices</summary>
        public MeshPrimitive(MaterialType material, ReadOnlySpan<Vector> vertices, Vector minBounds, Vector maxBounds, Matrix transform)
        {
            MeshPrimitiveBindings.Create(material, in MemoryMarshal.GetReference(vertices), vertices.Length, minBounds, maxBounds, ref transform, out native).ThrowIfError();
            owns = true;
            mesh = new Mesh(MeshPrimitiveBindings.GetMesh(native));
        }

        /// <summary>Create a mesh primitive that shares geometry with a Mesh. The BVH is built once in the Mesh and reused by every instance.</summary>
        public MeshPrimitive(MaterialType material, Mesh mesh, Matrix transform)
        {
            MeshPrimitiveBindings.CreatePrimitiveFromMesh(material, mesh.native, ref transform, out native).ThrowIfError();
            owns = true;
            this.mesh = mesh;
        }

        /// <summary>The geometry used by this primitive. Primitives created from vertices create their own Mesh, which is freed automatically once every MeshPrimitive using it has been destroyed.<br/>
        /// Pass it to <see cref="MeshPrimitive(MaterialType, Mesh, Matrix)"/> to create more primitives that share the same geometry and BVH.</summary>
        public Mesh mesh { get; }

        /// <summary>Can contain scale, rotation and translation components</summary>
        public Matrix transform
        {
            get => *MeshPrimitiveBindings.GetTransform(native);
            set => MeshPrimitiveBindings.SetTransform(native, ref value).ThrowIfError();
        }

        protected override VAResult DestroyNative(IntPtr native) => MeshPrimitiveBindings.Destroy(native);

        protected override string DebugInfo => $"material={material}, UseFlatTransmission={UseFlatTransmission}";
    }
}
