namespace rtttl_composer_library;

public class TwelveToneEqualTemperament
{
    public static List<(Note,Octave)> Sounds = (from octave in Enum.GetValues<Octave>() 
        from note in Enum.GetValues<Note>() 
        select (note, octave)).ToList();
    
    public static int CompileFrequencyHz(Tone? tone)
    {
        if (!tone.HasValue) return 0;

        var gap = Sounds.IndexOf((tone.Value.Note, tone.Value.Octave));
        return Convert.ToInt32(220 * Math.Pow(Math.Pow(2, 1.0/12), gap));
    }
}