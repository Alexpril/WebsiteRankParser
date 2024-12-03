using HtmlAgilityPack;
using WebsiteRank.Core.Data;
using WebsiteRank.Core.Data.Requests;

namespace WebsiteRank.Core.Helpers
{
    public static class GoogleResultParser
    {
        public static List<WebsiteRankDto> ExtractRankFromHtml(string htmlContent, WebsiteRankRequest request)
        {
            var parsedRanks = new List<WebsiteRankDto>();
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlContent);

            // Locate the main container for search results
            var mainContainer = htmlDocument.DocumentNode.SelectSingleNode("//div[@id='search']//div[@id='rso']");
            if (mainContainer == null) return parsedRanks;

            // Gather result blocks
            var resultBlocks = mainContainer.SelectNodes(".//div[contains(@class, 'g')]");
            if (resultBlocks == null) return parsedRanks;

            int rankPosition = 1;

            foreach (var block in resultBlocks)
            {
                // Find the link and title nodes within the block
                var linkNode = block.SelectSingleNode(".//a[@href]");
                var titleNode = block.SelectSingleNode(".//h3");

                if (linkNode == null || titleNode == null) continue;

                var extractedUrl = linkNode.GetAttributeValue("href", string.Empty);
                var extractedTitle = titleNode.InnerText.Trim();

                // Check if the URL matches the requested website URL
                if (extractedUrl.Contains(request.WebsiteUrl))
                {
                    parsedRanks.Add(new WebsiteRankDto
                    {
                        WebsiteUrl = request.WebsiteUrl,
                        ResultUrl = extractedUrl,
                        SearchKeyword = request.SearchKeyword,
                        Description = extractedTitle,
                        Position = rankPosition,
                        Date = DateTime.UtcNow
                    });
                }

                rankPosition++;
            }

            return parsedRanks;
        }
    }
}