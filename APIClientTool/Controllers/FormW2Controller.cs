using APIClientTool.Utilities;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Net.Http;
using APIClientTool.ViewModels;

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
            var responseJson = string.Empty;
            var requestText = JsonConvert.SerializeObject(formw2, Formatting.Indented);
            using (var client = new PublicAPIClient())
            {
                var createRequest = new JavaScriptSerializer().Deserialize<Object>(requestText);
                string requestUri = "Return/Create";
                APIGenerateAuthHeader.GenerateAuthHeader(client, requestUri, "POST");
                var _response = client.PostAsJsonAsync(requestUri, createRequest).Result;
                if (_response != null)
                {
                    var createResponse = _response.Content.ReadAsAsync<Object>().Result;
                    if (createResponse != null)
                    {
                        responseJson = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                    }
                }

            }
            return Json(responseJson, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Status

        public ActionResult APIResponseStatus(FormW2 formw2)
        {
            var responseJson = string.Empty;
            var requestText = JsonConvert.SerializeObject(formw2, Formatting.Indented);
            using (var client = new PublicAPIClient())
            {
                var createRequest = new JavaScriptSerializer().Deserialize<Object>(requestText);
                string requestUri = "Return/Create";
                APIGenerateAuthHeader.GenerateAuthHeader(client, requestUri, "POST");
                var _response = client.PostAsJsonAsync(requestUri, createRequest).Result;
                if (_response != null)
                {
                    var createResponse = _response.Content.ReadAsAsync<Object>().Result;
                    if (createResponse != null)
                    {
                        responseJson = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                    }
                }
            }
            ViewBag.responseJson = responseJson;
            return PartialView();
        }
        #endregion

    }
}