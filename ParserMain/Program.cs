using System;
using HtmlAgilityPack;
using Parser;

namespace ParserMain
{
    public class Program
    {
        static void Main(string[] args)
        {
            if(args.Length < 1)
            {
                Console.WriteLine("no url given");
                return;
            }

            Uri path = new Uri(args[0]);
            var parser = new Parser.Parser();
            HtmlDocument doc;

            try
            {
                if (path.IsFile)
                {
                    doc = parser.LoadFromFile(path.AbsolutePath);
                }
                else
                {
                    doc = parser.LoadFromWeb(path.AbsoluteUri);
                }
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            Console.WriteLine("Unique nodes:");
            foreach(var name in parser.GetUniqueTagNames(doc))
            {
                Console.Write(name + " ");
            }
            Console.WriteLine();

            Console.WriteLine("Most popular tag: " + parser.GetMostCommonTagName(doc));

            Console.WriteLine("Longest path:");
            foreach(var name in parser.GetLongestPath(doc))
            {
                Console.Write(name + " ");
            }
            Console.WriteLine();

            Console.WriteLine("Longest path with most popular tag:");
            foreach (var name in parser.GetLongestPathWithMostPopularTag(doc))
            {
                Console.Write(name + " ");
            }
            Console.WriteLine();

            Console.WriteLine("Finish...");
        }
    }
}
