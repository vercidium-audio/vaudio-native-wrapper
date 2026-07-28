using System;

namespace vaudionativewrapper.managed
{
    public unsafe class EAXReverb
    {
        public vaudionativewrapper.EAXReverb* native;

        public EAXReverb(vaudionativewrapper.EAXReverb* native)
        {
            this.native = native;
        }

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

        /// <summary>
        /// Computes a similarity score between this reverb preset and another, in the range [0, 1] where 1 means identical.
        /// </summary>
        public float GetSimilarity(EAXReverb other)
        {
            return EAXUtilsBindings.GetSimilarity(native, other.native);
        }

        /// <summary>
        /// Gets the number of seconds this reverb's tail remains audible after the emitter stops emitting, used to delay removal from the world.
        /// maxVolume is the loudest linear volume (0-1) the emitter's dry source is ever played at (see Emitter.GetMaxVolume).
        /// </summary>
        public float GetEffectiveTailSeconds(float maxVolume)
        {
            return EAXUtilsBindings.GetEffectiveTailSeconds(native, maxVolume);
        }

        /// <summary>
        /// Finds the candidate most similar to target.
        /// Returns the best match, or null if target or candidates is null/empty.
        /// </summary>
        public static EAXReverb FindBestMatch(EAXReverb target, EAXReverb[] candidates)
        {
            if (target == null || candidates == null || candidates.Length == 0)
                return null;

            var nativeCandidates = stackalloc vaudionativewrapper.EAXReverb*[candidates.Length];

            for (int i = 0; i < candidates.Length; i++)
                nativeCandidates[i] = candidates[i].native;

            vaudionativewrapper.EAXReverb* outBest;
            int index = EAXUtilsBindings.FindBestMatch(target.native, nativeCandidates, candidates.Length, &outBest);

            if (index < 0 || outBest == null)
                return null;

            return candidates[index];
        }
    }
}