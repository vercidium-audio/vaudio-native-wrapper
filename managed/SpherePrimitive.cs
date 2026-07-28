namespace vaudionativewrapper.managed
{
    public class SpherePrimitive : Primitive
    {
        public SpherePrimitive()
        {
            native = SpherePrimitiveBindings.Create();
        }

        public MaterialType material
        {
            get => SpherePrimitiveBindings.GetMaterial(native);
            set => SpherePrimitiveBindings.SetMaterial(native, value).ThrowIfError();
        }

        public Vector center
        {
            get => SpherePrimitiveBindings.GetCenter(native);
            set => SpherePrimitiveBindings.SetCenter(native, value).ThrowIfError();
        }

        public float radius
        {
            get => SpherePrimitiveBindings.GetRadius(native);
            set => SpherePrimitiveBindings.SetRadius(native, value).ThrowIfError();
        }

        public void Destroy()
        {
            SpherePrimitiveBindings.Destroy(native).ThrowIfError();
        }
    }
}
