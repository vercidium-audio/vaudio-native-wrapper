using System;

#if VA_DOUBLE_PRECISION
using VAFloat = System.Double;
#else
using VAFloat = System.Single;
#endif

namespace vaudionativewrapper.managed
{
    /// <summary>A capsule primitive (a rectangle with semicircular caps), aligned along its local Y axis</summary>
    public class CapsulePrimitive : Primitive
    {
        public CapsulePrimitive()
        {
            native = CapsulePrimitiveBindings.Create();
            owns = true;
        }

        /// <summary>Center of the capsule in world space</summary>
        public Vector center
        {
            get => CapsulePrimitiveBindings.GetCenter(native);
            set => CapsulePrimitiveBindings.SetCenter(native, value).ThrowIfError();
        }

        /// <summary>Radius of the capsule</summary>
        public VAFloat radius
        {
            get => CapsulePrimitiveBindings.GetRadius(native);
            set => CapsulePrimitiveBindings.SetRadius(native, value).ThrowIfError();
        }

        /// <summary>Length of the body between the semicircular caps. The total length of the capsule is length + 2 * radius</summary>
        public VAFloat length
        {
            get => CapsulePrimitiveBindings.GetLength(native);
            set => CapsulePrimitiveBindings.SetLength(native, value).ThrowIfError();
        }

        /// <summary>Rotation of the capsule in radians</summary>
        public VAFloat rotation
        {
            get => CapsulePrimitiveBindings.GetRotation(native);
            set => CapsulePrimitiveBindings.SetRotation(native, value).ThrowIfError();
        }

        protected override VAResult DestroyNative(IntPtr native) => CapsulePrimitiveBindings.Destroy(native);

        protected override string DebugInfo => $"material={material}, center={center}, radius={radius}, length={length}, rotation={rotation}";
    }
}
