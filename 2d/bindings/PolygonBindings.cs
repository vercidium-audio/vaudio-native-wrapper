using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper
{
    public static class PolygonBindings
    {
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPolygonCreate")]
        public static extern unsafe VAResult Create(Vector* points, int pointCount, IntPtr* outPolygon);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPolygonDestroy")]
        public static extern VAResult Destroy(IntPtr polygon);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPolygonGetPointCount")]
        public static extern int GetPointCount(IntPtr polygon);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPolygonGetPoint")]
        public static extern Vector GetPoint(IntPtr polygon, int index);
    }
}
