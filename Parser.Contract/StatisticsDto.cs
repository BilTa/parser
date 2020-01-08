using System.Collections.Generic;

namespace Parser.Contract
{
    public class StatisticsDto
    {
        public string MostPopularTag { get; set; }
        public IEnumerable<string> UniqueTags { get; set; }
        public IEnumerable<string> LongestPath { get; set; }
        public IEnumerable<string> LongestPathWithMostPopularTag { get; set; }
    }
}
