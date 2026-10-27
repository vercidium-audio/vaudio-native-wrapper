using System;

#if VA_DOUBLE_PRECISION
using VAFloat = System.Double;
#else
using VAFloat = System.Single;
#endif

namespace vaudionativewrapper.managed
{
    /// <summary>A box primitive</summary>
    public class BoxPrimitive : Primitive
    {
        public BoxPrimitive()
        {
            native = BoxPrimitiveBindings.Create();
            owns = true;
        }

        /// <summary>Center of the box in world space</summary>
        public Vector position
        {
            get => BoxPrimitiveBindings.GetPosition(native);
            set => BoxPrimitiveBindings.SetPosition(native, value).ThrowIfError();
        }

        /// <summary>Size of the box</summary>
        public Vector size
        {
            get => BoxPrimitiveBindings.GetSize(native);
            set => BoxPrimitiveBindings.SetSize(native, value).ThrowIfError();
        }

        /// <summary>Rotation of the box in radians</summary>
        public VAFloat rotation
        {
            get => BoxPrimitiveBindings.GetRotation(native);
            set => BoxPrimitiveBindings.SetRotation(native, value).ThrowIfError();
        }

        protected override VAResult DestroyNative(IntPtr native) => BoxPrimitiveBindings.Destroy(native);

        protected override string DebugInfo => $"material={material}, position={position}, size={size}, rotation={rotation}";
    }
}
