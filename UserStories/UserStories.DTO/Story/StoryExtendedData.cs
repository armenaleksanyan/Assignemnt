using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStories.DTO.User;

namespace UserStories.DTO.Story
{
    public class StoryExtendedData : Story
    {
        public User.User User { get; set; }
        public List<Group.Group> Groups { get; set; }
    }
}
