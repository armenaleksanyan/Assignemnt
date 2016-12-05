using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStories.Data.Entities;

namespace UserStories.Data.EntityMaps
{
    public class StoryMap : EntityTypeConfiguration<Story>
    {
        public StoryMap()
        {
            HasRequired(v => v.User).WithMany(v => v.Stories).HasForeignKey(v => v.UserId);            
            Property(v => v.Title).HasMaxLength(250).IsRequired().IsUnicode();
            Property(v => v.Description).HasMaxLength(500).IsRequired().IsUnicode();
            Property(v => v.Content).IsMaxLength().IsRequired().IsUnicode();
        }
    }
}
