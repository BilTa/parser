using FluentAssertions;
using Xunit;

namespace ParserTests
{
    public class MostPopularTagTests
    {
        private Parser.Parser _parser;

        public MostPopularTagTests()
        {
            _parser = new Parser.Parser();
        }

        [Fact]
        public void GetMostCommonTagName_EqualTagNumber_returnsFirstTag()
        {
            var xml = @"<notes><tag1></tag1></notes>";
            var xmlDoc = _parser.LoadFromString(xml);

            var result = _parser.GetMostCommonTagName(xmlDoc);

            result.Should().Be("notes");
        }

        [Fact]
        public void GetMostCommonTagName_Tag2Repeats_returnsTag2()
        {
            var xml = @"<notes><tag1><tag2></tag2><tag2></tag2></tag1></notes>";
            var xmlDoc = _parser.LoadFromString(xml);

            var result = _parser.GetMostCommonTagName(xmlDoc);

            result.Should().Be("tag2");
        }

    }
}
