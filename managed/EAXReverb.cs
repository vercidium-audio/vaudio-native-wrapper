using System;

namespace vaudionativewrapper.managed
{
    public unsafe class EAXReverb
    {
        public vaudionativewrapper.EAXReverb* native;

        public float ReflectionsDelay
        {
            get => native->reflectionsDelay;
            set => native->reflectionsDelay = value;
        }

        public float Density
        {
            get => native->density;
            set => native->density = value;
        }

        public float Diffusion
        {
            get => native->diffusion;
            set => native->diffusion = value;
        }

        public float GainLF
        {
            get => native->gainLF;
            set => native->gainLF = value;
        }

        public float GainHF
        {
            get => native->gainHF;
            set => native->gainHF = value;
        }

        public float Gain
        {
            get => native->gain;
            set => native->gain = value;
        }

        public float DecayTime
        {
            get => native->decayTime;
            set => native->decayTime = value;
        }

        public float DecayLFRatio
        {
            get => native->decayLFRatio;
            set => native->decayLFRatio = value;
        }

        public float DecayHFRatio
        {
            get => native->decayHFRatio;
            set => native->decayHFRatio = value;
        }

        public float ReflectionsGain
        {
            get => native->reflectionsGain;
            set => native->reflectionsGain = value;
        }

        public float LateReverbGain
        {
            get => native->lateReverbGain;
            set => native->lateReverbGain = value;
        }

        public float LateReverbDelay
        {
            get => native->lateReverbDelay;
            set => native->lateReverbDelay = value;
        }

        public float EchoTime
        {
            get => native->echoTime;
            set => native->echoTime = value;
        }

        public float EchoDepth
        {
            get => native->echoDepth;
            set => native->echoDepth = value;
        }

        public float ModulationTime
        {
            get => native->modulationTime;
            set => native->modulationTime = value;
        }

        public float ModulationDepth
        {
            get => native->modulationDepth;
            set => native->modulationDepth = value;
        }

        public float AirAbsorptionGainHF
        {
            get => native->airAbsorptionGainHF;
            set => native->airAbsorptionGainHF = value;
        }

        public float HFReference
        {
            get => native->hfReference;
            set => native->hfReference = value;
        }

        public float LFReference
        {
            get => native->lfReference;
            set => native->lfReference = value;
        }

        public float RoomRolloffFactor
        {
            get => native->roomRolloffFactor;
            set => native->roomRolloffFactor = value;
        }

        public int DecayHFLimit
        {
            get => native->decayHFLimit;
            set => native->decayHFLimit = value;
        }

        public Vector? GetRelativeDirection(Emitter emitter)
        {
            var ptr = EAXReverbResultsBindings.GetRelativeDirection((IntPtr)native, emitter.native);

            if (ptr != null)
                return *ptr;

            return null;
        }

        public float? GetRelativeGain(Emitter emitter)
        {
            var ptr = EAXReverbResultsBindings.GetRelativeGain((IntPtr)native, emitter.native);

            if (ptr != null)
                return *ptr;

            return null;
        }
    }
}