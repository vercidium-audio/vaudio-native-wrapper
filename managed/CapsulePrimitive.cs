namespace vaudionativewrapper.managed
{
    public unsafe class CapsulePrimitive : Primitive
    {
        public CapsulePrimitive()
        {
            native = CapsulePrimitiveBindings.Create();
        }

        public MaterialType material
        {
            get => CapsulePrimitiveBindings.GetMaterial(native);
            set => CapsulePrimitiveBindings.SetMaterial(native, value);
        }

        public float radius
        {
            get => CapsulePrimitiveBindings.GetRadius(native);
            set => CapsulePrimitiveBindings.SetRadius(native, value);
        }

        public float length
        {
            get => CapsulePrimitiveBindings.GetLength(native);
            set => CapsulePrimitiveBindings.SetLength(native, value);
        }

        public Matrix transform
        {
            get => *CapsulePrimitiveBindings.GetTransform(native);
            set => CapsulePrimitiveBindings.SetTransform(native, ref value);
        }
    }
}
