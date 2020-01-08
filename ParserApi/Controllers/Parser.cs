using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Parser;
using Parser.Contract;

namespace ParserApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Parser : ControllerBase
    {
        private readonly ILogger<Parser> _logger;
        private readonly IParser _parser;

        public Parser(ILogger<Parser> logger, IParser parser)
        {
            _logger = logger;
            _parser = parser;
        }

        [HttpGet]
        public StatisticsDto GetStatistics(string url)
        {
            try
            {
                if (!url.ToLower().StartsWith("http"))
                {
                    url = "http://" + url;
                }

                var doc = _parser.LoadFromWeb(url);
                return new StatisticsDto
                {
                    MostPopularTag = _parser.GetMostCommonTagName(doc),
                    UniqueTags = _parser.GetUniqueTagNames(doc),
                    LongestPath = _parser.GetLongestPath(doc),
                    LongestPathWithMostPopularTag = _parser.GetLongestPathWithMostPopularTag(doc)
                };
            } catch(Exception e)
            {
                _logger.LogError(e, e.Message);
                return new StatisticsDto
                {
                    MostPopularTag = "",
                    UniqueTags = Enumerable.Empty<string>(),
                    LongestPath = Enumerable.Empty<string>(),
                    LongestPathWithMostPopularTag = Enumerable.Empty<string>()
                };
            }

            
        }
    }
}
