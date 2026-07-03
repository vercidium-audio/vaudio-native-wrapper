namespace vaudionativewrapper.managed
{
    public unsafe class PrismPrimitive : Primitive
    {
        public PrismPrimitive()
        {
            native = PrismPrimitiveBindings.Create();
        }

        public MaterialType material
        {
            get => PrismPrimitiveBindings.GetMaterial(native);
            set => PrismPrimitiveBindings.SetMaterial(native, value);
        }

        public Vector size
        {
            get => PrismPrimitiveBindings.GetSize(native);
            set => PrismPrimitiveBindings.SetSize(native, value);
        }

        public Matrix transform
        {
            get => *PrismPrimitiveBindings.GetTransform(native);
            set => PrismPrimitiveBindings.SetTransform(native, ref value);
        }
    }
}
