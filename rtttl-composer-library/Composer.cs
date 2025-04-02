namespace rtttl_composer_library;

public class Composer
{
    public static Composition RtttlToComposition(string rtttl, int tempoBpm = 120, OscillationType oscillationType = OscillationType.Square)
    {
        var parsed = RtttlParser.Parse(rtttl);
        var compiled = Compiler.Compile(parsed, tempoBpm, oscillationType);
        return new Composition(tempoBpm, compiled.ToArray());
    }
    
    public static MemoryStream ToBuffer(string rtttl, int tempoBpm = 120, OscillationType oscillationType = OscillationType.Square)
    {
        // TODO: Oscillation type is ignored in this branch
        var parsed = RtttlParser.Parse(rtttl);
        var compiled = Compiler.Compile(parsed, tempoBpm, oscillationType);
        var buffer = PcmAudio.PackWaveBuffer(compiled);
        buffer.Seek(0, SeekOrigin.Begin);
        return buffer;
    }
}

public readonly record struct Composition(int TempoBpm, SoundToken[] Tokens);