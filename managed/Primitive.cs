using System;

namespace vaudionativewrapper.managed
{
    /// <summary>A 3D primitive that rays collide with.</summary>
    public unsafe class Primitive
    {
        public IntPtr native;

        public Primitive(IntPtr native)
        {
            this.native = native;
        }

        public Primitive() { }
    }
}
