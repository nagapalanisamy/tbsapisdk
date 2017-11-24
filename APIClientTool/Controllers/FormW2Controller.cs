using APIClient.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APIClientTool.Controllers
{
    public class FormW2Controller : Controller
    {
        // GET: FormW2
        public ActionResult Index()
        {
            return View();
        }

        #region CreateFormW2Return
        public ActionResult CreateFormW2Return()
        {
            return View();
        }
        #endregion

        #region GetReturnCreateStatus
        public ActionResult GetReturnCreateStatus(FormW2 formw2)
        {
            var responseText = string.Empty;
            var requestText = JsonConvert.SerializeObject(formw2, Formatting.Indented);
            return Json(requestText, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}