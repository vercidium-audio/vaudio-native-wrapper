using System;
using System.Runtime.InteropServices;

#if VA_DOUBLE_PRECISION
using VAFloat = System.Double;
#else
using VAFloat = System.Single;
#endif

namespace vaudionativewrapper
{
    public static class HeightmapPrimitiveBindings
    {
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaHeightmapPrimitiveCreate")]
        public static extern unsafe VAResult Create(VAFloat* heights, int sizeX, int sizeY, out IntPtr outPrimitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaHeightmapPrimitiveCreateWithHoles")]
        public static extern unsafe VAResult CreateWithHoles(VAFloat* heights, bool* holes, int sizeX, int sizeY, out IntPtr outPrimitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaHeightmapPrimitiveDestroy")]
        public static extern VAResult Destroy(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaHeightmapPrimitiveSetScale")]
        public static extern VAResult SetScale(IntPtr primitive, Vector scale);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaHeightmapPrimitiveGetScale")]
        public static extern Vector GetScale(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaHeightmapPrimitiveSetTransform")]
        public static extern VAResult SetTransform(IntPtr primitive, ref Matrix transform);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaHeightmapPrimitiveGetTransform")]
        public static extern unsafe Matrix* GetTransform(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaHeightmapPrimitiveGetSizeX")]
        public static extern int GetSizeX(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaHeightmapPrimitiveGetSizeY")]
        public static extern int GetSizeY(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaHeightmapPrimitiveGetHeights")]
        public static extern unsafe VAFloat* GetHeights(IntPtr primitive);

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaHeightmapPrimitiveGetHoles")]
        public static extern unsafe bool* GetHoles(IntPtr primitive);
    }
}
