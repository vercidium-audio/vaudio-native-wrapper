using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper
{
    public static class PathBindings
    {
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPathCreate")]
        public static extern unsafe VAResult Create([MarshalAs(UnmanagedType.LPStr)] string data, IntPtr* outPath);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPathDestroy")]
        public static extern VAResult Destroy(IntPtr path);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPathGetData")]
        private static extern IntPtr GetDataRaw(IntPtr path);
        public static string GetData(IntPtr path) => Marshal.PtrToStringAnsi(GetDataRaw(path));

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPathGetClosed")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool GetClosed(IntPtr path);
    }
}
