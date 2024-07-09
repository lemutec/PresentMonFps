using System;

namespace PresentMonFps;

public class FpsInspectorException(string message) : Exception(message)
{
    public uint ExitCode { get; set; }

    public FpsInspectorException(string message, uint res) : this(message)
    {
        ExitCode = res;
    }
}
