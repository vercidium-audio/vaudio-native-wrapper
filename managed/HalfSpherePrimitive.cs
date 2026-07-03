namespace vaudionativewrapper.managed
{
    public unsafe class HalfSpherePrimitive : Primitive
    {
        public HalfSpherePrimitive()
        {
            native = HalfSpherePrimitiveBindings.Create();
        }

        public MaterialType material
        {
            get => HalfSpherePrimitiveBindings.GetMaterial(native);
            set => HalfSpherePrimitiveBindings.SetMaterial(native, value);
        }

        public float radius
        {
            get => HalfSpherePrimitiveBindings.GetRadius(native);
            set => HalfSpherePrimitiveBindings.SetRadius(native, value);
        }

        public Matrix transform
        {
            get => *HalfSpherePrimitiveBindings.GetTransform(native);
            set => HalfSpherePrimitiveBindings.SetTransform(native, ref value);
        }
    }
}
