namespace vaudionativewrapper.managed
{
    public unsafe class TriangularConePrimitive : Primitive
    {
        public TriangularConePrimitive()
        {
            native = TriangularConePrimitiveBindings.Create();
        }

        public MaterialType material
        {
            get => TriangularConePrimitiveBindings.GetMaterial(native);
            set => TriangularConePrimitiveBindings.SetMaterial(native, value);
        }

        public float radius
        {
            get => TriangularConePrimitiveBindings.GetRadius(native);
            set => TriangularConePrimitiveBindings.SetRadius(native, value);
        }

        public float height
        {
            get => TriangularConePrimitiveBindings.GetHeight(native);
            set => TriangularConePrimitiveBindings.SetHeight(native, value);
        }

        public Matrix transform
        {
            get => *TriangularConePrimitiveBindings.GetTransform(native);
            set => TriangularConePrimitiveBindings.SetTransform(native, ref value);
        }
    }
}
