using System.Runtime.InteropServices;

namespace vaudionativewrapper
{
    public static class CustomEAXFormulasBindings
    {
        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCustomEaxFormulasCreate")]
        public static extern unsafe CustomEAXFormulas* Create();

        [DllImport(Constants.DLL_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCustomEaxFormulasFree")]
        public static extern unsafe void Free(CustomEAXFormulas* formulas);
    }
}
