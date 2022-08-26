namespace System.Timers;

/// <summary>
/// Represents a time interval that can be implicitly casted from/to <see cref="double"/> or <see cref="TimeSpan"/>.
/// </summary>
public readonly struct Interval
{
    readonly double _asMilliseconds;
    readonly TimeSpan _asTimeSpan;

    Interval(double value)
    {
        _asMilliseconds = value;
        _asTimeSpan = TimeSpan.FromMilliseconds(value);
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static implicit operator Interval(TimeSpan storageTime) => new(storageTime.TotalMilliseconds);
    public static implicit operator TimeSpan(Interval storageTime) => storageTime._asTimeSpan;

    public static implicit operator Interval(double milliseconds) => new Interval(milliseconds);
    public static implicit operator double(Interval storageTime) => storageTime._asMilliseconds;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
