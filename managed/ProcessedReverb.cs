namespace vaudionativewrapper.managed
{
    public unsafe class ProcessedReverb
    {
        public vaudionativewrapper.ProcessedReverb* native;

        public ProcessedReverb(vaudionativewrapper.ProcessedReverb* native)
        {
            this.native = native;
        }

        public float ReturnedPercent => native->returnedPercent;

        public float OutsidePercent => native->outsidePercent;

        public float MeasuredDecayTimeLF => native->measuredDecayTimeLF;

        public float MeasuredDecayTimeHF => native->measuredDecayTimeHF;

        public float MaterialRoughness => native->materialRoughness;

        public float MaterialAbsorptionLF => native->materialAbsorptionLF;

        public float MaterialAbsorptionHF => native->materialAbsorptionHF;

        public float GetMaterialAbsorption() => ProcessedReverbBindings.GetMaterialAbsorption(native);
    }
}
