namespace GTimer.Test;

using System.Timers;
using GTimer = System.Timers.GTimer;

public class GTimerTest
{
    [Fact]
    public void LastResetTime_UponInstantiation_EqualsCreationTime()
    {
        var gTimer = new GTimer();

        gTimer.LastResetTime.Should().Be(gTimer.CreationTime);
    }

    [Fact]
    public void Start_WhenEnabled_DoesNothing()
    {
        var gTimer = new GTimer();

        gTimer.Start();
        gTimer.Start();

        gTimer.LastResetTime.Should().Be(gTimer.CreationTime);
    }

    [Fact]
    public void Stop_WhenNotEnabled_DoesNothing()
    {
        var gTimer = new GTimer();
        var uptimeBeforeStop = gTimer.Uptime;

        gTimer.Stop();

        gTimer.Uptime.Should().Be(uptimeBeforeStop);
    }

    [Fact]
    public void Start_AfterStop_IsConsideredReset()
    {
        var gTimer = new GTimer();

        gTimer.Start();
        gTimer.Stop();
        gTimer.Start();

        gTimer.ShouldBeReset();
    }

    [Fact]
    public void Uptime_UponInstantiation_IsZero()
    {
        var gTimer = new GTimer();

        gTimer.Uptime.Should().Be(TimeSpan.Zero);
    }

    [Fact]
    public void Uptime_WhenEnabled_KeepsIncreasing()
    {
        var gTimer = new GTimer();

        gTimer.Start();
        var firstUptimeSample = gTimer.Uptime;
        var secondUptimeSample = gTimer.Uptime;

        secondUptimeSample.Should().BeGreaterThan(firstUptimeSample);
    }

    [Fact]
    public void Uptime_WhenStopped_StopsIncreasing()
    {
        var gTimer = new GTimer();

        gTimer.Start();
        gTimer.Stop();
        var firstUptimeSample = gTimer.Uptime;
        var secondUptimeSample = gTimer.Uptime;

        firstUptimeSample.Should().Be(secondUptimeSample);
    }

    [Fact]
    public void Restart_IsConsideredReset()
    {
        var gTimer = new GTimer();

        gTimer.Restart();

        gTimer.ShouldBeReset();
    }

    [Fact]
    public void IsConsideredReset_WhenElapsed()
    {
        var gTimer = new GTimer();
        gTimer.Elapsed += (sender, _) => ((GTimer)sender!).ShouldBeReset();

        gTimer.Start();
        Thread.Sleep(Interval.Default.WithExtraTime());
    }
}

static class TestExtensions
{
    internal static void ShouldBeReset(this GTimer gTimer) =>
        gTimer.LastResetTime.Should().BeAfter(gTimer.CreationTime);

    internal static Interval WithExtraTime(this Interval interval) =>
        interval += 100;
}
