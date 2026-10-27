using System.Runtime.InteropServices;

#if VA_DOUBLE_PRECISION
using VAFloat = System.Double;
#else
using VAFloat = System.Single;
#endif

namespace vaudionativewrapper
{
    /// <summary>A 3D vector with VAFloat components, single or double precision to match the native SDK build</summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector
    {
        /// <summary>Creates a vector with all components set to <paramref name="v"/></summary>
        public Vector(VAFloat v)
        {
            X = v;
            Y = v;
            Z = v;
        }

        /// <summary>Creates a vector with the specified components</summary>
        public Vector(VAFloat x, VAFloat y, VAFloat z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>The X component</summary>
        public VAFloat X;

        /// <summary>The Y component</summary>
        public VAFloat Y;
    
        /// <summary>The Z component</summary>
        public VAFloat Z;

        /// <inheritdoc/>
        public override string ToString() => $"({X:F3}, {Y:F4}, {Z:F4})";
    }
}
