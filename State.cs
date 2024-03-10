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
}
