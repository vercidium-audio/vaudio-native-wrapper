// Human-created

using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper.managed
{
    /// <summary>Settings that control how rays lose energy over distance</summary>
    public class AirAbsorptionSettings
    {
        public IntPtr native;

        // Must keep these to prevent garbage collection
        GCHandle handleLF;
        GCHandle handleHF;

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
            native = IntPtr.Zero;
            return result;
        }

        public AirAbsorptionFormulaDelegate SetCustomFormulaLF(Func<float, float> value)
        {
            if (handleLF.IsAllocated)
                handleLF.Free();

            if (value != null)
            {
                float callback(float distance) => value(distance);
                var del = (AirAbsorptionFormulaDelegate)callback;
                handleLF = GCHandle.Alloc(del);

                AirAbsorptionSettingsBindings.SetCustomFormulaLF(native, Marshal.GetFunctionPointerForDelegate(del)).ThrowIfError();
                return del;
            }
            else
            {
                AirAbsorptionSettingsBindings.SetCustomFormulaLF(native, IntPtr.Zero).ThrowIfError();
                return null;
            }
        }

        public AirAbsorptionFormulaDelegate SetCustomFormulaHF(Func<float, float> value)
        {
            if (handleHF.IsAllocated)
                handleHF.Free();

            if (value != null)
            {
                float callback(float distance) => value(distance);
                var del = (AirAbsorptionFormulaDelegate)callback;
                handleHF = GCHandle.Alloc(del);

                AirAbsorptionSettingsBindings.SetCustomFormulaHF(native, Marshal.GetFunctionPointerForDelegate(del)).ThrowIfError();
                return del;
            }
            else
            {
                AirAbsorptionSettingsBindings.SetCustomFormulaHF(native, IntPtr.Zero).ThrowIfError();
                return null;
            }
        }
    }
}