using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStories.DTO.Bases;

namespace UserStories.DTO.Story
{
    public class Story : IdentityOwnerEntity
    {        
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime PostedOn { get; set; }
        public DateTime LastModified { get; set; }
        public long UserId { get; set; }
        public List<long> GroupIds { get; set; }        
    }
}
