namespace rtttl_composer_library;

public class Composer
{
    public static Composition RtttlToComposition(string rtttl, int tempoBpm = 120, OscillationType oscillationType = OscillationType.Square)
    {
        var parsed = RtttlParser.Parse(rtttl);
        var compiled = Compiler.Compile(parsed, tempoBpm, oscillationType);
        return new Composition(tempoBpm, compiled);
    }
}

public readonly record struct Composition(int TempoBpm, SoundToken[] Tokens);