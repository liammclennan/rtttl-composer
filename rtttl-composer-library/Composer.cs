namespace rtttl_composer_library;

public class Composer
{
}



public enum OscillationType
{
    Sine,
    Square,
    Sawtooth,
    Triangle,
}

public class SoundToken
{
    public int FrequencyHz { get; set; }
    public TimeSpan Duration { get; set; }
    public OscillationType OscillationType { get; set; } = OscillationType.Square;
}

public class Composition
{
    public int TempoBpm { get; set; }
    public SoundToken[] Tokens { get; set; } = [];
}