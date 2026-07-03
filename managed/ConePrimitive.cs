namespace vaudionativewrapper.managed
{
    public unsafe class ConePrimitive : Primitive
    {
        public ConePrimitive()
        {
            native = ConePrimitiveBindings.Create();
        }

        public MaterialType material
        {
            get => ConePrimitiveBindings.GetMaterial(native);
            set => ConePrimitiveBindings.SetMaterial(native, value);
        }

        public float radius
        {
            get => ConePrimitiveBindings.GetRadius(native);
            set => ConePrimitiveBindings.SetRadius(native, value);
        }

        public float height
        {
            get => ConePrimitiveBindings.GetHeight(native);
            set => ConePrimitiveBindings.SetHeight(native, value);
        }

        public Matrix transform
        {
            get => *ConePrimitiveBindings.GetTransform(native);
            set => ConePrimitiveBindings.SetTransform(native, ref value);
        }
    }
}
