<img src="https://raw.githubusercontent.com/lemutec/PresentMonFps/v2/src/Favicon.png" width="80">

# PresentMonFps

The PresentMon .NET Wrapper for FPS.

> The Administrator Permission UAC is requested.
>
> The x64 target platform is requested.

## Installation

**Nuget**：https://www.nuget.org/packages/PresentMonFps

**PackageReference**

```xaml
<PackageReference Include="PresentMonFps" Version="1.0.8.1" />
```

## Demo

```c#
// Check Available.
if (!FpsInspector.IsAvailable)
{
    Console.WriteLine("This library is only available on Windows x64 and Administrator Permission.");
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

## Thanks to

- https://github.com/Andrey1994/fps_inspector_sdk
- https://pypi.org/project/fps-inspector-sdk/

- https://github.com/GameTechDev/PresentMon

## Licenses

MIT

