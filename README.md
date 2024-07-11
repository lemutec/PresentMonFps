<img src="https://raw.githubusercontent.com/lemutec/PresentMonFps/v2/src/Favicon.png" width="80">

# PresentMonFps

The PresentMon .NET Wrapper for FPS.

## Installation

**Nuget**：https://www.nuget.org/packages/PresentMonFps

**PackageReference**

```xaml
<PackageReference Include="PresentMonFps" Version="2.0.1" />
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

## Thanks to

- https://github.com/GameTechDev/PresentMon

## Licenses

[MIT](https://github.com/lemutec/PresentMonFps/blob/v2/LICENSE)

## Q&A

Q. What's the diff with v1?

A. C++ Library and UAC is not necessary anymore, the method of `ForeverAsync` is more faster.
