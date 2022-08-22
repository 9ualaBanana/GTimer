namespace System.Timers;

/// <inheritdoc cref="Timer"/>
/// <remarks>
/// Provides additional information about its state and interface that allows configuration using <see cref="TimeSpan"/> objects.
/// </remarks>
public class GTimer : Timer
{
    public readonly DateTimeOffset CreationTime = DateTimeOffset.Now;
    public DateTimeOffset LastResetTime { get; private set; }
    public TimeSpan ElapsedSinceCreation => DateTimeOffset.Now - CreationTime;
    public TimeSpan ElapsedSinceReset => DateTimeOffset.Now - LastResetTime;
    public TimeSpan IntervalAsTimeSpan
    {
        get => new((long)Interval * TimeSpan.TicksPerMillisecond);
        set => Interval = value.TotalMilliseconds;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref='GTimer'/> class, with the properties set to initial values.
    /// </summary>
    public GTimer() : this(default(double))
    {
    }

    /// <inheritdoc cref="GTimer.GTimer(double)"/>
    public GTimer(TimeSpan interval) : this(interval.TotalMilliseconds)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref='GTimer'/> class, setting the <see cref='Timer.Interval'/> property to the specified period.
    /// </summary>
    public GTimer(double interval) : base(interval)
    {
        Elapsed += (_, e) => LastResetTime = e.SignalTime;
    }
}