using ManagedBass;

namespace uwap.AudioPlayerLibrary;

public static partial class AudioPlayer
{
    /// <summary>
    /// Whether to keep the audio engine initialized after playback has stopped.<br/>
    /// Default: false
    /// </summary>
    public static bool KeepOpen { get; set; } = false;

    /// <summary>
    /// The volume for the playback (between 0 and 1).
    /// </summary>
    public static double Volume
    {
        get => Bass.GlobalStreamVolume / (double)10000;
        set
        {
            if (value < 0 || value > 1)
                throw new ArgumentOutOfRangeException(nameof(value), "The volume has to be between 0.0 and 1.0!");

            Bass.GlobalStreamVolume = (int)Math.Round(value * 10000, 0, MidpointRounding.AwayFromZero);
        }
    }
}
