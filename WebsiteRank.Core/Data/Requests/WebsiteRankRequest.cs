using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteRank.Core.Data.Requests
{
    public class WebsiteRankRequest
    {
        public required string SearchKeyword { get; set; }

        public required string WebsiteUrl { get; set; }
    }
}
