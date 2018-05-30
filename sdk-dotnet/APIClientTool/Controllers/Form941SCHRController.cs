using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APIClientTool.ViewModels.Form941SCHR;

namespace APIClientTool.Controllers
{
    public class Form941SCHRController : Controller
    {
        #region Form 941 View Get Method
        // GET: Form941SCHR
        [Route("form941schr")]
        public ActionResult Index()
        {
            Form941SchRRecords form941SchRRecords = new Form941SchRRecords();
            return View(form941SchRRecords);
        }
        #endregion
    }
}