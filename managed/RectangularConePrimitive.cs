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
            set => RectangularConePrimitiveBindings.SetMaterial(native, value).ThrowIfError();
        }

        public float width
        {
            get => RectangularConePrimitiveBindings.GetWidth(native);
            set => RectangularConePrimitiveBindings.SetWidth(native, value).ThrowIfError();
        }

        public float length
        {
            get => RectangularConePrimitiveBindings.GetLength(native);
            set => RectangularConePrimitiveBindings.SetLength(native, value).ThrowIfError();
        }

        public float height
        {
            get => RectangularConePrimitiveBindings.GetHeight(native);
            set => RectangularConePrimitiveBindings.SetHeight(native, value).ThrowIfError();
        }

        public Matrix transform
        {
            get => *RectangularConePrimitiveBindings.GetTransform(native);
            set => RectangularConePrimitiveBindings.SetTransform(native, ref value).ThrowIfError();
        }

        public void Destroy()
        {
            RectangularConePrimitiveBindings.Destroy(native).ThrowIfError();
        }
    }
}
