using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteRank.Core.Data;
using WebsiteRank.Core.Data.Requests;

namespace WebsiteRank.Core.Services
{
    public interface IGoogleService
    {
        public Task<List<WebsiteRankDto>> GetWebsiteRankAsync(WebsiteRankRequest seoRequestDto);
    }
}
