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

/// <summary>
/// Represents a time interval that can be implicitly casted from/to <see cref="double"/> or <see cref="TimeSpan"/>.
/// </summary>
public struct Interval
{
    readonly double _asMilliseconds;
    readonly TimeSpan _asTimeSpan;

    /// <summary>
    /// Initializes interval value compliant with the initial value of <see cref="Timer.Interval"/>.
    /// </summary>
    public static Interval Default => new(100d);

    Interval(double value)
    {
        _asMilliseconds = value;
        _asTimeSpan = TimeSpan.FromMilliseconds(value);
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static implicit operator Interval(TimeSpan storageTime) => new(storageTime.TotalMilliseconds);
    public static implicit operator TimeSpan(Interval storageTime) => storageTime._asTimeSpan;

    public static implicit operator Interval(double milliseconds) => new(milliseconds);
    public static implicit operator double(Interval storageTime) => storageTime._asMilliseconds;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
