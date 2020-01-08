using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Parser
{
    public class Parser : IParser
    {

        public HtmlDocument LoadFromWeb(string path)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(path);
            doc.OptionEmptyCollection = true;
            return doc;
        }

        public HtmlDocument LoadFromString(string content)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.OptionEmptyCollection = true;
            htmlDoc.LoadHtml(content);
            return htmlDoc;
        }

        public HtmlDocument LoadFromFile(string path)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.OptionEmptyCollection = true;
            htmlDoc.Load(path);
            return htmlDoc;
        }

        public IEnumerable<string> GetUniqueTagNames(HtmlDocument doc)
        {
            var tags = doc.DocumentNode.SelectNodes("//*");
            return tags.DistinctBy(node => node.Name).Select(node => node.Name);
        }

        public string GetMostCommonTagName(HtmlDocument doc)
        {
            var x = doc.DocumentNode.SelectNodes("//*");
            string name = x.GroupBy(node => node.Name)
                .Select(nodes => new 
                { 
                    Name = nodes.Key, 
                    Count = nodes.Count() 
                })
                .OrderByDescending(n => n.Count)
                .First()
                .Name;
            return name;
        }

        public IEnumerable<string> GetLongestPath(HtmlDocument doc)
        {
            var root = doc.DocumentNode;
            HtmlNode lastNode = root;
            int lastNodeDepth = 0;

            TraverseRecursively(root, 0, ref lastNode, ref lastNodeDepth);

            return MakePathFromLastNode(lastNodeDepth, lastNode);
        }

        public IEnumerable<string> GetLongestPathWithMostPopularTag(HtmlDocument doc)
        {
            var root = doc.DocumentNode;
            HtmlNode lastNode = root;
            int lastNodeDepth = 0;

            var mostPopularTag = GetMostCommonTagName(doc);
            int lastPopularTagCount = 0;

            TraverseRecursivelyForMostPopularTag(root, 0, 0, mostPopularTag, ref lastNode, ref lastNodeDepth, ref lastPopularTagCount);

            return MakePathFromLastNode(lastNodeDepth, lastNode);
        }

        private void TraverseRecursively(HtmlNode node, int currentDepth, ref HtmlNode lastNode, ref int lastNodeDepth)
        {
            if (node.NodeType == HtmlNodeType.Text || node.NodeType == HtmlNodeType.Comment)
            {
                return;
            }

            if (currentDepth > lastNodeDepth)
            {
                lastNode = node;
                lastNodeDepth = currentDepth;
            }

            if (node.HasChildNodes)
            {
                foreach(var childNode in node.ChildNodes)
                {
                    TraverseRecursively(childNode, currentDepth+1, ref lastNode, ref lastNodeDepth);
                }
            }
        }

        private void TraverseRecursivelyForMostPopularTag(HtmlNode node, int currentDepth, int currentTagCount, string tagName, ref HtmlNode lastNode, ref int lastNodeDepth, ref int lastNodeTagCount)
        {
            if (node.NodeType == HtmlNodeType.Text || node.NodeType == HtmlNodeType.Comment)
            {
                return;
            }

            if(node.Name == tagName)
            {
                currentTagCount++;
            }

            if ((currentTagCount > lastNodeTagCount) || (currentTagCount == lastNodeTagCount && currentDepth > lastNodeDepth))
            {
                lastNode = node;
                lastNodeDepth = currentDepth;
                lastNodeTagCount = currentTagCount;
            }

            if (node.HasChildNodes)
            {
                foreach (var childNode in node.ChildNodes)
                {
                    TraverseRecursivelyForMostPopularTag(childNode, currentDepth + 1, currentTagCount, tagName, ref lastNode, ref lastNodeDepth, ref lastNodeTagCount);
                }
            }
        }

        private IEnumerable<string> MakePathFromLastNode(int depth, HtmlNode lastNode)
        {
            List<string> path = new List<string>(depth);
            HtmlNode tempNode = lastNode;
            path.Add(tempNode.Name);
            while (tempNode.ParentNode != null && tempNode.ParentNode.NodeType != HtmlNodeType.Document)
            {
                tempNode = tempNode.ParentNode;
                path.Add(tempNode.Name);
            }
            path.Reverse();

            return path;
        }

    }
}
