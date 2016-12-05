using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStories.Data.Entities;

namespace UserStories.Data.EntityMaps
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            Property(v => v.FirstName).HasMaxLength(50).IsRequired().IsUnicode();
            Property(v => v.LastName).HasMaxLength(50).IsRequired().IsUnicode();
        }
    }
}
