namespace rtttl_composer_library;

public static class Compiler
{
    public static IEnumerable<SoundToken> Compile(RtttlToken[] tokens, int tempBpm, OscillationType oscillationType)
    {
        return tokens.Select(r => new SoundToken(
            TwelveToneEqualTemperament.CompileFrequencyHz(r.Tone),
            ToneDuration.CompileNoteLengthSeconds(r.NoteLength, tempBpm),
            oscillationType
        ));
    }
}

public enum OscillationType
{
    Sine,
    Square,
    Sawtooth,
    Triangle,
}

public readonly record struct SoundToken(int FrequencyHz, Double DurationSeconds, OscillationType OscillationType);
