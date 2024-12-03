using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteRank.Core.Data;
using WebsiteRank.Core.Data.Requests;

namespace WebsiteRank.Core.Repository
{
    public interface IWebsiteRankRepository
    {
        public Task RegisterHighestWebsiteRankFoundAsync(WebsiteRankDto dto);

        public Task<List<WebsiteRankDto>> GetSearchHistoryAsync();
    }
}
