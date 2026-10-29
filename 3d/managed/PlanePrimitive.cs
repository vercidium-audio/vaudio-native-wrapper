using System;

#if VA_DOUBLE_PRECISION
using VAFloat = System.Double;
#else
using VAFloat = System.Single;
#endif

namespace vaudionativewrapper.managed
{
    /// <summary>A flat rectangular plane primitive</summary>
    public unsafe class PlanePrimitive : Primitive
    {
        /// <summary>Create a plane primitive</summary>
        public PlanePrimitive()
        {
            native = PlanePrimitiveBindings.Create();
            owns = true;
        }

        /// <summary>Width of the plane</summary>
        public VAFloat width
        {
            get => PlanePrimitiveBindings.GetWidth(native);
            set => PlanePrimitiveBindings.SetWidth(native, value).ThrowIfError();
        }

        /// <summary>Height of the plane</summary>
        public VAFloat height
        {
            get => PlanePrimitiveBindings.GetHeight(native);
            set => PlanePrimitiveBindings.SetHeight(native, value).ThrowIfError();
        }

        /// <summary>Must only contain rotation and translation components, not scale</summary>
        public Matrix transform
        {
            get => *PlanePrimitiveBindings.GetTransform(native);
            set => PlanePrimitiveBindings.SetTransform(native, ref value).ThrowIfError();
        }

        protected override VAResult DestroyNative(IntPtr native) => PlanePrimitiveBindings.Destroy(native);

        protected override string DebugInfo => $"material={material}, width={width}, height={height}";
    }
}
