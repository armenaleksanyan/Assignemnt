using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserStories.Web.Models
{
    public class StoryExtendedDataModel : StoryModel
    {
        public UserInfo User { get; set; }
        public List<GroupModel> Groups { get; set; }
    }
}