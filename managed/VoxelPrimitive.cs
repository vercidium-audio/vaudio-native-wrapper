namespace vaudionativewrapper.managed
{
    public unsafe class VoxelPrimitive : Primitive
    {
        public readonly int width;
        public readonly int height;
        public readonly int depth;

        public VoxelPrimitive(int width, int height, int depth)
        {
            this.width = width;
            this.height = height;
            this.depth = depth;

            native = VoxelPrimitiveBindings.Create(width, height, depth);
        }

        public MaterialType material
        {
            get => VoxelPrimitiveBindings.GetMaterial(native);
            set => VoxelPrimitiveBindings.SetMaterial(native, value);
        }

        public float scale
        {
            get => VoxelPrimitiveBindings.GetScale(native);
            set => VoxelPrimitiveBindings.SetScale(native, value);
        }

        public Matrix transform
        {
            get => *VoxelPrimitiveBindings.GetTransform(native);
            set => VoxelPrimitiveBindings.SetTransform(native, ref value);
        }

        public MaterialType this[int x, int y, int z]
        {
            get => VoxelPrimitiveBindings.GetVoxel(native, x, y, z);
            set => VoxelPrimitiveBindings.SetVoxel(native, x, y, z, value);
        }

        public void SetDataDirty() => VoxelPrimitiveBindings.SetDataDirty(native);
    }
}