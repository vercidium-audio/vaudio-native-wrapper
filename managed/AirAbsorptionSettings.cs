using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper.managed
{
    public class AirAbsorptionSettings
    {
        public IntPtr native;

        public AirAbsorptionSettings()
        {
            native = AirAbsorptionSettingsBindings.Create();
        }

        public AirAbsorptionSettings(IntPtr native)
        {
            this.native = native;
        }

        public float Humidity
        {
            get => AirAbsorptionSettingsBindings.GetHumidity(native);
            set => AirAbsorptionSettingsBindings.SetHumidity(native, value);
        }

        public float Temperature
        {
            get => AirAbsorptionSettingsBindings.GetTemperature(native);
            set => AirAbsorptionSettingsBindings.SetTemperature(native, value);
        }

        public float Pressure
        {
            get => AirAbsorptionSettingsBindings.GetPressure(native);
            set => AirAbsorptionSettingsBindings.SetPressure(native, value);
        }

        public AirAbsorptionFormulaDelegate SetCustomFormulaLF(Func<float, float> value)
        {
            if (value != null)
            {
                float callback(float distance) => value(distance);

                AirAbsorptionSettingsBindings.SetCustomFormulaLF(native, Marshal.GetFunctionPointerForDelegate((AirAbsorptionFormulaDelegate)callback));
                return callback;
            }
            else
            {
                AirAbsorptionSettingsBindings.SetCustomFormulaLF(native, IntPtr.Zero);
                return null;
            }
        }

        public AirAbsorptionFormulaDelegate SetCustomFormulaHF(Func<float, float> value)
        {
            if (value != null)
            {
                float callback(float distance) => value(distance);

                AirAbsorptionSettingsBindings.SetCustomFormulaHF(native, Marshal.GetFunctionPointerForDelegate((AirAbsorptionFormulaDelegate)callback));
                return callback;
            }
            else
            {
                AirAbsorptionSettingsBindings.SetCustomFormulaHF(native, IntPtr.Zero);
                return null;
            }
        }
    }
}