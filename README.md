<img src="./pack/Favicon.png" width="80">

# PresentMonFps

The PresentMon .NET Wrapper for FPS.

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

