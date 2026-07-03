namespace vaudionativewrapper.managed
{
    public unsafe class PlanePrimitive : Primitive
    {
        public PlanePrimitive()
        {
            native = PlanePrimitiveBindings.Create();
        }

        public MaterialType material
        {
            get => PlanePrimitiveBindings.GetMaterial(native);
            set => PlanePrimitiveBindings.SetMaterial(native, value);
        }

        public float width
        {
            get => PlanePrimitiveBindings.GetWidth(native);
            set => PlanePrimitiveBindings.SetWidth(native, value);
        }

        public float height
        {
            get => PlanePrimitiveBindings.GetHeight(native);
            set => PlanePrimitiveBindings.SetHeight(native, value);
        }

        public Matrix transform
        {
            get => *PlanePrimitiveBindings.GetTransform(native);
            set => PlanePrimitiveBindings.SetTransform(native, ref value);
        }
    }
}
