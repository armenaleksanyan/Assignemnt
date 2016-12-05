﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using UserStories.Web.Filters;
using UserStories.Web.Models;
using UserStories.Business.Managers;
using UserStories.Web.Utilities;
using UserStories.Web.Helpers;

namespace UserStories.Web.Controllers
{
    public class AccountController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            SessionManager.UserInfo = new UserInfo();           
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (TryToLogin(model))
            {
                return RedirectToLocal(returnUrl);
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (UserManager manager = new UserManager(UserId))
                    {
                        if (manager.InsertOrUpdateUser(new DTO.User.User
                        {
                            FirstName = model.LastName,
                            LastName = model.LastName,
                            UserName = model.UserName,
                            Password = model.Password.GetMd5Hash()
                        }))
                        {
                            if (!TryToLogin(new LoginModel { UserName = model.UserName, Password = model.Password }))
                            {
                                ModelState.AddModelError("", "The user name or password provided is incorrect.");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Can not register Account");

                        }
                    }

                    return RedirectToAction("Stories", "Story");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private bool TryToLogin(LoginModel model)
        {
            using (UserManager manager = new UserManager(UserId))
            {
                try
                {
                    var logedUser = manager.Authentication(model.UserName, model.Password.GetMd5Hash());
                    if (logedUser.Id != 0)
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, false);
                        HttpContext.Session.Timeout = 25;
                        SessionManager.UserInfo = new UserInfo
                        {
                            FirstName = logedUser.FirstName,
                            LastName = logedUser.LastName,
                            UserName = logedUser.UserName,
                            Id = logedUser.Id
                        };
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    manager.ErrorLog(ex);
                    return false;
                }

            }
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Stories", "Story");
            }
        }
               

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}