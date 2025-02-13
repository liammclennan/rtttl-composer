using rtttl_composer_library;
using Xunit;

namespace rtttl_tests;

public class Parsing
{
    [Fact]
    public void ParseSimpleToken()
    {
        var ds = RtttlParser.Parse("2c1");
        
    }

    [Fact]
    public void ParseRest()
    {
        
    }

    [Fact]
    public void ParseComplexToken()
    {
        var input = "16.#d2";
    }

    [Fact]
    public void ParseSeparatedTokens()
    {
        
    }
}