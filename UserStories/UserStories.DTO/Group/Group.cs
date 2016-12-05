using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStories.DTO.Bases;

namespace UserStories.DTO.Group
{
    public class Group : IdentityOwnerEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
