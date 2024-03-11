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

    /// <summary>
    /// Stops the playback.<br/>
    /// If KeepOpen is false, the audio engine will be closed afterwards.
    /// </summary>
    public static void Stop()
        => Stop(true);

    private static void Stop(bool allowClose)
    {
        try
        {
            switch (State)
            {
                case States.Uninitialized:
                case States.Stopped:
                    return;
            }

            if (!Bass.ChannelRemoveSync(Stream, EndSync))
                throw new Exception("Failed to remove the 'end' sync from the stream: " + DetailedError());
            if (!Bass.StreamFree(Stream))
                throw DetailedException();

            State = States.Stopped;
            Stream = 0;

            if (allowClose && !KeepOpen)
                Close();
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to stop playback: " + ex.Message);
        }
    }
}
