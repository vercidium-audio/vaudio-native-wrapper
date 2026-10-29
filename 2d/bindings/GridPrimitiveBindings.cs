using System;
using System.Runtime.InteropServices;

#if VA_DOUBLE_PRECISION
using VAFloat = System.Double;
#else
using VAFloat = System.Single;
#endif

namespace vaudionativewrapper
{
    public static class GridPrimitiveBindings
    {
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaGridPrimitiveCreate")]
        public static extern IntPtr Create(int width, int height);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaGridPrimitiveDestroy")]
        public static extern VAResult Destroy(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaGridPrimitiveGetSize")]
        public static extern VAResult GetSize(IntPtr primitive, out int width, out int height);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaGridPrimitiveGetCell")]
        public static extern MaterialType GetCell(IntPtr primitive, int x, int y);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaGridPrimitiveSetCell")]
        public static extern VAResult SetCell(IntPtr primitive, int x, int y, MaterialType material);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaGridPrimitiveSetDataDirty")]
        public static extern VAResult SetDataDirty(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaGridPrimitiveGetPosition")]
        public static extern Vector GetPosition(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaGridPrimitiveSetPosition")]
        public static extern VAResult SetPosition(IntPtr primitive, Vector position);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaGridPrimitiveGetRotation")]
        public static extern VAFloat GetRotation(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaGridPrimitiveSetRotation")]
        public static extern VAResult SetRotation(IntPtr primitive, VAFloat rotation);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaGridPrimitiveGetScale")]
        public static extern VAFloat GetScale(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaGridPrimitiveSetScale")]
        public static extern VAResult SetScale(IntPtr primitive, VAFloat scale);
    }
}
