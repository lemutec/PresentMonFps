using System.Runtime.InteropServices;

namespace PresentMonFps;

public unsafe static class PresentMon
{
    public enum EventTracerExitCodes
    {
        STATUS_OK = 0,
        GENERAL_ERROR = 1000,
        EVENT_RECORDING_ALREADY_RUN_ERROR = 1001,
        EVENT_RECORDING_SHOULD_QUIT_ERROR = 1002,
        EVENT_RECORDING_IS_NOT_RUNNING_ERROR = 1003,
        EVENT_RECORDING_STOP_ERROR = 1004,
        INVALID_ARGUMENTS_ERROR = 1005,
        BUFFER_IS_NOT_EMPTY_ERROR = 1006,
        PRIVILIGIES_ERROR = 1007,
    };

    public struct EventScores
    {
        public double fps;
        public double flip;
        public double deltaReady;
        public double deltaDisplayed;
        public double timeTaken;
        public double screenTime;
    }

    [DllImport("PresentMon.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern EventTracerExitCodes StartEventRecording(int TargetPid, int arraySize);

    [DllImport("PresentMon.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern EventTracerExitCodes StopEventRecording();

    [DllImport("PresentMon.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern EventTracerExitCodes SetLogLevel(int level);

    [DllImport("PresentMon.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern EventTracerExitCodes GetCurrentData(int numSamples, EventScores* scoresOutputBuf, double* timeOutputBuf, int* returnedSamples);

    [DllImport("PresentMon.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern EventTracerExitCodes GetDataCount(ref int result);

    [DllImport("PresentMon.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern EventTracerExitCodes GetData(int dataCount, double* tsBuf, EventScores* scoresBuf);
}
