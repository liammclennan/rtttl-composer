namespace rtttl_composer_library;

public static class Compiler
{
    public static SoundToken[] Compile(RtttlToken[] tokens, int tempBpm, OscillationType oscillationType)
    {
        return tokens.Select(r => new SoundToken(
            CompileFrequencyHz(r.Tone),
            CompileNoteLength(r.NoteLength, tempBpm),
            oscillationType
        )).ToArray();
    }

    static int CompileFrequencyHz(Tone? tone)
    {
        return 0;
    }

    static TimeSpan CompileNoteLength(NoteLength noteLength, int tempoBpm)
    {
        var msPerBeat = 60000 / tempoBpm;
        return TimeSpan.FromMilliseconds(noteLength.MeasureFraction switch
        {
            MeasureFraction.Whole => 4 * msPerBeat,
            _ => 1,
        } * (noteLength.Dotted ? 1.5 : 1));
    }
    
}

public enum OscillationType
{
    Sine,
    Square,
    Sawtooth,
    Triangle,
}

public readonly record struct SoundToken(int FrequencyHz, TimeSpan Duraction, OscillationType OscillationType);
