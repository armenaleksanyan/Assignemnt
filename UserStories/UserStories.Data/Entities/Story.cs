using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStories.Data.Entities
{
    public class Story
    {
        public Story()
        {
            User = new User();
            Groups = new HashSet<Group>();
        }
        public long StoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime PostedOn { get; set; }
        public DateTime LastModified { get; set; }
        public long UserId { get; set; }
        public virtual  User User { get; set; }
        public virtual ICollection<Group> Groups { get; set; }

    }
}
