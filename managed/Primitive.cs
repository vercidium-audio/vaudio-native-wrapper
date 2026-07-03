using System;

namespace vaudionativewrapper.managed
{
    public unsafe class Primitive
    {
        public IntPtr native;

        public Primitive(IntPtr native)
        {
            this.native = native;
        }
    }
}
