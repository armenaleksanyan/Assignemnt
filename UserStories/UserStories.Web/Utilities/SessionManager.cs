using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserStories.Web.Models;

namespace UserStories.Web.Utilities
{
    public class SessionManager
    {
        public static UserInfo UserInfo
        {
            get
            {
                return HttpContext.Current.Session[Constants.SessionKey.USER_SESSION_OBJECT] as UserInfo;
            }
            set
            {
                HttpContext.Current.Session[Constants.SessionKey.USER_SESSION_OBJECT] = value;

                // For cashing, cause Session is not available in GetVaryByCustomString
                HttpCookie cookie = new HttpCookie(Constants.SessionKey.USER_SESSION_ID, value.UserName);
                HttpContext.Current.Response.Cookies.Remove(Constants.SessionKey.USER_SESSION_ID);
                HttpContext.Current.Response.SetCookie(cookie);
            }
        }
    }
}