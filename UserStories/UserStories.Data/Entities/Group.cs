using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStories.Data.Entities
{
    public class Group
    {
        public Group()
        {
            Stories = new HashSet<Story>();
        }
        public long GroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }        
        public virtual ICollection<Story> Stories { get; set; }
    }
}
