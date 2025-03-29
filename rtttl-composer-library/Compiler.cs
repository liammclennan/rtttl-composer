namespace rtttl_composer_library;

public static class Compiler
{
    public static SoundToken[] Compile(RtttlToken[] tokens, int tempBpm, OscillationType oscillationType)
    {
        return tokens.Select(r => new SoundToken(
            TwelveToneEqualTemperament.CompileFrequencyHz(r.Tone),
            ToneDuration.CompileNoteLengthSeconds(r.NoteLength, tempBpm),
            oscillationType
        )).ToArray();
    }
}

public enum OscillationType
{
    Sine,
    Square,
    Sawtooth,
    Triangle,
}

public readonly record struct SoundToken(int FrequencyHz, Double DuractionSeconds, OscillationType OscillationType);
