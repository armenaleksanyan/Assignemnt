
using System.Data.Entity;
using UserStories.Data.Entities;
using UserStories.Data.EntityMaps;
namespace UserStories.Data.Context
{
    public class UserStoriesContext: DbContext
    {
        public UserStoriesContext()
            : base("name=DefaultConnection")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<ErrorLog> ErrorLog { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new StoryMap());
            modelBuilder.Configurations.Add(new GroupMap());
        }
    }
}
