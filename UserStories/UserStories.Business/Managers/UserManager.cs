using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStories.Business.Bases;
using UserStories.Data.Context;
using UserStories.DTO.User;

namespace UserStories.Business.Managers
{
    public class UserManager : BaseManager
    {
        public UserManager(long userId)
            : base(userId)
        {

        }

        public User Authentication(string userName, string password)
        {
            try
            {
                var user = Context.Users.FirstOrDefault(v => v.UserName == userName && v.Password == password);
                return new User
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id = user.UserId,
                    UserName = user.UserName
                };

            }
            catch (Exception ex)
            {
                base.ErrorLog(ex);
                return new User();
            }
        }

        public List<User> GetUsers(string name)
        {
            try
            {
                return Context.Users.Where(v => !string.IsNullOrWhiteSpace(name) && v.FirstName.StartsWith(name)).Select(v => new User
                {
                    FirstName = v.FirstName,
                    Id = v.UserId,
                    LastName = v.LastName,
                    UserName = v.UserName

                }).ToList();
            }
            catch (Exception ex)
            {
                base.ErrorLog(ex);
                return new List<User>();
            }
        }

        public bool InsertOrUpdateUser(User user)
        {
            try
            {
                var item = new UserStories.Data.Entities.User();
                if (user.Id == 0)
                {
                    if (Context.Users.FirstOrDefault(v => v.UserName == user.UserName) == null)
                    {
                        item.FirstName = user.FirstName;
                        item.LastName = user.LastName;
                        item.UserName = user.UserName;
                        item.Password = user.Password;
                        Context.Entry(item).State = EntityState.Added;
                        Context.SaveChanges();
                    }
                    else { return false; }
                }
                else
                {
                    item.UserId = user.Id;
                    item.UserName = user.UserName;
                    item.FirstName = user.FirstName;
                    item.LastName = user.LastName;
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

        public bool DeleteUser(int id)
        {
            try
            {
                var user = Context.Users.FirstOrDefault(b => b.UserId == id);
                Context.Users.Remove(user);
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
