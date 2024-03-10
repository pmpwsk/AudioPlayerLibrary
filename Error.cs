using ManagedBass;

namespace uwap.AudioPlayerLibrary;

public static partial class AudioPlayer
{
    private static Exception DetailedException()
        => new(DetailedError());

    private static string DetailedError()
        => Bass.LastError switch
        {
            Errors.Busy => "The audio driver is busy, most likely because something else is playing and the driver doesn't support multiple audio streams at once.",
            Errors.Unknown => "An unknown error occurred.",
            Errors.OK => "No error.",
            _ => $"Unrecognized error \"{Bass.LastError}\"."
        };
}
