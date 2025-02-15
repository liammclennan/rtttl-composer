using rtttl_composer_library;
using Xunit;

namespace rtttl_tests;

public class Parsing
{
    [Fact]
    public void ParseSimpleToken()
    {
        var token = RtttlParser.Parse("2c1").Single();
        Assert.Equal(token, new RtttlToken(new NoteLength(MeasureFraction.Half, false), new Tone(Note.C, Octave.One)));
    }

    [Fact]
    public void ParseRest()
    {
        var token = RtttlParser.Parse("32.-").Single();
        Assert.Equal(token, new RtttlToken(new NoteLength(MeasureFraction.ThirtySecond, true), null));
    }

    [Fact]
    public void ParseComplexToken()
    {
        var token = RtttlParser.Parse("16.#d3").Single();
        Assert.Equal(token, new RtttlToken(new NoteLength(MeasureFraction.Sixteenth, true), new Tone(Note.DSharp, Octave.Three)));
    }   
    
    [Fact]
    public void ParseTokenWithNoOctave()
    {
        var token = RtttlParser.Parse("16.#d").Single();
        Assert.Equal(token, new RtttlToken(new NoteLength(MeasureFraction.Sixteenth, true), new Tone(Note.DSharp, Octave.Two)));
    }

    [Fact]
    public void ParseSeparatedTokens()
    {
        var tokens = RtttlParser.Parse("16.#d3 32-");
        Assert.Equal(tokens, new []
        {
            new RtttlToken(new NoteLength(MeasureFraction.Sixteenth, true), new Tone(Note.DSharp, Octave.Three)),
            new RtttlToken(new NoteLength(MeasureFraction.ThirtySecond, false), null)
        });
    }
}