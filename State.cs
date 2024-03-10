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
}
