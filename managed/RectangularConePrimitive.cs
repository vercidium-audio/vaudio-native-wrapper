namespace vaudionativewrapper.managed
{
    public unsafe class RectangularConePrimitive : Primitive
    {
        public RectangularConePrimitive()
        {
            native = RectangularConePrimitiveBindings.Create();
        }

        public MaterialType material
        {
            get => RectangularConePrimitiveBindings.GetMaterial(native);
            set => RectangularConePrimitiveBindings.SetMaterial(native, value);
        }

        public float width
        {
            get => RectangularConePrimitiveBindings.GetWidth(native);
            set => RectangularConePrimitiveBindings.SetWidth(native, value);
        }

        public float length
        {
            get => RectangularConePrimitiveBindings.GetLength(native);
            set => RectangularConePrimitiveBindings.SetLength(native, value);
        }

        public float height
        {
            get => RectangularConePrimitiveBindings.GetHeight(native);
            set => RectangularConePrimitiveBindings.SetHeight(native, value);
        }

        public Matrix transform
        {
            get => *RectangularConePrimitiveBindings.GetTransform(native);
            set => RectangularConePrimitiveBindings.SetTransform(native, ref value);
        }
    }
}
