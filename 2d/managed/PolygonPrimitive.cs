using System;

namespace vaudionativewrapper.managed
{
    /// <summary>A polygon primitive constructed from a <see cref="Polygon"/>, which can be shared across multiple primitives</summary>
    public unsafe class PolygonPrimitive : Primitive
    {
        Polygon _polygon;

        /// <summary>Create a polygon primitive. The polygon must not be destroyed until every primitive using it has been destroyed</summary>
        public PolygonPrimitive(Polygon polygon)
        {
            if (polygon == null)
                throw new ArgumentNullException(nameof(polygon));

            IntPtr outPrimitive;
            PolygonPrimitiveBindings.Create(polygon.native, &outPrimitive).ThrowIfError();

            native = outPrimitive;
            owns = true;
            _polygon = polygon;
        }

        /// <summary>Shared local-space points, before rotation, scale and position are applied</summary>
        public Polygon polygon
        {
            get => _polygon;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(polygon));

                PolygonPrimitiveBindings.SetPolygon(native, value.native).ThrowIfError();

                _polygon = value;
            }
        }

        /// <summary>Position of the polygon in world space</summary>
        public Vector position
        {
            get => PolygonPrimitiveBindings.GetPosition(native);
            set => PolygonPrimitiveBindings.SetPosition(native, value).ThrowIfError();
        }

        /// <summary>Rotation of the polygon in radians</summary>
        public float rotation
        {
            get => PolygonPrimitiveBindings.GetRotation(native);
            set => PolygonPrimitiveBindings.SetRotation(native, value).ThrowIfError();
        }

        /// <summary>Scale applied to the local-space points, before rotation and position</summary>
        public Vector scale
        {
            get => PolygonPrimitiveBindings.GetScale(native);
            set => PolygonPrimitiveBindings.SetScale(native, value).ThrowIfError();
        }

        /// <summary>Whether the last point should connect back to the first</summary>
        public bool enclosed
        {
            get => PolygonPrimitiveBindings.GetEnclosed(native);
            set => PolygonPrimitiveBindings.SetEnclosed(native, value).ThrowIfError();
        }

        protected override VAResult DestroyNative(IntPtr native) => PolygonPrimitiveBindings.Destroy(native);

        protected override string DebugInfo => $"material={material}, position={position}, rotation={rotation}, scale={scale}";
    }
}
