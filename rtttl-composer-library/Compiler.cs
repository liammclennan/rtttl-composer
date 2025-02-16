namespace rtttl_composer_library;

public static class Compiler
{
    public static SoundToken[] Compile(RtttlToken[] tokens, int tempBpm, OscillationType oscillationType)
    {
        return tokens.Select(r => new SoundToken(
            TwelveToneEqualTemperament.CompileFrequencyHz(r.Tone),
            CompileNoteLengthSeconds(r.NoteLength, tempBpm),
            oscillationType
        )).ToArray();
    }

    public static Double CompileNoteLengthSeconds(NoteLength noteLength, int tempoBpm)
    {
        var msPerBeat = 60000.0 / tempoBpm;
        return noteLength.MeasureFraction switch
        {
            MeasureFraction.Whole => 4 * msPerBeat,
            MeasureFraction.Half => 2 * msPerBeat,
            MeasureFraction.Quarter => msPerBeat,
            MeasureFraction.Eighth => msPerBeat / 2,
            MeasureFraction.Sixteenth => msPerBeat / 4,
            MeasureFraction.ThirtySecond => msPerBeat / 8,
        } * (noteLength.Dotted ? 1.5 : 1) / 1000;
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
