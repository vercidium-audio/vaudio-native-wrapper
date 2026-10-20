using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper
{
    public static class PolygonPrimitiveBindings
    {
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPolygonPrimitiveCreate")]
        public static extern VAResult Create(in Vector points, int pointCount, out IntPtr outPrimitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPolygonPrimitiveCreateFromPolygon")]
        public static extern VAResult CreateFromPolygon(IntPtr polygon, out IntPtr outPrimitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPolygonPrimitiveSetPolygon")]
        public static extern VAResult SetPolygon(IntPtr primitive, IntPtr polygon);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPolygonPrimitiveGetPolygon")]
        public static extern IntPtr GetPolygon(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPolygonPrimitiveSetPosition")]
        public static extern VAResult SetPosition(IntPtr primitive, Vector position);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPolygonPrimitiveSetRotation")]
        public static extern VAResult SetRotation(IntPtr primitive, float rotation);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPolygonPrimitiveSetScale")]
        public static extern VAResult SetScale(IntPtr primitive, Vector scale);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPolygonPrimitiveGetPosition")]
        public static extern Vector GetPosition(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPolygonPrimitiveGetRotation")]
        public static extern float GetRotation(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPolygonPrimitiveGetScale")]
        public static extern Vector GetScale(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPolygonPrimitiveGetUseFlatTransmission")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool GetUseFlatTransmission(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPolygonPrimitiveSetUseFlatTransmission")]
        public static extern VAResult SetUseFlatTransmission(IntPtr primitive, bool useFlatTransmission);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPolygonPrimitiveGetEnclosed")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool GetEnclosed(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPolygonPrimitiveSetEnclosed")]
        public static extern VAResult SetEnclosed(IntPtr primitive, bool enclosed);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaPolygonPrimitiveDestroy")]
        public static extern VAResult Destroy(IntPtr primitive);
    }
}
