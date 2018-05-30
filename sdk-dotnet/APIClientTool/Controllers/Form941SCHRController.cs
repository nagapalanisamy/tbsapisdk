using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APIClientTool.ViewModels.Form941SCHR;
using APIClientTool.ViewModels.Form941CoreModel;
using Newtonsoft.Json;
using APIClientTool.Utilities;
using System.Net.Http;
using System.Web.Script.Serialization;

namespace APIClientTool.Controllers
{
    public class Form941SCHRController : Controller
    {
        #region Form 941SCHR View Get Method
        // GET: Form941SCHR
        [Route("form941schr")]
        public ActionResult Index()
        {
            Form941SchRRecords form941SCHRRecords = new Form941SchRRecords();
            return View(form941SCHRRecords);
        }
        #endregion

        #region Form 941SchR Create Return Response Status
        /// <summary>
        /// Function inputs Form 941SchR details, POST all those details to the API and returns the response.
        /// Successful response contains SubmissionId, StatusCode and RecordSuccessStatus details (Sequence, RecordId, RecordStatus etc)
        /// Error response contains StatusCode and RecordErrorStatus details (RecordId, Sequence and list of Error information such as Code, Name, Message and Type)
        /// </summary>
        /// <param name="form941SchRRecords">Form 941SchRdetails passed through form941SchRRecords parameter</param>
        /// <returns>Form941SchRCreateReturnResponse</returns>
        [HttpPost]
        public ActionResult APIResponseStatus(Form941SchRRecords form941SchRRecords)
        {
            //Hardcoded values for Sequence
            var responseJson = string.Empty;

            form941SchRRecords.Sequence = "Record1";
            form941SchRRecords.RecordId = null;
            form941SchRRecords.ReturnHeader.ReturnType = null;
            form941SchRRecords.ReturnHeader.Business.IsEIN = true;
            form941SchRRecords.ReturnHeader.Business.IsForeign = false;

            if (form941SchRRecords?.ReturnHeader?.ThirdPartyDesignee != null && (!string.IsNullOrEmpty(form941SchRRecords.ReturnHeader.ThirdPartyDesignee.Name) || !string.IsNullOrEmpty(form941SchRRecords.ReturnHeader.ThirdPartyDesignee.Phone) || !string.IsNullOrEmpty(form941SchRRecords.ReturnHeader.ThirdPartyDesignee.PIN)))
            {
                form941SchRRecords.ReturnHeader.IsThirdPartyDesignee = true;
            }

            if (form941SchRRecords?.ReturnHeader?.BusinessStatusDetails != null)
            {
                if (form941SchRRecords.ReturnHeader.BusinessStatusDetails.IsBusinessClosed == false)
                {
                    form941SchRRecords.ReturnHeader.BusinessStatusDetails.BusinessClosedDetails = new BusinessClosedDetails();
                }
                if (form941SchRRecords.ReturnHeader.BusinessStatusDetails.IsBusinessTransferred == false)
                {
                    form941SchRRecords.ReturnHeader.BusinessStatusDetails.BusinessTransferredDetails = new BusinessTransferredDetails();
                }
            }

            var form941SchRResponse = new Form941SchRCreateReturnResponse();
            var form941SchRReturnList = new Form941SchRCreateReturnRequest { Form941SchRRecords = new List<Form941SchRRecords>() };
            form941SchRReturnList.Form941SchRRecords.Add(form941SchRRecords);

            // Generate JSON for Form 941
            var requestJson = JsonConvert.SerializeObject(form941SchRReturnList, Formatting.Indented);

            using (var client = new PublicAPIClient())
            {
                //API URL to Create Form 941 Return
                string requestUri = "Form941SCHR/Create";

                //POST
                APIGenerateAuthHeader.GenerateAuthHeader(client, requestUri, HttpMethod.Post.ToString());

                //Get Response
                var response = client.PostAsJsonAsync(requestUri, form941SchRReturnList).Result;
                if (response != null && response.IsSuccessStatusCode)
                {
                    //Read Response
                    var createResponse = response.Content.ReadAsAsync<Form941SchRCreateReturnResponse>().Result;
                    if (createResponse != null)
                    {
                        responseJson = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                        //Deserializing JSON (Success Response) to Form941CreateReturnResponse object
                        form941SchRResponse = new JavaScriptSerializer().Deserialize<Form941SchRCreateReturnResponse>(responseJson);
                        if (form941SchRResponse.SubmissionId != null && form941SchRResponse.SubmissionId != Guid.Empty)
                        {
                            //Adding Form941CreateReturnResponse Response to Session
                            //APISession.AddAPIResponse(form941SchRResponse); To Do
                        }
                    }
                }
                else
                {
                    var createResponse = response.Content.ReadAsAsync<Object>().Result;
                    responseJson = JsonConvert.SerializeObject(createResponse, Formatting.Indented);

                    //Deserializing JSON (Error Response) to Form941CreateReturnResponse object
                    form941SchRResponse = new JavaScriptSerializer().Deserialize<Form941SchRCreateReturnResponse>(responseJson);
                }
            }
            return PartialView(form941SchRResponse);
        }
        #endregion
    }
}