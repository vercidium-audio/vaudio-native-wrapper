using System;

#if VA_DOUBLE_PRECISION
using VAFloat = System.Double;
#else
using VAFloat = System.Single;
#endif

namespace vaudionativewrapper.managed
{
    /// <summary>A cylinder primitive</summary>
    public unsafe class CylinderPrimitive : Primitive
    {
        public CylinderPrimitive()
        {
            native = CylinderPrimitiveBindings.Create();
            owns = true;
        }

        /// <summary>Radius of the cylinder</summary>
        public VAFloat radius
        {
            get => CylinderPrimitiveBindings.GetRadius(native);
            set => CylinderPrimitiveBindings.SetRadius(native, value).ThrowIfError();
        }

        /// <summary>Length of the cylinder body</summary>
        public VAFloat length
        {
            get => CylinderPrimitiveBindings.GetLength(native);
            set => CylinderPrimitiveBindings.SetLength(native, value).ThrowIfError();
        }

        /// <summary>Must only contain rotation and translation components, not scale</summary>
        public Matrix transform
        {
            get => *CylinderPrimitiveBindings.GetTransform(native);
            set => CylinderPrimitiveBindings.SetTransform(native, ref value).ThrowIfError();
        }

        protected override VAResult DestroyNative(IntPtr native) => CylinderPrimitiveBindings.Destroy(native);

        protected override string DebugInfo => $"material={material}, radius={radius}, length={length}";
    }
}
