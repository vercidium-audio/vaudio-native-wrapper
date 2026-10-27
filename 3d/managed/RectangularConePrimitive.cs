using System;

#if VA_DOUBLE_PRECISION
using VAFloat = System.Double;
#else
using VAFloat = System.Single;
#endif

namespace vaudionativewrapper.managed
{
    /// <summary>A cone primitive with a rectangular base</summary>
    public unsafe class RectangularConePrimitive : Primitive
    {
        public RectangularConePrimitive()
        {
            native = RectangularConePrimitiveBindings.Create();
            owns = true;
        }

        /// <summary>Width of the rectangular base</summary>
        public VAFloat width
        {
            get => RectangularConePrimitiveBindings.GetWidth(native);
            set => RectangularConePrimitiveBindings.SetWidth(native, value).ThrowIfError();
        }

        /// <summary>Length of the rectangular base</summary>
        public VAFloat length
        {
            get => RectangularConePrimitiveBindings.GetLength(native);
            set => RectangularConePrimitiveBindings.SetLength(native, value).ThrowIfError();
        }

        /// <summary>Height of the cone</summary>
        public VAFloat height
        {
            get => RectangularConePrimitiveBindings.GetHeight(native);
            set => RectangularConePrimitiveBindings.SetHeight(native, value).ThrowIfError();
        }

        /// <summary>Must only contain rotation and translation components, not scale</summary>
        public Matrix transform
        {
            get => *RectangularConePrimitiveBindings.GetTransform(native);
            set => RectangularConePrimitiveBindings.SetTransform(native, ref value).ThrowIfError();
        }

        protected override VAResult DestroyNative(IntPtr native) => RectangularConePrimitiveBindings.Destroy(native);

        protected override string DebugInfo => $"material={material}, width={width}, length={length}, height={height}";
    }
}
