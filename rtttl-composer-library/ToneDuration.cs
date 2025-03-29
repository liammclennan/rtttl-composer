namespace rtttl_composer_library;

public static class ToneDuration
{
    // Function name is *Seconds because the return type does not encode the units. 
    public static double CompileNoteLengthSeconds(NoteLength noteLength, int tempoBpm)
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
            _ => throw new Exception($"Unknown measure fraction value {noteLength.MeasureFraction}")
        } * (noteLength.Dotted ? 1.5 : 1) / 1000;
    }
}