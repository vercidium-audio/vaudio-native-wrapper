using System;

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
        public float radius
        {
            get => CapsulePrimitiveBindings.GetRadius(native);
            set => CapsulePrimitiveBindings.SetRadius(native, value).ThrowIfError();
        }

        /// <summary>Length of the body between the semicircular caps. The total length of the capsule is length + 2 * radius</summary>
        public float length
        {
            get => CapsulePrimitiveBindings.GetLength(native);
            set => CapsulePrimitiveBindings.SetLength(native, value).ThrowIfError();
        }

        /// <summary>Rotation of the capsule in radians</summary>
        public float rotation
        {
            get => CapsulePrimitiveBindings.GetRotation(native);
            set => CapsulePrimitiveBindings.SetRotation(native, value).ThrowIfError();
        }

        /// <summary>When true, rays lose a flat percentage of energy the moment they touch this capsule, instead of calculating how long the ray spent inside it</summary>
        public bool UseFlatTransmission
        {
            get => CapsulePrimitiveBindings.GetUseFlatTransmission(native);
            set => CapsulePrimitiveBindings.SetUseFlatTransmission(native, value).ThrowIfError();
        }

        protected override VAResult DestroyNative(IntPtr native) => CapsulePrimitiveBindings.Destroy(native);

        protected override string DebugInfo => $"material={material}, center={center}, radius={radius}, length={length}, rotation={rotation}";
    }
}
