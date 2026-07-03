using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper
{
    public static class EAXReverbResultsBindings
    {
        private const string DllName = "vaudionative";

        /// <summary>
        /// Returns a pointer to the relative direction Vector for the given emitter, or null if not set.
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEaxReverbGetRelativeDirection")]
        public static extern unsafe Vector* GetRelativeDirection(IntPtr eax, IntPtr emitter);

        /// <summary>
        /// Returns a pointer to the relative gain float for the given emitter, or null if not set.
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaEaxReverbGetRelativeGain")]
        public static extern unsafe float* GetRelativeGain(IntPtr eax, IntPtr emitter);
    }
}
