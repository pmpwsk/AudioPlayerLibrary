using ManagedBass;

namespace uwap.AudioPlayerLibrary;

public static partial class AudioPlayer
{
    /// <summary>
    /// Starts playing the given file.<br/>
    /// If the audio engine isn't initialized, it will be opened.<br/>
    /// If something else is playing, playback will be stopped first.
    /// </summary>
    public static void Start(string filePath)
    {
        try
        {
            if (State == States.Uninitialized)
                Open();
            else if (State != States.Stopped)
                Stop();

            Stream = Bass.CreateStream(filePath);
            if (Stream == 0)
                throw DetailedException();

            if (!Bass.ChannelPlay(Stream))
                throw DetailedException();
            EndSync = Bass.ChannelSetSync(Stream, SyncFlags.End, 0, TrackOverSync, 0);
            if (EndSync == 0)
                throw new Exception("Failed to add the 'end' sync to the stream: " + DetailedError());

            State = States.Playing;
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to start playback: " + ex.Message);
        }
    }
}
