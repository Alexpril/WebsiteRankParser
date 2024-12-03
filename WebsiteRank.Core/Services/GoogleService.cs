using WebsiteRank.Core.Data.Requests;
using WebsiteRank.Core.Data;
using WebsiteRank.Core.Constants;
using WebsiteRank.Core.Helpers;

namespace WebsiteRank.Core.Services;

public class GoogleService(IHttpClientFactory httpClientFactory) : IGoogleService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(WebsiteRankConstants.GoogleHttpClientName);

    public async Task<List<WebsiteRankDto>> GetWebsiteRankAsync(WebsiteRankRequest seoRequestDto)
    {
        var url = $"/search?num=100&q={Uri.EscapeDataString(seoRequestDto.SearchKeyword)}";

        string response = await _httpClient.GetStringAsync(url);

        return GoogleResultParser.ExtractRankFromHtml(response, seoRequestDto);
    }
}
