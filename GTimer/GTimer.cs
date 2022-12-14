//MIT License

//Copyright (c) 2022 9ualaBanana

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

namespace System.Timers;

/// <inheritdoc cref="Timer"/>
/// <remarks>
/// Provides additional information about its state and interface that allows configuration using <see cref="TimeSpan"/> objects.
/// </remarks>
public class GTimer : Timer
{
    public readonly DateTimeOffset CreationTime = DateTimeOffset.Now;
    public DateTimeOffset LastResetTime { get; private set; }
    public Interval ElapsedSinceCreation => DateTimeOffset.Now - CreationTime;
    public Interval ElapsedSinceReset => DateTimeOffset.Now - LastResetTime;
    public Interval TimeLeftUntilReset => Interval - ElapsedSinceReset;
    public TimeSpan IntervalAsTimeSpan
    {
        get => new((long)Interval * TimeSpan.TicksPerMillisecond);
        set => Interval = value.TotalMilliseconds;
    }
    public TimeSpan Uptime => Enabled ?
        _uptimeUptoLastStop + _UptimeSinceLastStart:
        _uptimeUptoLastStop;
    TimeSpan _uptimeUptoLastStop = TimeSpan.Zero;
    TimeSpan _UptimeSinceLastStart => DateTimeOffset.Now - _lastStartTime;
    DateTimeOffset _lastStartTime;


    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref='GTimer'/> class, with the properties set to initial values.
    /// </summary>
    public GTimer() : this(Timers.Interval.Default)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref='GTimer'/> class, setting the <see cref='Timer.Interval'/> property to the specified period.
    /// </summary>
    public GTimer(Interval interval) : base(interval)
    {
        LastResetTime = CreationTime;
        base.Elapsed += (_, e) => LastResetTime = e.SignalTime;
    }
    #endregion


    /// <summary>
    /// Starts raising the <see cref="Timer.Elapsed"/> event by setting <see cref='Timer.Enabled'/> to <see langword = 'true'/>.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public new void Start()
    {
        if (!Enabled)
        {
            bool isBeingReset = _lastStartTime != default;
            _lastStartTime = DateTimeOffset.Now;
            if (isBeingReset) LastResetTime = _lastStartTime;
        }
        base.Start();
    }

    /// <summary>
    /// Stops raising the <see cref="Timer.Elapsed"/> event by setting <see cref='Timer.Enabled'/> to <see langword = 'false'/>.
    /// </summary>
    public new void Stop()
    {
        if (Enabled) _uptimeUptoLastStop += DateTimeOffset.Now - _lastStartTime;
        base.Stop();
    }

    /// <summary>
    /// Resets the elapsed time.
    /// </summary>
    public void Restart()
    {
        Stop();
        Start();
        LastResetTime = DateTimeOffset.Now;
    }
}