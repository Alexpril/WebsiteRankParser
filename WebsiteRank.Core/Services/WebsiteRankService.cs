using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteRank.Core.Data;
using WebsiteRank.Core.Data.Requests;
using WebsiteRank.Core.Repository;

namespace WebsiteRank.Core.Services
{

    public class WebsiteRankService(IGoogleService googleScrapingService,
        IWebsiteRankRepository websiteRankRepository) : IWebsiteRankService
    {
        private readonly IGoogleService _scraper = googleScrapingService;
        private readonly IWebsiteRankRepository _websiteRankRepository = websiteRankRepository;

        public async Task<List<WebsiteRankDto>> GetWebsiteRankAsync(WebsiteRankRequest seoRequestDto)
        {
            var results = await _scraper.GetWebsiteRankAsync(seoRequestDto);

            // saving the highest ranked search result to database
            if (results.Count > 0) _websiteRankRepository?.RegisterHighestWebsiteRankFoundAsync(
                results.MinBy(result => result.Position)!);

            return results;
        }

        public async Task<List<WebsiteRankDto>> GetSearchHistoryAsync()
        {
            return await _websiteRankRepository.GetSearchHistoryAsync();
        }
    }
}
