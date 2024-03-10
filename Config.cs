using ManagedBass;

namespace uwap.AudioPlayerLibrary;

public static partial class AudioPlayer
{
    /// <summary>
    /// Whether to keep the audio engine initialized after playback has stopped.<br/>
    /// Default: false
    /// </summary>
    public static bool KeepOpen { get; set; } = false;
}
