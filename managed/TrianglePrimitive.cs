namespace vaudionativewrapper.managed
{
    public class TrianglePrimitive : Primitive
    {
        public TrianglePrimitive()
        {
            native = TrianglePrimitiveBindings.Create();
        }

        public MaterialType material
        {
            get => TrianglePrimitiveBindings.GetMaterial(native);
            set => TrianglePrimitiveBindings.SetMaterial(native, value);
        }

        public Vector position0
        {
            get => TrianglePrimitiveBindings.GetPosition0(native);
            set => TrianglePrimitiveBindings.SetPosition0(native, value);
        }

        public Vector position1
        {
            get => TrianglePrimitiveBindings.GetPosition1(native);
            set => TrianglePrimitiveBindings.SetPosition1(native, value);
        }

        public Vector position2
        {
            get => TrianglePrimitiveBindings.GetPosition2(native);
            set => TrianglePrimitiveBindings.SetPosition2(native, value);
        }
    }
}
