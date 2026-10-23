using System;

namespace vaudionativewrapper.managed
{
    /// <summary>An audio primitive defined by a grid of heights, e.g. terrain. Sample (x, y) is at local position (x * scale.X, height * scale.Y, y * scale.Z), so the grid starts at the local origin and extends along +X and +Z, with heights along +Y. Always uses flat transmission and is two-sided.</summary>
    public unsafe class HeightmapPrimitive : Primitive
    {
        /// <summary>Create a heightmap primitive. heights is sizeX * sizeY values in row-major order, i.e. the height of sample (x, y) is heights[y * sizeX + x]. The heights are copied.</summary>
        public HeightmapPrimitive(ReadOnlySpan<float> heights, int sizeX, int sizeY) : this(heights, ReadOnlySpan<bool>.Empty, sizeX, sizeY) { }

        /// <summary>Create a heightmap primitive with holes that rays pass straight through. holes is (sizeX - 1) * (sizeY - 1) values, one per cell in row-major order, i.e. the cell between samples (x, y) and (x + 1, y + 1) is holes[y * (sizeX - 1) + x]. An empty span means no holes. The heights and holes are copied.</summary>
        public HeightmapPrimitive(ReadOnlySpan<float> heights, ReadOnlySpan<bool> holes, int sizeX, int sizeY)
        {
            if (heights.Length != sizeX * sizeY)
                throw new ArgumentException($"heights.Length must be sizeX * sizeY ({sizeX * sizeY}). Instead received: {heights.Length}");

            if (holes.Length != 0 && holes.Length != (sizeX - 1) * (sizeY - 1))
                throw new ArgumentException($"holes.Length must be 0 or (sizeX - 1) * (sizeY - 1) ({(sizeX - 1) * (sizeY - 1)}). Instead received: {holes.Length}");

            fixed (float* heightsPtr = heights)
            fixed (bool* holesPtr = holes)
                HeightmapPrimitiveBindings.CreateWithHoles(heightsPtr, holes.Length > 0 ? holesPtr : null, sizeX, sizeY, out native).ThrowIfError();

            owns = true;
        }

        /// <summary>Scale of each local axis: X is the distance between adjacent samples along local X, Y multiplies every height, and Z is the distance between adjacent samples along local Z. Defaults to (1, 1, 1)</summary>
        public Vector scale
        {
            get => HeightmapPrimitiveBindings.GetScale(native);
            set => HeightmapPrimitiveBindings.SetScale(native, value).ThrowIfError();
        }

        /// <summary>Must only contain rotation and translation components, not scale. Use scale instead</summary>
        public Matrix transform
        {
            get => *HeightmapPrimitiveBindings.GetTransform(native);
            set => HeightmapPrimitiveBindings.SetTransform(native, ref value).ThrowIfError();
        }

        /// <summary>Number of samples along local X</summary>
        public int sizeX => HeightmapPrimitiveBindings.GetSizeX(native);

        /// <summary>Number of samples along local Z</summary>
        public int sizeY => HeightmapPrimitiveBindings.GetSizeY(native);

        /// <summary>The heightmap's copy of the heights, sizeX * sizeY values in row-major order</summary>
        public ReadOnlySpan<float> heights => new ReadOnlySpan<float>(HeightmapPrimitiveBindings.GetHeights(native), sizeX * sizeY);

        /// <summary>The heightmap's copy of the holes, (sizeX - 1) * (sizeY - 1) values in row-major order. Empty if it has no holes</summary>
        public ReadOnlySpan<bool> holes
        {
            get
            {
                bool* ptr = HeightmapPrimitiveBindings.GetHoles(native);
                return ptr == null ? ReadOnlySpan<bool>.Empty : new ReadOnlySpan<bool>(ptr, (sizeX - 1) * (sizeY - 1));
            }
        }

        protected override VAResult DestroyNative(IntPtr native) => HeightmapPrimitiveBindings.Destroy(native);

        protected override string DebugInfo => $"material={material}, sizeX={sizeX}, sizeY={sizeY}";
    }
}
