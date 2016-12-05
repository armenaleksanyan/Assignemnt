using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStories.Data.Entities
{
    public class User
    {
        public User()
        {
            Stories = new HashSet<Story>();
        }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set;}
        public virtual ICollection<Story> Stories { get; set; }
    }
}
