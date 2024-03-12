namespace uwap.AudioPlayerLibrary;

public static partial class AudioPlayer
{
    public enum States
    {
        Uninitialized,
        Stopped,
        Paused,
        Playing
    }
    
    private static int Stream = 0;

    private static int EndSync = 0;

    /// <summary>
    /// The current state.
    /// </summary>
    public static States State { get; private set; } = States.Uninitialized;

    /// <summary>
    /// The ratio of position to length (between 0 and 1).
    /// </summary>
    public static double PositionRatio
    {
        get => State == States.Playing || State == States.Paused ? Math.Round((double)Bass.ChannelGetPosition(Stream) / Bass.ChannelGetLength(Stream), 1, MidpointRounding.AwayFromZero) : 0;
    }
}
