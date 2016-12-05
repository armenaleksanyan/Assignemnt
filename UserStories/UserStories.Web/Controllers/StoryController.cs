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
    public class StoryController : BaseController
    {
        public ActionResult Stories()
        {
            return View();
        }
        public ActionResult AddNewStories()
        {
            return PartialView("_AddNewStories");
        }
        public ActionResult ViewStory()
        {
            return PartialView("_ViewStory");
        }
        public JsonResult GetStories()
        {
            using (StoryManager manager = new StoryManager(UserId))
            {
                try
                {
                    List<StoryExtendedDataModel> storyModel = new List<StoryExtendedDataModel>();
                    var result = manager.GetStories();
                    result.ForEach(item =>
                    {
                        storyModel.Add(item.ToStoryExtendedDataModel());
                    });
                    return Json(storyModel, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    manager.ErrorLog(ex);
                    return Json(new { ErrorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult SaveStory(StoryModel model)
        {
            using (StoryManager manager = new StoryManager(UserId))
            {
                try
                {
                    var isSaved = manager.InsertOrUpdateStory(model.ToStory());
                    if (!isSaved)
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