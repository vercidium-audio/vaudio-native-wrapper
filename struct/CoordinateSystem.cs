namespace vaudionativewrapper
{
    /// <summary>
    /// Coordinate system used when calculating listener-relative reverb directionality
    /// (<see cref="managed.World.CalculateListenerRelativePan"/>). Must match vaudio.CoordinateSystem (C#) and
    /// VACoordinateSystem (native C).
    /// </summary>
    public enum CoordinateSystem
    {
        /// <summary>
        /// Right-handed. Up = Y+, Forward = Z-, Right = X+. Same as OpenGL/glTF, and identical to
        /// this SDK's internal space (no conversion applied). Also matches Godot's camera/light
        /// forward convention.
        /// </summary>
        Default = 0,

        /// <summary>
        /// Right-handed. Up = Z+, Forward = Y-, Right = X+. Matches Blender's viewport look
        /// direction (Front/Numpad-1 view looks down Y-).
        /// </summary>
        Blender,

        /// <summary>
        /// Right-handed. Up = Y+, Forward = Z-, Right = X+. Matches Godot's camera/light forward
        /// convention (not the separate +Z "model front" convention Godot uses for imported assets).
        /// Mathematically identical to <see cref="Default"/>.
        /// </summary>
        Godot,

        /// <summary>
        /// Left-handed. Up = Y+, Forward = Z+, Right = X+.
        /// </summary>
        Unity,

        /// <summary>
        /// Left-handed. Up = Z+, Forward = X+, Right = Y+.
        /// </summary>
        Unreal,
    }
}
