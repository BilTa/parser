using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ParserTests
{
    public class CommonTests
    {
        private Parser.Parser _parser;

        public CommonTests()
        {
            _parser = new Parser.Parser();
        }

        [Fact]
        public void UniqueNodes_EmptyXml_ReturnsAllUniqueTagNames()
        {
            var xml = "";
            var xmlDoc = _parser.LoadFromString(xml);

            var result = _parser.GetUniqueTagNames(xmlDoc);

            result.Should().BeEmpty();
        }

        [Fact]
        public void UniqueNodes_MalformedXml_ReturnsAllUniqueTagNames()
        {
            var xml = "tag1";
            var xmlDoc = _parser.LoadFromString(xml);

            var result = _parser.GetUniqueTagNames(xmlDoc);

            result.Should().BeEmpty();
        }
    }
}
