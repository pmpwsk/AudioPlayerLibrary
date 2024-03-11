using ManagedBass;

namespace uwap.AudioPlayerLibrary;

public static partial class AudioPlayer
{
    /// <summary>
    /// Manually initializes the audio engine. Note that this isn't really necessary since Start(...) does this automatically.
    /// </summary>
    public static void Open()
    {
        try
        {
            if (State != States.Uninitialized)
                return;

            if (!Bass.Init(-1))
                throw DetailedException();

            State = States.Stopped;
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to open the audio engine: " + (ex.Message.Contains("Unable to load shared library 'bass'") ? "The BASS library could not be loaded, most likely because bass.dll (Windows) or libbass.so (Linux) is not in this program's directory. Refer to https://uwap.org/no-bass in order to resolve this issue." : ex.Message));
        }
    }
}
