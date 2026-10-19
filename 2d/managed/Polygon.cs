using System;
using System.Collections.Generic;

namespace vaudionativewrapper.managed
{
    /// <summary>An immutable set of local-space points that can be shared across multiple PolygonPrimitive instances</summary>
    public unsafe class Polygon
    {
        public IntPtr native;

        /// <summary>Create a polygon from an array of points. The points are copied, so later changes to the array have no effect</summary>
        public Polygon(Vector[] points)
        {
            IntPtr outPolygon;

            fixed (Vector* ptr = points)
                PolygonBindings.Create(ptr, points.Length, &outPolygon).ThrowIfError();

            native = outPolygon;
        }

        // Wraps a polygon owned by a PolygonPrimitive, which frees it natively
        internal Polygon(IntPtr native)
        {
            this.native = native;
            ownedByPrimitive = true;
        }

        readonly bool ownedByPrimitive;

        /// <summary>Create a polygon from a list of points. The points are copied, so later changes to the list have no effect</summary>
        public Polygon(List<Vector> points) : this(points.ToArray()) { }

        /// <summary>Local-space points, before rotation, scale and position are applied</summary>
        public Vector[] points
        {
            get
            {
                int count = PolygonBindings.GetPointCount(native);
                var result = new Vector[count];

                for (int i = 0; i < count; i++)
                    result[i] = PolygonBindings.GetPoint(native, i);

                return result;
            }
        }

        /// <summary>Destroys the native polygon. Returns <see cref="VAResult.ErrorInUse"/> if a PolygonPrimitive using this polygon has not been destroyed yet, in which case Destroy can be called again later</summary>
        public VAResult Destroy()
        {
            if (native == IntPtr.Zero || ownedByPrimitive)
                return VAResult.Success;

            var result = PolygonBindings.Destroy(native);

            if (result == VAResult.Success)
                native = IntPtr.Zero;

            return result;
        }

        ~Polygon()
        {
            if (native != IntPtr.Zero && !ownedByPrimitive)
                LogSettings.Warn("Polygon was garbage collected without calling Destroy() first.");
        }
    }
}
