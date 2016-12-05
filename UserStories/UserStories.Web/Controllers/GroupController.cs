using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserStories.Business.Managers;
using UserStories.Web.Models;
using UserStories.Web.Helpers;

namespace UserStories.Web.Controllers
{
    public class GroupController : BaseController
    {
        public ActionResult Groups()
        {
            return View();
        }
        public ActionResult AddNewGroup()
        {
            return PartialView("_AddNewGroup");
        }
        public JsonResult GetGroupInfo()
        {
            using (GroupManager manager = new GroupManager(UserId))
            {
                try
                {
                    var data = manager.GetGroups();
                    List<GroupInfoModel> model = new List<GroupInfoModel>();
                    data.ForEach(v =>
                    {
                        model.Add(v.ToGroupInfoModel());
                    });
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    manager.ErrorLog(ex);
                    return Json(new { ErrorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult SaveGroup(GroupModel model)
        {
            using (GroupManager manager = new GroupManager(UserId))
            {
                try
                {
                    bool isSave = manager.InsertOrUpdateGroup(model.ToGroup());
                    if (!isSave)
                        return Json(new { ErrorMessage = "Can't save group. Please try again." }, JsonRequestBehavior.AllowGet);

                    return Json(new { }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    manager.ErrorLog(ex);
                    return Json(new { ErrorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}
