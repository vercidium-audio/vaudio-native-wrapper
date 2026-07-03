namespace vaudionativewrapper.managed
{
    public unsafe class TriangularPrismPrimitive : Primitive
    {
        public TriangularPrismPrimitive()
        {
            native = TriangularPrismPrimitiveBindings.Create();
        }

        public MaterialType material
        {
            get => TriangularPrismPrimitiveBindings.GetMaterial(native);
            set => TriangularPrismPrimitiveBindings.SetMaterial(native, value);
        }

        public float radius
        {
            get => TriangularPrismPrimitiveBindings.GetRadius(native);
            set => TriangularPrismPrimitiveBindings.SetRadius(native, value);
        }

        public float length
        {
            get => TriangularPrismPrimitiveBindings.GetLength(native);
            set => TriangularPrismPrimitiveBindings.SetLength(native, value);
        }

        public Matrix transform
        {
            get => *TriangularPrismPrimitiveBindings.GetTransform(native);
            set => TriangularPrismPrimitiveBindings.SetTransform(native, ref value);
        }
    }
}
