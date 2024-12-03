using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteRank.DAL.Models
{
    public class HighestWebsiteRank
    {
        public int Id { get; set; }
        public string SearchKeyword { get; set; }
        public string ResultUrl { get; set; }
        public string WebsiteUrl { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
        public DateTime Date { get; set; }
    }
}
