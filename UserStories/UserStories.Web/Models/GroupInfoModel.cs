using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserStories.Web.Models
{
    public class GroupInfoModel : GroupModel
    {
        public string GroupInfo { get { return Name + " " + Description; } }
        public int MembersCount { get; set; }
        public int StoriesCount { get; set; }
    }
}