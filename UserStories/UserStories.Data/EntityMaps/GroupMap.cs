using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStories.Data.Entities;

namespace UserStories.Data.EntityMaps
{
    public class GroupMap : EntityTypeConfiguration<Group>
    {
        public GroupMap()
        {
            Property(v => v.Name).HasMaxLength(50).IsRequired().IsUnicode();
            Property(v => v.Description).HasMaxLength(500).IsRequired().IsUnicode();
        }
    }
}
