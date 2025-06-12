using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblog.Domain.Models
{
    public class PodcastTag
    {
        public int PodcastId { get; set; }
        public Podcast? Podcast { get; set; }
        public int TagId { get; set; }
        public Tag? Tag { get; set; }
    }
}