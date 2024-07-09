using System;

namespace PresentMonFps;

public sealed class FpsCalculator
{
    private const int _sampleCount = 100;
    private long[] _presentTimestamps = new long[_sampleCount];
    private int _index = 0;
    private int _count = 0;

    public double Fps { get; private set; } = default;

    public event EventHandler<double>? ResultReceived = null;

    public unsafe void Calculate(long timestampTicks)
    {
        fixed (long* pTimestamps = _presentTimestamps)
        {
            pTimestamps[_index] = timestampTicks;
            _index = (_index + 1) % _sampleCount;
            if (_count < _sampleCount)
            {
                _count++;
            }

            if (_count >= 2)
            {
                long firstTimestamp = pTimestamps[(_index - _count + _sampleCount) % _sampleCount];
                long lastTimestamp = pTimestamps[(_index - 1 + _sampleCount) % _sampleCount];
                double totalTime = (lastTimestamp - firstTimestamp) / (double)TimeSpan.TicksPerSecond;
                double fps = (_count - 1) / totalTime;
                Fps = fps;
                ResultReceived?.Invoke(this, fps);
            }
        }
    }
}
