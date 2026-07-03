using System.Collections.Generic;

namespace vaudionativewrapper.managed
{
    public unsafe class MeshPrimitive : Primitive
    {
        public MeshPrimitive(MaterialType material, List<Vector> vertices, Vector minBounds, Vector maxBounds, Matrix transform)
        {
            Vector[] copy = vertices.ToArray();

            fixed (Vector* ptr = copy)
            {
                native = MeshPrimitiveBindings.Create(material, ptr, copy.Length, minBounds, maxBounds, ref transform);
            }
        }

        public MeshPrimitive(MaterialType material, Vector[] vertices, Vector minBounds, Vector maxBounds, Matrix transform)
        {
            fixed (Vector* ptr = vertices)
            {
                native = MeshPrimitiveBindings.Create(material, ptr, vertices.Length, minBounds, maxBounds, ref transform);
            }
        }

        public MeshPrimitive(MaterialType material, Mesh mesh, Matrix transform)
        {
            native = MeshPrimitiveBindings.CreatePrimitiveFromMesh(material, mesh.native, ref transform);
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

        public bool supports3DPermeation
        {
            get => MeshPrimitiveBindings.GetSupports3DPermeation(native);
            set => MeshPrimitiveBindings.SetSupports3DPermeation(native, value);
        }
    }
}
