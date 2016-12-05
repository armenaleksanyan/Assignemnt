using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserStories.Web.Models
{
    public class StoryInfoModel
    {
        public long StoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime PostedOn { get; set; }
        public DateTime LastModified { get; set; }
        public long UserId { get; set; }
        public List<long> GroupIds { get; set; }
    }
}