using System;

namespace vaudionativewrapper.managed
{
    /// <summary>An immutable, pre-parsed SVG path that can be shared across multiple PathPrimitive instances</summary>
    public unsafe class Path
    {
        public IntPtr native;

        /// <summary>Parse local-space SVG path data (the contents of an SVG "d" attribute) once, so it can be reused by any number of PathPrimitive instances. Supported commands: M/m, L/l, H/h, V/v, C/c, Q/q, A/a, Z/z (absolute and relative forms). Elliptical arcs (A/a) are converted to cubic bezier curves when parsed. Smooth-curve shorthand (S/s, T/t) is not supported. If the path ends with Z/z it is treated as closed.</summary>
        public Path(string data)
        {
            IntPtr outPath;
            PathBindings.Create(data, &outPath).ThrowIfError();

            native = outPath;
        }

        /// <summary>The SVG path data this path was parsed from</summary>
        public string data => PathBindings.GetData(native);

        /// <summary>Whether the path ends with Z/z</summary>
        public bool closed => PathBindings.GetClosed(native);

        /// <summary>Destroys the native path. Returns <see cref="VAResult.ErrorInUse"/> if a PathPrimitive using this path has not been destroyed yet, in which case Destroy can be called again later</summary>
        public VAResult Destroy()
        {
            if (native == IntPtr.Zero)
                return VAResult.Success;

            var result = PathBindings.Destroy(native);

            if (result == VAResult.Success)
                native = IntPtr.Zero;

            return result;
        }

        ~Path()
        {
            if (native != IntPtr.Zero)
                LogSettings.Warn("Path was garbage collected without calling Destroy() first.");
        }
    }
}
