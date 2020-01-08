using FluentAssertions;
using Xunit;

namespace ParserTests
{
    public class LongestPathTests
    {
        private Parser.Parser _parser;

        public LongestPathTests()
        {
            _parser = new Parser.Parser();
        }

        [Fact]
        public void GetLongestPath_MultipleLongestPaths_ReturnsFirstLongestPath()
        {
            var xml = @"<notes>
<tag1><tag2></tag2></tag1>
<tag3><tag4></tag4></tag3>
</notes>";
            var xmlDoc = _parser.LoadFromString(xml);

            var result = _parser.GetLongestPath(xmlDoc);

            result.Should().BeEquivalentTo("notes", "tag1", "tag2");
        }

        [Fact]
        public void GetLongestPath_SingleLongestPaths_ReturnsLongestPath()
        {
            var xml = @"<notes>
<tag1><tag2></tag2></tag1>
<tag3><tag4></tag4></tag3>
<tag11><tag22><tag33></tag33></tag22></tag11>
</notes>";
            var xmlDoc = _parser.LoadFromString(xml);

            var result = _parser.GetLongestPath(xmlDoc);

            result.Should().BeEquivalentTo("notes", "tag11", "tag22", "tag33");
        }

        [Fact]
        public void GetLongestPath_OneTag_ReturnsOneTag()
        {
            var xml = @"<notes></notes>";
            var xmlDoc = _parser.LoadFromString(xml);

            var result = _parser.GetLongestPath(xmlDoc);

            result.Should().BeEquivalentTo("notes");
        }
    }
}
