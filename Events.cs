﻿namespace uwap.AudioPlayerLibrary;

public static partial class AudioPlayer
{
    /// <summary>
    /// This event is called when the currently playing audio track is over (but not when Stop() is called).
    /// </summary>
    public static event Action? TrackOver = null;

    private static void TrackOverSync(int Handle, int Channel, int Data, IntPtr User)
    {
        Stop();
        TrackOver?.Invoke();
    }
}
