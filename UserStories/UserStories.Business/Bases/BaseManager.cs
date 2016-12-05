using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStories.Data.Context;
using UserStories.Data.Entities;

namespace UserStories.Business.Bases
{
    public class BaseManager : IDisposable
    {
        protected UserStoriesContext Context { get; private set; }
        public long UserId { get; private set; }

        public bool IsDbExist { get; private set; }

        public BaseManager(long userId)
        {
            this.UserId = userId;
            Context = new UserStoriesContext();
        }

        public BaseManager()
        {
            IsDbExist = true;
            Database.SetInitializer<UserStoriesContext>(null);
            using (Context = new UserStoriesContext())
            {
                if (!Context.Database.Exists())
                {
                    IsDbExist = false;
                    // Create the SimpleMembership database without Entity Framework migration schema
                    ((IObjectContextAdapter)Context).ObjectContext.CreateDatabase();
                }
            }
        }

        private bool isDisposed;

        ~BaseManager()
        {
            Dispose(true);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!isDisposed && disposing)
            {
                Context.Dispose();
            }

            isDisposed = true;
        }

        public void ErrorLog(Exception exception) 
        {
            string message = exception.InnerException != null ? String.Format("Inner Exception is {0}, default is {1}", exception.InnerException.Message, exception.Message) : exception.Message;
            message = String.Format("message = {0}, Source= {1}, StackTrace = {2}, TargetSite={3}", message, exception.Source, exception.StackTrace, exception.TargetSite);
            Context.ErrorLog.Add(new ErrorLog
            {
                UserId =UserId,
                RecDate = DateTime.Now,
                Message = message,                
            });
            Context.SaveChanges();
        }
    }
}
