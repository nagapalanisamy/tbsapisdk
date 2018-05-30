using APIClientTool.Utilities;
using APIClientTool.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APIClientTool.Controllers
{
    public class EfileController : Controller
    {

        #region Get EFile Status
        /// <summary>
        /// Get EFile Status
        /// </summary>
        /// <returns></returns>
        public ActionResult _GetEFileStatus(int? id)
        {
            if (id == (int)FormType.FormW2)
            {
                return PartialView(APISession.GetAPIResponse());
            }
            else if (id == (int)FormType.Form941)
            {
                return PartialView(APISession.GetForm941APIResponse());
            }
            else if (id == (int)FormType.Form941SCHR)
            {
                return PartialView(APISession.GetForm941APIResponse());
            }
            return PartialView();
        }
        #endregion

        #region  EFile Status
        /// <summary>
        /// Function loads list of all SubmissionIds created in Form941Return page
        /// </summary>
        /// <returns>List of all SubmissionIds</returns>
        public ActionResult EFileStatus()
        {
            return View(APISession.GetAPIResponse());
        }
        #endregion

    }
}