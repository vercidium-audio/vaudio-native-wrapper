using System;

#if VA_DOUBLE_PRECISION
using VAFloat = System.Double;
#else
using VAFloat = System.Single;
#endif

namespace vaudionativewrapper.managed
{
    /// <summary>A path primitive constructed from an Path, which can be shared across multiple primitives</summary>
    public unsafe class PathPrimitive : Primitive
    {
        Path _svgPath;

        /// <summary>Create a path primitive</summary>
        public PathPrimitive(Path svgPath)
        {
            if (svgPath == null)
                throw new ArgumentNullException(nameof(svgPath));

            IntPtr outPrimitive;
            PathPrimitiveBindings.CreateFromPath(svgPath.native, &outPrimitive).ThrowIfError();

            native = outPrimitive;
            owns = true;
            _svgPath = svgPath;
        }

        /// <summary>Create a path primitive from an SVG path</summary>
        public PathPrimitive(string svgPath)
        {
            if (svgPath == null)
                throw new ArgumentNullException(nameof(svgPath));

            IntPtr outPrimitive;
            PathPrimitiveBindings.Create(svgPath, &outPrimitive).ThrowIfError();

            native = outPrimitive;
            owns = true;
            _svgPath = new Path(PathPrimitiveBindings.GetSvgPath(outPrimitive));
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
        public VAFloat rotation
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
