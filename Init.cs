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
            throw new Exception("Failed to open the audio engine: " + (ex.Message.Contains("Unable to load shared library 'bass'") || ex.Message.Contains("Unable to load DLL 'bass'") ? "The BASS library could not be loaded, most likely because bass.dll (Windows) or libbass.so (Linux) is not in this program's directory. Refer to https://uwap.org/no-bass in order to resolve this issue." : ex.Message));
        }
    }

    /// <summary>
    /// Manually closes the audio engine.<br/>
    /// If something is playing, playback will be stopped first.
    /// </summary>
    public static void Close()
    {
        try
        {
            if (State == States.Uninitialized)
                return;
            if (State != States.Stopped)
                Stop(false);

            if (!Bass.Free())
                throw DetailedException();

            State = States.Uninitialized;
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to close the audio engine: " + ex.Message);
        }
    }
}
