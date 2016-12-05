using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStories.Business.Bases;
using UserStories.DTO.Group;

namespace UserStories.Business.Managers
{
    public class GroupManager : BaseManager
    {
        public GroupManager(long userId)
            : base(userId)
        {

        }

        public List<GroupInfo> GetGroups()
        {
            return Context.Groups.Select(v => new GroupInfo
            {
                Description = v.Description,
                Id = v.GroupId,
                Name = v.Name,
                MembersCount = v.Stories.Count,
                StoriesCount = v.Stories.Count
            }).ToList();
        }

        public bool InsertOrUpdateGroup(Group group)
        {
            try
            {
                var item = new UserStories.Data.Entities.Group();
                if (group.Id == 0)
                {
                    item.Description = group.Description;
                    item.Name = group.Name;
                    Context.Groups.Add(item);
                    Context.Entry(item).State = EntityState.Added;
                    Context.SaveChanges();
                }
                else
                {
                    item.GroupId = group.Id;
                    item.Description = group.Description;
                    item.Name = group.Name;
                    Context.Groups.Attach(item);
                    Context.Entry(item).State = EntityState.Modified;
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                base.ErrorLog(ex);
                return false;
            }

        }

        public bool DeleteGroup(int id)
        {
            try
            {
                var group = Context.Groups.FirstOrDefault(b => b.GroupId == id);
                Context.Entry(group).State = EntityState.Modified;
                Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                base.ErrorLog(ex);
                return false;
            }
        }
    }
}
