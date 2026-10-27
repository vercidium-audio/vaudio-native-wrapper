using System;
using System.Runtime.InteropServices;

#if VA_DOUBLE_PRECISION
using VAFloat = System.Double;
#else
using VAFloat = System.Single;
#endif

namespace vaudionativewrapper
{
    public static class VersionBindings
    {
        /// <summary>Get the major, minor and patch version numbers of this SDK.</summary>
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaGetVersion")]
        public static extern void GetVersion(out int major, out int minor, out int patch);

        /// <summary>Get whether this is a production build. Returns false in dev/debug builds.</summary>
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaIsProduction")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool IsProduction();

        /// <summary>Get the size of VAFloat in the loaded native SDK: 4 for single precision, 8 for double precision.</summary>
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaGetFloatSize")]
        public static extern int GetFloatSize();

        static bool floatSizeChecked;

        /// <summary>Throws if the loaded native SDK was built with a different VAFloat precision than this wrapper. The double precision native SDK must be paired with the -double wrapper.</summary>
        public static void EnsureFloatSizeMatches()
        {
            if (floatSizeChecked)
                return;

            int nativeSize = GetFloatSize();
            if (nativeSize != sizeof(VAFloat))
                throw new InvalidOperationException($"The loaded {Constants.DLL_NAME} library uses {nativeSize}-byte VAFloats, but this wrapper was built for {sizeof(VAFloat)}-byte VAFloats. Use the {(nativeSize == 8 ? "double" : "single")} precision wrapper with this library.");

            floatSizeChecked = true;
        }
    }
}
