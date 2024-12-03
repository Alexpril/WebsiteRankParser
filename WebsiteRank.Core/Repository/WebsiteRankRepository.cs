using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteRank.Core.Data;
using WebsiteRank.Core.Data.Requests;
using WebsiteRank.DAL.Contexts;
using WebsiteRank.DAL.Models;

namespace WebsiteRank.Core.Repository
{
    public class WebsiteRankRepository : IWebsiteRankRepository
    {
        private readonly WebsiteRankDbContext _dbContext;

        public WebsiteRankRepository(WebsiteRankDbContext dbContext) => _dbContext = dbContext;

        public async Task RegisterHighestWebsiteRankFoundAsync(WebsiteRankDto dto)
        {
            var entity = new HighestWebsiteRank
            {
                SearchKeyword = dto.SearchKeyword,
                ResultUrl = dto.ResultUrl,
                WebsiteUrl = dto.WebsiteUrl,
                Description = dto.Description,
                Position = dto.Position,
                Date = dto.Date
            };

            _dbContext.HighestWebsiteRanks.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<WebsiteRankDto>> GetSearchHistoryAsync()
        {
            return await _dbContext.HighestWebsiteRanks
                .Select(rank => new WebsiteRankDto
                {
                    SearchKeyword = rank.SearchKeyword,
                    ResultUrl = rank.ResultUrl,
                    WebsiteUrl = rank.WebsiteUrl,
                    Description = rank.Description,
                    Position = rank.Position,
                    Date = rank.Date
                })
                .ToListAsync();
        }
    }

}
