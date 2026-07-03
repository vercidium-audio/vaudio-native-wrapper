using System;

namespace vaudionativewrapper.managed
{
    public class MaterialProperties
    {
        private IntPtr world;
        private int id;

        public MaterialProperties(IntPtr ctx, int id)
        {
            world = ctx;
            this.id = id;
        }

        public float AbsorptionLF
        {
            get => WorldBindings.GetMaterialAbsorptionLF(world, id);
            set => WorldBindings.SetMaterialAbsorptionLF(world, id, value);
        }

        public float AbsorptionHF
        {
            get => WorldBindings.GetMaterialAbsorptionHF(world, id);
            set => WorldBindings.SetMaterialAbsorptionHF(world, id, value);
        }

        public float Scattering
        {
            get => WorldBindings.GetMaterialScattering(world, id);
            set => WorldBindings.SetMaterialScattering(world, id, value);
        }

        public float TransmissionLF
        {
            get => WorldBindings.GetMaterialTransmissionLF(world, id);
            set => WorldBindings.SetMaterialTransmissionLF(world, id, value);
        }

        public float TransmissionHF
        {
            get => WorldBindings.GetMaterialTransmissionHF(world, id);
            set => WorldBindings.SetMaterialTransmissionHF(world, id, value);
        }

        public float PlaneTransmissionLF
        {
            get => WorldBindings.GetMaterialPlaneTransmissionLF(world, id);
            set => WorldBindings.SetMaterialPlaneTransmissionLF(world, id, value);
        }

        public float PlaneTransmissionHF
        {
            get => WorldBindings.GetMaterialPlaneTransmissionHF(world, id);
            set => WorldBindings.SetMaterialPlaneTransmissionHF(world, id, value);
        }
    }
}