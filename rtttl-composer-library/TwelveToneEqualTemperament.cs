namespace rtttl_composer_library;

public static class TwelveToneEqualTemperament
{
    // Set of tones available to RTTTL is the cross product of 
    // the set of octaves and the set of notes.
     static readonly List<(Note,Octave)> Sounds = (
        from octave in Enum.GetValues<Octave>() 
        from note in Enum.GetValues<Note>() 
        select (note, octave)
    ).ToList();
    
    public static int CompileFrequencyHz(Tone? maybeTone)
    {
        if (maybeTone is not { } tone) return 0;
        var gap = Sounds.IndexOf((tone.Note, tone.Octave));
        return Convert.ToInt32(220 * Math.Pow(Math.Pow(2, 1.0 / 12), gap));
    }
}