using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserStories.Web.Models
{
    public class GroupModel
    {
        public long GroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}