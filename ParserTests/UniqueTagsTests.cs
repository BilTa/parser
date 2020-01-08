using FluentAssertions;
using Xunit;

namespace ParserTests
{
    public class UniqueTagsTests
    {
        private Parser.Parser _parser;

        public UniqueTagsTests()
        {
            _parser = new Parser.Parser();
        }

        [Fact]
        public void UniqueNodes_returnsAllUniqueTagNames()
        {
            var xml = @"<notes>
<note><to>Tove</to><from>Jani</from><heading>Reminder</heading><body>Don't forget me this weekend!</body></note>
</notes>";
            var xmlDoc = _parser.LoadFromString(xml);

            var result = _parser.GetUniqueTagNames(xmlDoc);

            result.Should().BeEquivalentTo("notes", "note", "to", "from", "heading", "body");
        }

        [Fact]
        public void UniqueNodes_duplicateTagsExists_returnsAllUniqueTagNames()
        {
            var xml = @"<notes>
<note><to>Tove</to><from>Jani</from><heading>Reminder</heading><body>Don't forget me this weekend!</body></note>
<note><to>Tove</to><from>Jani</from><heading>Reminder</heading><body>Don't forget me this weekend!</body></note>
</notes>";
            var xmlDoc = _parser.LoadFromString(xml);

            var result = _parser.GetUniqueTagNames(xmlDoc);

            result.Should().BeEquivalentTo("notes", "note", "to", "from", "heading", "body");
        }

        [Fact]
        public void UniqueNodes_shortCloseFormExists_returnsAllUniqueTagNames()
        {
            var xml = @"<notes>
<note><to>Tove</to><from>Jani</from><heading>Reminder</heading><body>Don't forget me this weekend!</body></note>
<note />
</notes>";
            var xmlDoc = _parser.LoadFromString(xml);

            var result = _parser.GetUniqueTagNames(xmlDoc);

            result.Should().BeEquivalentTo("notes", "note", "to", "from", "heading", "body");
        }


    }
}
