using FluentAssertions;
using Xunit;

namespace ParserTests
{
    public class LongestPathWithMostPopulatTagTests
    {
        private Parser.Parser _parser;

        public LongestPathWithMostPopulatTagTests()
        {
            _parser = new Parser.Parser();
        }

        [Fact]
        public void GetLongestPathWithMostPopularTag_OneTag_ReturnsSingleTag()
        {
            var xml = @"<notes></notes>";
            var xmlDoc = _parser.LoadFromString(xml);

            var result = _parser.GetLongestPathWithMostPopularTag(xmlDoc);

            result.Should().BeEquivalentTo("notes");
        }

        [Fact]
        public void GetLongestPathWithMostPopularTag_AllTagsUnique_ReturnsLongestPath()
        {
            var xml = @"<notes>
<tag1><tag2></tag2></tag1>
</notes>";
            var xmlDoc = _parser.LoadFromString(xml);

            var result = _parser.GetLongestPathWithMostPopularTag(xmlDoc);

            result.Should().BeEquivalentTo("notes", "tag1", "tag2");
        }

        [Fact]
        public void GetLongestPathWithMostPopularTag_AllTagsUniqueAndMultipleLongestPaths_ReturnsFirstLongestPath()
        {
            var xml = @"<notes>
<tag1><tag2></tag2></tag1>
<tag11><tag22></tag22></tag11>
</notes>";
            var xmlDoc = _parser.LoadFromString(xml);

            var result = _parser.GetLongestPathWithMostPopularTag(xmlDoc);

            result.Should().BeEquivalentTo("notes", "tag1", "tag2");
        }

        [Fact]
        public void GetLongestPathWithMostPopularTag_MostPopularTagInSecondPath_ReturnsSecondLongestPath()
        {
            var xml = @"<notes>
<tag22></tag22><tag22></tag22>
<tag1><tag2></tag2></tag1>
<tag11><tag22></tag22></tag11>
</notes>";
            var xmlDoc = _parser.LoadFromString(xml);

            var result = _parser.GetLongestPathWithMostPopularTag(xmlDoc);

            result.Should().BeEquivalentTo("notes", "tag11", "tag22");
        }

        [Fact]
        public void GetLongestPathWithMostPopularTag_MostPopularTagInShorterPath_ReturnsShorterPathWithMostPopularTag()
        {
            var xml = @"<notes>
<tag22></tag22><tag22></tag22>
<tag1><tag2><tag3><tag4></tag4></tag3></tag2></tag1>
<tag11><tag22></tag22></tag11>
</notes>";
            var xmlDoc = _parser.LoadFromString(xml);

            var result = _parser.GetLongestPathWithMostPopularTag(xmlDoc);

            result.Should().BeEquivalentTo("notes", "tag11", "tag22");
        }
    }
}
