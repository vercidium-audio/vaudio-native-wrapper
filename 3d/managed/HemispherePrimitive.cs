using System;

#if VA_DOUBLE_PRECISION
using VAFloat = System.Double;
#else
using VAFloat = System.Single;
#endif

namespace vaudionativewrapper.managed
{
    /// <summary>A hemisphere primitive</summary>
    public unsafe class HemispherePrimitive : Primitive
    {
        public HemispherePrimitive()
        {
            native = HemispherePrimitiveBindings.Create();
            owns = true;
        }

        /// <summary>Radius of the hemisphere</summary>
        public VAFloat radius
        {
            get => HemispherePrimitiveBindings.GetRadius(native);
            set => HemispherePrimitiveBindings.SetRadius(native, value).ThrowIfError();
        }

        /// <summary>Must only contain rotation and translation components, not scale</summary>
        public Matrix transform
        {
            get => *HemispherePrimitiveBindings.GetTransform(native);
            set => HemispherePrimitiveBindings.SetTransform(native, ref value).ThrowIfError();
        }

        protected override VAResult DestroyNative(IntPtr native) => HemispherePrimitiveBindings.Destroy(native);

        protected override string DebugInfo => $"material={material}, radius={radius}";
    }
}
