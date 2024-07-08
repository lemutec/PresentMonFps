<img src="https://github.com/lemutec/PresentMonFps/blob/master/pack/Favicon.png?raw=true" width="80">

# PresentMonFps

The PresentMon .NET Wrapper for FPS.

> The Administrator Permission UAC is requested.
>
> The x64 target platform is requested.

## Demo

```c#
uint pid = await FpsInspector.GetProcessIdByNameAsync("YourApp.exe");

// Once
FpsResult result = await FpsInspector.StartOnceAsync(new FpsRequest(pid));
Console.WriteLine(result);

// Forever
await FpsInspector.StartForeverAsync(new FpsRequest(pid), Console.WriteLine, null!);
```

## Thanks to

- https://github.com/Andrey1994/fps_inspector_sdk
- https://pypi.org/project/fps-inspector-sdk/

- https://github.com/GameTechDev/PresentMon

## Licenses

MIT

