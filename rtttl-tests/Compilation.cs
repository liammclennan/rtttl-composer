using rtttl_composer_library;
using Xunit;

namespace rtttl_tests;

public class Compilation
{
    [Theory]
    [InlineData(60, MeasureFraction.Whole, false, 4)]    
    [InlineData(60, MeasureFraction.Half, false, 2)]    
    [InlineData(60, MeasureFraction.Quarter, false, 1)]
    [InlineData(60, MeasureFraction.Quarter, true, 1.5)]
    [InlineData(60, MeasureFraction.Eighth, false, 0.5)]
    [InlineData(60, MeasureFraction.Sixteenth, false, 0.250)]
    [InlineData(60, MeasureFraction.Sixteenth, true, 0.375)]
    [InlineData(60, MeasureFraction.ThirtySecond, false, 0.125)]
    [InlineData(120, MeasureFraction.Quarter, false, 0.500)]
    public void CompilesNoteLengthsCorrectly(int bpm, MeasureFraction measureFraction, bool dotted, Double durationSeconds)
    {
        Assert.Equal(
            durationSeconds,
            Compiler.CompileNoteLengthSeconds(new NoteLength(measureFraction, dotted), bpm)
        );
    }
    
    [Theory]
    [InlineData(Note.A, Octave.Two, 440)]
    [InlineData(Note.C, Octave.One, 262)]
    [InlineData(Note.A, Octave.Three, 880)]
    [InlineData(Note.FSharp, Octave.One, 370)]
    public void CompilesFrequencyCorrectly(Note note, Octave octave, int frequencyHz)
    {
        Assert.Equal(
            frequencyHz,
            TwelveToneEqualTemperament.CompileFrequencyHz(new Tone(note, octave))
        );
    }
}