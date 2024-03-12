using ManagedBass;

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
    /// The elapsed time.
    /// </summary>
    public static TimeSpan Position
    {
        get => State == States.Playing || State == States.Paused ? TimeSpan.FromSeconds(Bass.ChannelBytes2Seconds(Stream, Bass.ChannelGetPosition(Stream))) : TimeSpan.Zero;
        set
        {
            try
            {
                if (State != States.Playing && State != States.Paused)
                    throw new Exception("Nothing is playing right now!");

                long length = Bass.ChannelGetLength(Stream);
                long pos = Bass.ChannelSeconds2Bytes(Stream, value.TotalSeconds);

                if (pos < 0)
                    pos = 0;

                if (pos >= length)
                {
                    Stop();
                    TrackOver?.Invoke();
                }
                else if (!Bass.ChannelSetPosition(Stream, pos))
                    throw DetailedException();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to set the position: " + ex.Message);
            }
        }
    }

    /// <summary>
    /// The total length of the track.
    /// </summary>
    public static TimeSpan Length
        => State == States.Playing || State == States.Paused ? TimeSpan.FromSeconds(Bass.ChannelBytes2Seconds(Stream, Bass.ChannelGetLength(Stream))) : TimeSpan.Zero;

    /// <summary>
    /// The ratio of position to length (between 0 and 1).
    /// </summary>
    public static double PositionRatio
    {
        get => State == States.Playing || State == States.Paused ? Math.Round((double)Bass.ChannelGetPosition(Stream) / Bass.ChannelGetLength(Stream), 1, MidpointRounding.AwayFromZero) : 0;
        set
        {
            try
            {
                if (State != States.Playing && State != States.Paused)
                    throw new Exception("Nothing is playing right now!");
                if (value < 0 || value > 1)
                    throw new Exception("The given position is outside of the possible range (0 to 1)!");

                long length = Bass.ChannelGetLength(Stream);
                long pos = (long)Math.Round(value * length, MidpointRounding.AwayFromZero);

                if (pos == length)
                {
                    Stop();
                    TrackOver?.Invoke();
                }
                else if (!Bass.ChannelSetPosition(Stream, pos))
                    throw DetailedException();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to set the position: " + ex.Message);
            }
        }
    }
}
