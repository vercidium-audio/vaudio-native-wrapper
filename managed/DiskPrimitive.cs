namespace vaudionativewrapper.managed
{
    public unsafe class DiskPrimitive : Primitive
    {
        public DiskPrimitive()
        {
            native = DiskPrimitiveBindings.Create();
        }

        public MaterialType material
        {
            get => DiskPrimitiveBindings.GetMaterial(native);
            set => DiskPrimitiveBindings.SetMaterial(native, value).ThrowIfError();
        }

        public float radius
        {
            get => DiskPrimitiveBindings.GetRadius(native);
            set => DiskPrimitiveBindings.SetRadius(native, value).ThrowIfError();
        }

        public Matrix transform
        {
            get => *DiskPrimitiveBindings.GetTransform(native);
            set => DiskPrimitiveBindings.SetTransform(native, ref value).ThrowIfError();
        }

        public void Destroy()
        {
            DiskPrimitiveBindings.Destroy(native).ThrowIfError();
        }
    }
}
