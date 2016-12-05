using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserStories.Business.Managers;
using UserStories.Web.Models;

namespace UserStories.Web.Controllers
{
    public class CommonController : BaseController
    {
        [HttpGet]
        public JsonResult GetGroups()
        {
            using (GroupManager manager = new GroupManager(UserId))
            {
                List<SimpleModel> model = new List<SimpleModel>();
                var dbModel = manager.GetGroups();
                foreach (var item in dbModel)
                {
                    model.Add(new SimpleModel
                    {
                        Text = item.Name,
                        Value = item.Id                        
                    });
                }
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
