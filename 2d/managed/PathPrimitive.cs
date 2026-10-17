using System;

namespace vaudionativewrapper.managed
{
    /// <summary>A path primitive constructed from a <see cref="Path"/>, which can be shared across multiple primitives</summary>
    public unsafe class PathPrimitive : Primitive
    {
        Path _svgPath;

        /// <summary>Create a path primitive. The path must not be destroyed until every primitive using it has been destroyed</summary>
        public PathPrimitive(Path svgPath)
        {
            if (svgPath == null)
                throw new ArgumentNullException(nameof(svgPath));

            IntPtr outPrimitive;
            PathPrimitiveBindings.Create(svgPath.native, &outPrimitive).ThrowIfError();

            native = outPrimitive;
            owns = true;
            _svgPath = svgPath;
        }

        /// <summary>Shared, pre-parsed local-space SVG path, before rotation, scale and position are applied</summary>
        public Path svgPath
        {
            get => _svgPath;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(svgPath));

                PathPrimitiveBindings.SetSvgPath(native, value.native).ThrowIfError();

                _svgPath = value;
            }
        }

        /// <summary>Position of the path in world space</summary>
        public Vector position
        {
            get => PathPrimitiveBindings.GetPosition(native);
            set => PathPrimitiveBindings.SetPosition(native, value).ThrowIfError();
        }

        /// <summary>Rotation of the path in radians</summary>
        public float rotation
        {
            get => PathPrimitiveBindings.GetRotation(native);
            set => PathPrimitiveBindings.SetRotation(native, value).ThrowIfError();
        }

        /// <summary>Scale applied to the local-space path, before rotation and position</summary>
        public Vector scale
        {
            get => PathPrimitiveBindings.GetScale(native);
            set => PathPrimitiveBindings.SetScale(native, value).ThrowIfError();
        }

        protected override VAResult DestroyNative(IntPtr native) => PathPrimitiveBindings.Destroy(native);

        protected override string DebugInfo => $"material={material}, position={position}, rotation={rotation}, scale={scale}";
    }
}
