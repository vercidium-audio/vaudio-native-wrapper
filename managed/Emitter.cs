using System;

namespace vaudionativewrapper.managed
{
    public unsafe class Emitter
    {
        public IntPtr native;

        public Emitter(IntPtr native)
        {
            this.native = native;
        }
    }
}
