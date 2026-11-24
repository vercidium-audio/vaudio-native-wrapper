// Human-created

using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper.managed
{
    /// <summary>Settings that control how rays lose energy over distance</summary>
    public class AirAbsorptionSettings
    {
        public IntPtr native;

#if DEBUG
        private readonly bool owns;
        string stackTrace;
#endif

        /// <summary>Create a new AirAbsorptionSettings with default settings</summary>
        public AirAbsorptionSettings()
        {
            native = AirAbsorptionSettingsBindings.Create();
#if DEBUG
            owns = true;
            stackTrace = Environment.StackTrace;
#endif
        }

        public AirAbsorptionSettings(IntPtr native)
        {
            this.native = native;
#if DEBUG
            stackTrace = Environment.StackTrace;
#endif
        }

#if DEBUG
        ~AirAbsorptionSettings()
        {
            if (owns && native != IntPtr.Zero)
                LogSettings.Warn($"AirAbsorptionSettings was garbage collected without calling Destroy() first. Stack trace: {stackTrace}");
        }
#endif

        /// <summary>Relative humidity as a percentage</summary>
        public float Humidity
        {
            get => AirAbsorptionSettingsBindings.GetHumidity(native);
            set => AirAbsorptionSettingsBindings.SetHumidity(native, value).ThrowIfError();
        }

        /// <summary>Air temperature in degrees Celsius</summary>
        public float Temperature
        {
            get => AirAbsorptionSettingsBindings.GetTemperature(native);
            set => AirAbsorptionSettingsBindings.SetTemperature(native, value).ThrowIfError();
        }

        /// <summary>Atmospheric pressure in Pascals</summary>
        public float Pressure
        {
            get => AirAbsorptionSettingsBindings.GetPressure(native);
            set => AirAbsorptionSettingsBindings.SetPressure(native, value).ThrowIfError();
        }

        // TODO - remaining XML docs
        public VAResult Validate() => AirAbsorptionSettingsBindings.Validate(native);

        public VAResult Destroy()
        {
            var result = AirAbsorptionSettingsBindings.Destroy(native);

            if (result == VAResult.Success)
            {
                formulaLFHandlers.Remove(native);
                formulaHFHandlers.Remove(native);
            }

            native = IntPtr.Zero;
            return result;
        }

        // Handlers are keyed by the settings' native pointer, which is passed to native as the formula's userData, since the raytracing threads invoke the formulas on a per-frame copy of the settings. See CallbackTable
        static readonly CallbackTable<Func<float, float>> formulaLFHandlers = new CallbackTable<Func<float, float>>();
        static readonly CallbackTable<Func<float, float>> formulaHFHandlers = new CallbackTable<Func<float, float>>();

        static readonly AirAbsorptionFormulaDelegate formulaLFNative = FormulaLFNative;
        static readonly AirAbsorptionFormulaDelegate formulaHFNative = FormulaHFNative;

        // The handler is only missing if the settings were destroyed while a frame was in flight, so fall back to no energy loss
        [AOT.MonoPInvokeCallback(typeof(AirAbsorptionFormulaDelegate))]
        static float FormulaLFNative(float distance, IntPtr settings) => formulaLFHandlers.Get(settings)?.Invoke(distance) ?? 1;

        [AOT.MonoPInvokeCallback(typeof(AirAbsorptionFormulaDelegate))]
        static float FormulaHFNative(float distance, IntPtr settings) => formulaHFHandlers.Get(settings)?.Invoke(distance) ?? 1;

        // A cleared formula keeps its handler until the settings are destroyed, as a frame in flight may still invoke it
        AirAbsorptionFormulaDelegate SetCustomFormula(CallbackTable<Func<float, float>> handlers, Func<float, float> value, Func<IntPtr, AirAbsorptionFormulaDelegate, IntPtr, VAResult> set, AirAbsorptionFormulaDelegate trampoline)
        {
            if (value == null)
            {
                set(native, null, IntPtr.Zero).ThrowIfError();
                return null;
            }

            handlers.Set(native, value);
            set(native, trampoline, native).ThrowIfError();
            return trampoline;
        }

        /// <summary>Set a custom air absorption formula for low frequencies. Takes distance (meters) and returns energy loss as a percentage. Invoked on the raytracing threads. Pass null to use the default formula.</summary>
        public AirAbsorptionFormulaDelegate SetCustomFormulaLF(Func<float, float> value) => SetCustomFormula(formulaLFHandlers, value, AirAbsorptionSettingsBindings.SetCustomFormulaLF, formulaLFNative);

        /// <summary>Set a custom air absorption formula for high frequencies. Takes distance (meters) and returns energy loss as a percentage. Invoked on the raytracing threads. Pass null to use the default formula.</summary>
        public AirAbsorptionFormulaDelegate SetCustomFormulaHF(Func<float, float> value) => SetCustomFormula(formulaHFHandlers, value, AirAbsorptionSettingsBindings.SetCustomFormulaHF, formulaHFNative);
    }
}