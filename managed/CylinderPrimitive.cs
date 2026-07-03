namespace vaudionativewrapper.managed
{
    public unsafe class CylinderPrimitive : Primitive
    {
        public CylinderPrimitive()
        {
            native = CylinderPrimitiveBindings.Create();
        }

        public MaterialType material
        {
            get => CylinderPrimitiveBindings.GetMaterial(native);
            set => CylinderPrimitiveBindings.SetMaterial(native, value);
        }

        public float radius
        {
            get => CylinderPrimitiveBindings.GetRadius(native);
            set => CylinderPrimitiveBindings.SetRadius(native, value);
        }

        public float length
        {
            get => CylinderPrimitiveBindings.GetLength(native);
            set => CylinderPrimitiveBindings.SetLength(native, value);
        }

        public Matrix transform
        {
            get => *CylinderPrimitiveBindings.GetTransform(native);
            set => CylinderPrimitiveBindings.SetTransform(native, ref value);
        }
    }
}
