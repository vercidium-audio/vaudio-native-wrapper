using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace vaudionativewrapper.managed
{
    public unsafe class MeshPrimitive : Primitive
    {
        public MeshPrimitive(MaterialType material, List<Vector> vertices, Vector minBounds, Vector maxBounds, Matrix transform)
        {
            Vector[] copy = vertices.ToArray();
            IntPtr outPrimitive;

            fixed (Vector* ptr = copy)
            {
                var result = MeshPrimitiveBindings.Create(material, ptr, copy.Length, minBounds, maxBounds, ref transform, &outPrimitive);
                Debug.Assert(result == VAResult.Success);
            }

            native = outPrimitive;
        }

        public MeshPrimitive(MaterialType material, Vector[] vertices, Vector minBounds, Vector maxBounds, Matrix transform)
        {
            IntPtr outPrimitive;

            fixed (Vector* ptr = vertices)
            {
                var result = MeshPrimitiveBindings.Create(material, ptr, vertices.Length, minBounds, maxBounds, ref transform, &outPrimitive);
                Debug.Assert(result == VAResult.Success);
            }

            native = outPrimitive;
        }

        public MeshPrimitive(MaterialType material, Mesh mesh, Matrix transform)
        {
            IntPtr outPrimitive;
            var result = MeshPrimitiveBindings.CreatePrimitiveFromMesh(material, mesh.native, ref transform, &outPrimitive);
            Debug.Assert(result == VAResult.Success);

            native = outPrimitive;
        }

        public MaterialType material
        {
            get => MeshPrimitiveBindings.GetMaterial(native);
            set => MeshPrimitiveBindings.SetMaterial(native, value);
        }

        public Matrix transform
        {
            get => *MeshPrimitiveBindings.GetTransform(native);
            set => MeshPrimitiveBindings.SetTransform(native, ref value);
        }

        public bool Supports3DPermeation
        {
            get => MeshPrimitiveBindings.GetSupports3DPermeation(native);
            set => MeshPrimitiveBindings.SetSupports3DPermeation(native, value);
        }
    }
}
