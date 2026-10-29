using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace vaudionativewrapper.managed
{
    /// <summary>An immutable set of local-space points that can be shared across multiple PolygonPrimitive instances</summary>
    public class Polygon
    {
        public IntPtr native;

        /// <summary>Create a polygon from an array of points. The points are copied, so later changes to the array have no effect</summary>
        public Polygon(Vector[] points) : this(new ReadOnlySpan<Vector>(points)) { }

        /// <summary>Create a polygon from a list of points. The points are copied, so later changes to the list have no effect</summary>
        public Polygon(List<Vector> points) : this(points.ToArray()) { }

        /// <summary>Create a polygon from a span of points. The points are copied, so later changes to the span have no effect</summary>
        public Polygon(ReadOnlySpan<Vector> points)
        {
            PolygonBindings.Create(in MemoryMarshal.GetReference(points), points.Length, out native).ThrowIfError();
        }

        // Wraps a polygon owned by a PolygonPrimitive, which frees it natively
        internal Polygon(IntPtr native)
        {
            this.native = native;
            ownedByPrimitive = true;
        }

        readonly bool ownedByPrimitive;

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

        /// <summary>Destroy the native polygon. Returns <see cref="VAResult.ErrorInUse"/> if a PolygonPrimitive using this polygon has not been destroyed yet.</summary>
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
