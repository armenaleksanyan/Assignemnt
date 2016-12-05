using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStories.Business.Bases;
using UserStories.DTO.Group;
using UserStories.DTO.Story;

namespace UserStories.Business.Managers
{
    public class StoryManager : BaseManager
    {
        public StoryManager(long userId)
            : base(userId)
        {

        }

        public List<StoryExtendedData> GetStories()
        {
            try
            {
                return Context.Stories.Where(v => v.UserId == UserId).Select(v => new StoryExtendedData
                 {
                     Content = v.Content,
                     Description = v.Description,
                     Id = v.StoryId,
                     LastModified = v.LastModified,
                     PostedOn = v.PostedOn,
                     Title = v.Title,
                     UserId = v.UserId,
                     Groups = v.Groups.Select(g => new Group
                     {
                         Description = g.Description,
                         Id = g.GroupId,
                         Name = g.Name
                     }).ToList(),
                     User = new DTO.User.User
                     {
                         FirstName = v.User.FirstName,
                         Id = v.User.UserId,
                         LastName = v.User.LastName,
                         UserName = v.User.UserName
                     }

                 }).ToList();
            }
            catch (Exception ex)
            {
                base.ErrorLog(ex);
                return new List<StoryExtendedData>();
            }
        }

        public bool InsertOrUpdateStory(Story story)
        {
            try
            {
                var item = Context.Stories.FirstOrDefault(v => v.StoryId == story.Id) ?? new UserStories.Data.Entities.Story();
                var groups = Context.Groups.Where(v => story.GroupIds.Contains(v.GroupId));
               
                foreach (var it in groups)
                {
                    item.Groups.Add(it);
                }
                item.Content = story.Content;
                item.Description = story.Description;
                item.PostedOn = story.PostedOn;
                item.LastModified = DateTime.Now;
                item.Title = story.Title;
                item.UserId = UserId;
                item.User = Context.Users.FirstOrDefault(v => v.UserId == UserId);
                item.StoryId = story.Id;
                if (story.Id == 0)
                {
                    Context.Entry(item).State = EntityState.Added;
                    Context.SaveChanges();
                }
                else
                {
                    Context.Entry(item).State = EntityState.Modified;
                    Context.SaveChanges();
                    return true;
                }

                return true;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);

                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            catch (Exception ex1)
            {
                base.ErrorLog(ex1);
                return false;
            }
        }
        
        public bool DeleteStory(int id)
        {
            try
            {
                var story = Context.Stories.FirstOrDefault(v => v.StoryId == id);
                Context.Entry(story).State = EntityState.Deleted;
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
