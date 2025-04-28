using System.Text.RegularExpressions;

namespace rtttl_composer_library;

public enum MeasureFraction {
    Whole, 
    Half,
    Quarter,
    Eighth,
    Sixteenth,
    ThirtySecond, // This is all that is supported by RTTTL
}

public readonly record struct NoteLength(MeasureFraction MeasureFraction, bool Dotted);

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
    //s Parse a space separated list of tokens.
    public static RtttlToken[] Parse(string rtttl)
    {
        return rtttl.Split(' ').Select(ParseToken).ToArray();
    }

    // Parse a token like '16.#d2'.
    static RtttlToken ParseToken(string rtttl)
    {
        var match = new Regex(
            @"(?<MeasureFraction>\d+)(?<Dot>\.?)(?<Note>#?\w)?(?<OctaveOrRest>[1|2|3|4|-]?)"
        ).Match(rtttl.Trim());

        if (!match.Success) throw new Exception("Failed to parse token" + rtttl);

        return new RtttlToken(
            new NoteLength(ToMeasureFraction(
                match.Groups["MeasureFraction"].Value), 
                match.Groups["Dot"].Value.Equals(".")), 
            ToOctave(match.Groups["OctaveOrRest"].Value) is { } o 
                ? new Tone(ToNote(match.Groups["Note"].Value), o)
                : null
        );
    }
    
    static MeasureFraction ToMeasureFraction(string input)
    {
        return input switch
        {
            "1" => MeasureFraction.Whole,
            "2" => MeasureFraction.Half,
            "4" => MeasureFraction.Quarter,
            "8" => MeasureFraction.Eighth,
            "16" => MeasureFraction.Sixteenth,
            "32" => MeasureFraction.ThirtySecond,
            _ => throw new Exception("Unable to parse measure fraction " + input)
        };
    }

    static Note ToNote(string input)
    {
        return input.ToLowerInvariant() switch
        {
            "a" => Note.A,
            "#a" => Note.ASharp,
            "b" => Note.B,
            "c" => Note.C,
            "#c" => Note.CSharp,
            "d" => Note.D,
            "#d" => Note.DSharp,
            "e" => Note.E,
            "f" => Note.F,
            "#f" => Note.FSharp,
            "g" => Note.G,
            "#g" => Note.GSharp,
            _ => throw new Exception("Unable to parse note " + input)
        };
    }

    static Octave? ToOctave(string input)
    {
        return input switch
        {
            "1" => Octave.One,
            "2" or "" => Octave.Two,
            "3" => Octave.Three,
            "4" => Octave.Four,
            "-" => null,
            _ => throw new Exception("Unable to parse octave or rest " + input)
        };
    }
}