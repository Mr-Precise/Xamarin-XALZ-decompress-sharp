# Xamarin-XALZ-decompress-sharp
Xamarin XALZ .dll assembly decompressor written in Mono / .Net  

## Dependencies
Linux/macOS:  
install `git`, `mono` and `nuget` packages from repository, [mono-project](https://www.mono-project.com/download/nightly/#download-lin) or [winehq/mono](https://gitlab.winehq.org/mono/mono)

## Build
```sh
git clone https://github.com/Mr-Precise/Xamarin-XALZ-decompress-sharp
cd Xamarin-XALZ-decompress-sharp
nuget restore
msbuild /p:Configuration=Release
```

## Download
1. Download latest build from [release](https://github.com/Mr-Precise/Xamarin-XALZ-decompress-sharp/releases)  
2. Unpack && Run  
`mono ./Xamarin_XALZ_decompress_sharp.exe xamarin-compressed-input.dll xamarin-uncompressed-output.dll`

## Used libraries & projects
[K4os.Compression.LZ4](https://github.com/MiloszKrajewski/K4os.Compression.LZ4)  
[DotDevelop / MonoDevelop](https://github.com/dotdevelop/dotdevelop/)  
[compressed assemblies in APK](https://github.com/dotnet/android/pull/4686)