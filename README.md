# Vercidium Audio

This repository contains .NET Standard 2.1 wrappers for the 2D and 3D Vercidium Audio C SDKs.

This repository requires:
- Vercidium Audio v1.11.0. Download it from [vercidium.com](https://vercidium.com)

> Please note that the Vercidium Audio SDK is not free for commercial use. See [vercidium.com/eula](https://vercidium.com/eula)

## Single and double precision

The 3D C SDK is available as a single- and double-precision DLL. Use the wrapper project that matches the SDK you're using:

- `2d/vaudio-native-wrapper-2d.csproj` and `3d/vaudio-native-wrapper-3d.csproj` for the single precision DLL
- `3d-double/vaudio-native-wrapper-3d-double.csproj` for the double precision DLL

Creating a `World` will throw if the loaded DLL's precision doesn't match the wrapper.

## Features

- Muffle sounds in real time
- Accurate reverb in any environment
- Innovative event-based raytracing system
- Realistic energy-based model with materials
- Dynamic scene updates - automatically handles moving objects

## References
- [Vercidum Audio documentation](https://vercidium.com/docs)

## Licencing

The Vercidium Audio SDK is free for non-commercial products only. To purchase a licence for commercial use, head over to the [Vercidium Audio website](https://vercidium.com).
