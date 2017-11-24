using APIClientTool.Utilities;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Net.Http;
using APIClientTool.ViewModels;
using APIClientTool.Repository;

namespace APIClientTool.Controllers
{
    public class FormW2Controller : Controller
    {
        // GET: FormW2
        ReturnRepository _repository;
        public FormW2Controller()
        {
            _repository = new ReturnRepository();
        }
        public ActionResult Index()
        {
            return View();
        }

        #region CreateFormW2Return
        public ActionResult CreateFormW2Return(bool? id)
        {
            FormW2 formw2 = new FormW2();
            bool _prePopulate = id ?? false;
            if(_prePopulate)
            {
               
                formw2.Sequence = "1";
                //Mapping BusinessDetails
                formw2.Business = new BusinessDetails();
                formw2.Business.BusinessNm = "API Client Tool Team";
                formw2.Business.TradeNm = "API Client";
                formw2.Business.Email = "e990dev@expressexcise.com";
                formw2.Business.EIN = "123456789";
                formw2.Business.ContactNm = "Express Team";
                formw2.Business.Phone = "9841381515";
                formw2.Business.BusinessType = "ESTE";
                formw2.Business.SigningAuthority = new SigningAuthority();

                formw2.Business.SigningAuthority.Name = "Peter Samuel";
                formw2.Business.SigningAuthority.DayTimePhone = "9876543210";
                formw2.Business.SigningAuthority.BusinessMembers = "Owner";

                formw2.Business.KindOfEmployer = "Regular(941)";
                formw2.Business.EmploymentCd = "FederalGovt";
                formw2.Business.IsForeign = false;
                formw2.Business.Country = "US";
                formw2.Business.Address1 = "109 Pangbourne Way";
                formw2.Business.City = "Hanover";
                formw2.Business.USState = "MD";
                formw2.Business.USZip = "21076";

                //Mapping Employee
                formw2.Employee = new Employee();
                formw2.Employee.FirstNm = "Peter";
                formw2.Employee.LastNm = "Yengaran";
                formw2.Employee.IsForeign = false;
                formw2.Employee.Country = "US";
                formw2.Employee.Address1 = "First Street";
                formw2.Employee.City = "Rockhill";
                formw2.Employee.USState = "SC";
                formw2.Employee.USZip = "29727";
                formw2.Employee.Phone = "9884523450";
                formw2.Employee.Email = "peter@spanenterprises.com";

                //Mapping FormW2Details
                formw2.FormW2Details = new FormW2Details();
                formw2.FormW2Details.Box1 = 10000.00M;

                //Optional BusinessDetails
                formw2.Business.PhoneExtn = "";
                formw2.Business.Fax = "";
                formw2.Business.Address2 = "";
                formw2.Business.ProvinceState = "";
                formw2.Business.PostalCd = "";

                //Optional Employee
                formw2.Employee.Suffix = "";
                formw2.Employee.Fax = "";
                formw2.Employee.Address2 = "";
                formw2.Employee.ProvinceState = "";
                formw2.Employee.PostalCd = "";
            }
            return View(formw2);
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
            W2ReturnResponse response = new W2ReturnResponse();
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
                        response = new JavaScriptSerializer().Deserialize<W2ReturnResponse>(responseJson);
                        _repository.SaveAPIResponse(response);
                    }
                }
            }
            ViewBag.responseJson = responseJson;
            return PartialView();
        }
        #endregion

        #region Status
        public ActionResult APITestStatus()
        {
            using (var client = new PublicAPIClient())
            {
                string requestUri = "Values/Get/1";
                APIGenerateAuthHeader.GenerateAuthHeader(client, requestUri, "GET");
                var _response = client.GetAsync(requestUri,).Result;
                if (_response != null)
                {
                    var createResponse = _response.Content.ReadAsAsync<string>().Result;
                }
            }
            return PartialView();
        }
        #endregion

    }
}