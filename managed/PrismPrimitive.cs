namespace vaudionativewrapper.managed
{
    /// <summary>A rectangular prism (box) audio primitive</summary>
    public unsafe class PrismPrimitive : Primitive
    {
        public PrismPrimitive()
        {
            native = PrismPrimitiveBindings.Create();
        }

        /// <summary>Determines the amount of energy lost when rays bounce off this primitive, permeate through it, and scatter off it</summary>
        public MaterialType material
        {
            get => PrismPrimitiveBindings.GetMaterial(native);
            set => PrismPrimitiveBindings.SetMaterial(native, value).ThrowIfError();
        }

        /// <summary>Dimensions of the prism along each axis</summary>
        public Vector size
        {
            get => PrismPrimitiveBindings.GetSize(native);
            set => PrismPrimitiveBindings.SetSize(native, value).ThrowIfError();
        }

        /// <summary>Must only contain rotation and translation components, not scale</summary>
        public Matrix transform
        {
            get => *PrismPrimitiveBindings.GetTransform(native);
            set => PrismPrimitiveBindings.SetTransform(native, ref value).ThrowIfError();
        }

        public void Destroy()
        {
            PrismPrimitiveBindings.Destroy(native).ThrowIfError();
        }
    }
}
