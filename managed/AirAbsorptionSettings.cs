using System;
using System.Runtime.InteropServices;

namespace vaudionativewrapper.managed
{
    public class AirAbsorptionSettings
    {
        public IntPtr native;

        // Native holds a raw function pointer into these delegates, invoked from native worker
        // threads that the CLR doesn't scan the same way as managed call stacks. A managed field
        // reference alone isn't a reliable guarantee against collection for this pattern, so pin
        // them explicitly for as long as native might call back into them.
        GCHandle lfHandle;
        GCHandle hfHandle;

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
            if (lfHandle.IsAllocated)
                lfHandle.Free();

            if (value != null)
            {
                float callback(float distance) => value(distance);
                var del = (AirAbsorptionFormulaDelegate)callback;
                lfHandle = GCHandle.Alloc(del);

                AirAbsorptionSettingsBindings.SetCustomFormulaLF(native, Marshal.GetFunctionPointerForDelegate(del));
                return del;
            }
            else
            {
                AirAbsorptionSettingsBindings.SetCustomFormulaLF(native, IntPtr.Zero);
                return null;
            }
        }

        public AirAbsorptionFormulaDelegate SetCustomFormulaHF(Func<float, float> value)
        {
            if (hfHandle.IsAllocated)
                hfHandle.Free();

            if (value != null)
            {
                float callback(float distance) => value(distance);
                var del = (AirAbsorptionFormulaDelegate)callback;
                hfHandle = GCHandle.Alloc(del);

                AirAbsorptionSettingsBindings.SetCustomFormulaHF(native, Marshal.GetFunctionPointerForDelegate(del));
                return del;
            }
            else
            {
                AirAbsorptionSettingsBindings.SetCustomFormulaHF(native, IntPtr.Zero);
                return null;
            }
        }
    }
}