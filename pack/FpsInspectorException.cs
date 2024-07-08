using System;

namespace PresentMonFps;

public class FpsInspectorException(string message) : Exception(message)
{
    public PresentMon.EventTracerExitCodes ExitCode { get; set; }

    public FpsInspectorException(string message, PresentMon.EventTracerExitCodes res) : this(message)
    {
        ExitCode = res;
    }
}
