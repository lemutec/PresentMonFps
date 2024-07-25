<img src="https://raw.githubusercontent.com/lemutec/PresentMonFps/v2/src/Favicon.png" width="80">

[![NuGet](https://img.shields.io/nuget/v/PresentMonFps.svg)](https://nuget.org/packages/PresentMonFps) [![Actions](https://github.com/lemutec/PresentMonFps/actions/workflows/library.nuget.yml/badge.svg)](https://github.com/lemutec/PresentMonFps/actions/workflows/library.nuget.yml) [![Platform](https://img.shields.io/badge/platform-Windows-blue?logo=windowsxp&color=1E9BFA)](https://dotnet.microsoft.com/en-us/download/dotnet/latest/runtime)

# PresentMonFps

The PresentMon .NET Wrapper for FPS.

## Installation

**Nuget**：https://www.nuget.org/packages/PresentMonFps

**PackageReference**

```xaml
<PackageReference Include="PresentMonFps" Version="2.0.3" />
```

## Demo

```c#
// Check Available.
if (!FpsInspector.IsAvailable)
{
    Console.WriteLine("This library is only available on Windows.");
    return;
}

// Simple method to get PID.
// Fullname is unnecessary.
uint pid = await FpsInspector.GetProcessIdByNameAsync("YourApp.exe");

// Calculate FPS Once.
FpsResult result = await FpsInspector.StartOnceAsync(new FpsRequest(pid));
Console.WriteLine(result);

// Calculate FPS Forever.
await FpsInspector.StartForeverAsync(new FpsRequest(pid), Console.WriteLine, null!);
```

See more from [PresentMon.SampleWPF](https://github.com/lemutec/PresentMonFps/tree/v2/demo/PresentMon.SampleWPF) and [PresentMon.SampleConsole](https://github.com/lemutec/PresentMonFps/tree/v2/demo/PresentMon.SampleConsole).

## Thanks to

- https://github.com/GameTechDev/PresentMon

## Licenses

[MIT](https://github.com/lemutec/PresentMonFps/blob/v2/LICENSE)

## Q&A

Q. What's the diff with v1?

A. C++ Library and UAC is not necessary anymore, the method of `ForeverAsync` is more faster.
