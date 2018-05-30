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
        #region Form 941SCHR View Get Method
        // GET: Form941SCHR
        [Route("form941schr")]
        public ActionResult Index()
        {
            Form941SCHRRecords form941SCHRRecords = new Form941SCHRRecords();
            return View(form941SCHRRecords);
        }
        #endregion
    }
}