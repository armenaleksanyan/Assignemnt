using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStories.DTO.Group
{
    public class GroupInfo : Group
    {
        public int MembersCount { get; set; }
        public int StoriesCount { get; set; }
    }
}
