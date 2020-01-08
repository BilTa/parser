using HtmlAgilityPack;
using System.Collections.Generic;

namespace Parser
{
    public interface IParser
    {
        HtmlDocument LoadFromWeb(string path);

        HtmlDocument LoadFromString(string content);

        HtmlDocument LoadFromFile(string path);

        IEnumerable<string> GetUniqueTagNames(HtmlDocument doc);

        string GetMostCommonTagName(HtmlDocument doc);

        IEnumerable<string> GetLongestPath(HtmlDocument doc);

        IEnumerable<string> GetLongestPathWithMostPopularTag(HtmlDocument doc);
    }
}
