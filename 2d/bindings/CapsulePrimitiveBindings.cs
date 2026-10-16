using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper
{
    public static class CapsulePrimitiveBindings
    {
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCapsulePrimitiveCreate")]
        public static extern IntPtr Create();

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCapsulePrimitiveSetCenter")]
        public static extern VAResult SetCenter(IntPtr primitive, Vector center);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCapsulePrimitiveSetRadius")]
        public static extern VAResult SetRadius(IntPtr primitive, float radius);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCapsulePrimitiveSetLength")]
        public static extern VAResult SetLength(IntPtr primitive, float length);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCapsulePrimitiveSetRotation")]
        public static extern VAResult SetRotation(IntPtr primitive, float rotation);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCapsulePrimitiveGetCenter")]
        public static extern Vector GetCenter(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCapsulePrimitiveGetRadius")]
        public static extern float GetRadius(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCapsulePrimitiveGetLength")]
        public static extern float GetLength(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCapsulePrimitiveGetRotation")]
        public static extern float GetRotation(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCapsulePrimitiveGetUseFlatTransmission")]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool GetUseFlatTransmission(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCapsulePrimitiveSetUseFlatTransmission")]
        public static extern VAResult SetUseFlatTransmission(IntPtr primitive, bool useFlatTransmission);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCapsulePrimitiveDestroy")]
        public static extern VAResult Destroy(IntPtr primitive);
    }
}
