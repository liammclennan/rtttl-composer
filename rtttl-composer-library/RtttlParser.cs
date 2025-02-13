namespace rtttl_composer_library;

public enum Duration {
    Whole, 
    Half,
    Quarter,
    Eighth,
    Sixteenth,
    ThirtySecond, // This is all that is supported by RTTTL
}

public readonly record struct NoteLength(Duration Duration, bool Dotted);

public enum Note
{
    A, ASharp, B, C, CSharp, D, DSharp, E, F, FSharp, G, GSharp
}

public enum Octave
{
    One,Two,Three,Four
}

public readonly record struct Tone(Note Note, Octave Octave);

public readonly record struct RtttlToken(NoteLength NoteLength, Tone? Tone); // null tone is a rest

public static class RtttlParser
{
    public static RtttlToken[] Parse(string rtttl)
    {
        // a validation step could go here
    }
    
}