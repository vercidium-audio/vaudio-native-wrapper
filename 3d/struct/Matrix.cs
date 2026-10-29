using System.Runtime.InteropServices;

#if VA_DOUBLE_PRECISION
using VAFloat = System.Double;
#else
using VAFloat = System.Single;
#endif

namespace vaudionativewrapper
{
    /// <summary>A 4x4 VAFloat matrix, single or double precision to match the native SDK build, laid out to match GLSL expectations</summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Matrix
    {
        /// <summary>Initializes a new matrix with all 16 elements in column-major order (each group of 4 is one column)</summary>
        public Matrix(VAFloat m11, VAFloat m12, VAFloat m13, VAFloat m14,
                      VAFloat m21, VAFloat m22, VAFloat m23, VAFloat m24,
                      VAFloat m31, VAFloat m32, VAFloat m33, VAFloat m34,
                      VAFloat m41, VAFloat m42, VAFloat m43, VAFloat m44)
        {
            M11 = m11; M12 = m12; M13 = m13; M14 = m14;
            M21 = m21; M22 = m22; M23 = m23; M24 = m24;
            M31 = m31; M32 = m32; M33 = m33; M34 = m34;
            M41 = m41; M42 = m42; M43 = m43; M44 = m44;
        }

        public VAFloat M11;
        public VAFloat M12;
        public VAFloat M13;
        public VAFloat M14;
        public VAFloat M21;
        public VAFloat M22;
        public VAFloat M23;
        public VAFloat M24;
        public VAFloat M31;
        public VAFloat M32;
        public VAFloat M33;
        public VAFloat M34;
        public VAFloat M41;
        public VAFloat M42;
        public VAFloat M43;
        public VAFloat M44;
    }
}
