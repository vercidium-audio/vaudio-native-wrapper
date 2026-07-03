using System.Runtime.InteropServices;

namespace vaudionativewrapper
{
    public static class CustomEAXFormulasBindings
    {
        private const string DllName = "vaudionative";

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCustomEaxFormulasCreate")]
        public static extern unsafe CustomEAXFormulas* Create();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "vaCustomEaxFormulasFree")]
        public static extern unsafe void Free(CustomEAXFormulas* formulas);
    }
}
