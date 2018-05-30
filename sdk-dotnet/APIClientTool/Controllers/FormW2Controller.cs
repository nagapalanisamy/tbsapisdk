using APIClientTool.Utilities;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Net.Http;
using APIClientTool.ViewModels;
using System.Collections.Generic;

namespace APIClientTool.Controllers
{
    public class FormW2Controller : Controller
    {

        #region Create Form W-2 Return
        /// <summary>
        /// Function returns the form view to create W-2 return
        /// </summary>
        /// <param name="id">Parameter id is passed to prefill default values in the W-2 Form</param>
        /// <returns>FormW2 View</returns>
        public ActionResult FormW2Return(bool? id)
        {
            FormW2 formw2 = new FormW2();
            bool _prePopulate = id ?? false;
            if (_prePopulate)
            {
                PrePopulate(formw2);
            }
            return View(formw2);
        }

        private static void PrePopulate(FormW2 formw2)
        {
            //Mapping FormW2
            formw2.TaxYear = 2017;
            formw2.Sequence = "Record1";

            //Mapping BusinessDetails
            formw2.Business = new Business();
            formw2.Business.BusinessNm = "Test Business";
            formw2.Business.TradeNm = "";
            formw2.Business.Email = "employer@company.com";
            formw2.Business.IsEIN = true;
            formw2.Business.EINorSSN = "123456789";
            formw2.Business.ContactNm = "John Doe";
            formw2.Business.Phone = "1234567890";
            formw2.Business.BusinessType = "ESTE";

            formw2.Business.SigningAuthority = new SigningAuthority();
            formw2.Business.SigningAuthority.Name = "John Doe";
            formw2.Business.SigningAuthority.Phone = "1234567890";
            formw2.Business.SigningAuthority.BusinessMemberType = "Owner";

            formw2.Business.KindOfEmployer = "FEDERALGOVT";
            formw2.Business.KindOfPayer = "REGULAR941";

            formw2.Business.IsForeign = false;
            formw2.Business.USAddress = new USAddress();
            formw2.Business.USAddress.Address1 = "Address Line 1";
            formw2.Business.USAddress.City = "Rockhill";
            formw2.Business.USAddress.State = "SC";
            formw2.Business.USAddress.ZipCd = "29730";


            //Mapping Employee
            formw2.Employee = new Employee();
            formw2.Employee.SSN = "123456789";
            formw2.Employee.FirstNm = "Steve";
            formw2.Employee.LastNm = "Smith";
            formw2.Employee.Country = "US";
            formw2.Employee.Address1 = "Address Line 1";
            formw2.Employee.City = "Rockhill";
            formw2.Employee.State = "SC";
            formw2.Employee.Zip = "29730";
            formw2.Employee.Phone = "9876543210";
            formw2.Employee.Email = "employee@company.com";

            //Mapping FormW2Details
            formw2.FormDetails = new FormW2Details();
            formw2.FormDetails.Box1 = 10000.00M;

            //Optional BusinessDetails
            formw2.Business.PhoneExtn = "";
            formw2.Business.Fax = "";

            //Optional Employee
            formw2.Employee.MiddleNm = "";
            formw2.Employee.Suffix = "";
            formw2.Employee.Fax = "";
            formw2.Employee.Address2 = "";
        }
        #endregion

        #region Form W-2 Create Return Response Status
        /// <summary>
        /// Function inputs Form W-2 details, POST all those details to the API and returns the response.
        /// Successful response contains SubmissionId, StatusCode and RecordSuccessStatus details (Sequence, RecordId, RecordStatus etc)
        /// Error response contains StatusCode and RecordErrorStatus details (RecordId, Sequence and list of Error information such as Code, Name, Message and Type)
        /// </summary>
        /// <param name="formw2">Form W-2 details passed through formw2 parameter</param>
        /// <returns>W2CreateReturnResponse</returns>
        public ActionResult APIResponseStatus(FormW2 formw2)
        {
            //Hardcoded values for Sequence and TaxYear
            var responseJson = string.Empty;
            formw2.TaxYear = 2017;
            formw2.Sequence = "Record1";

            W2CreateReturnResponse w2response = new W2CreateReturnResponse();
            W2CreateReturnRequest w2ReturnList = new W2CreateReturnRequest();
            w2ReturnList.W2Forms = new List<FormW2>();
            w2ReturnList.W2Forms.Add(formw2);

            // Generate JSON for Form W-2
            var requestJson = JsonConvert.SerializeObject(w2ReturnList, Formatting.Indented);

            using (var client = new PublicAPIClient())
            {
                //API URL to Create Form W-2 Return
                string requestUri = "FormW2/Create";

                //POST
                APIGenerateAuthHeader.GenerateAuthHeader(client, requestUri, "POST");

                //Get Response
                var _response = client.PostAsJsonAsync(requestUri, w2ReturnList).Result;
                if (_response != null && _response.IsSuccessStatusCode)
                {
                    //Read Response
                    var createResponse = _response.Content.ReadAsAsync<W2CreateReturnResponse>().Result;
                    if (createResponse != null)
                    {
                        responseJson = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                        //Deserializing JSON (Success Response) to W2CreateReturnResponse object
                        w2response = new JavaScriptSerializer().Deserialize<W2CreateReturnResponse>(responseJson);
                        if (w2response.SubmissionId != null && w2response.SubmissionId != Guid.Empty)
                        {
                            //Adding W2CreateReturnResponse Response to Session
                            APISession.AddAPIResponse(w2response);
                        }
                    }
                }
                else
                {
                    var createResponse = _response.Content.ReadAsAsync<Object>().Result;
                    responseJson = JsonConvert.SerializeObject(createResponse, Formatting.Indented);

                    //Deserializing JSON (Error Response) to W2CreateReturnResponse object
                    w2response = new JavaScriptSerializer().Deserialize<W2CreateReturnResponse>(responseJson);
                }
            }
            return PartialView(w2response);
        }
        #endregion

        #region Get EFile Status
        public ActionResult _GetEFileStatus()
        {
            List<EFileStatus> _formw2List = new List<EFileStatus>();
            _formw2List = APISession.GetAPIResponse();
            return PartialView(_formw2List);
        }
        #endregion

        /// <summary>
        /// Function loads list of all SubmissionIds created in FormW2Return page
        /// </summary>
        /// <returns>List of all SubmissionIds</returns>
        #region  EFile Status
        public ActionResult EFileStatus()
        {
            List<EFileStatus> _formw2List = new List<EFileStatus>();
            _formw2List = APISession.GetAPIResponse();
            return View(_formw2List);
        }
        #endregion

        #region Transmit Return
        /// <summary>
        /// Function transmit the Form W-2 Return to Efile
        /// </summary>
        /// <param name="submissionId">SubmissionId passed to transmit the W-2 return</param>
        /// <returns>TransmitFormW2Response</returns>
        public ActionResult _TransmitReturn(Guid submissionId)
        {
            TransmitForm transmitFormW2 = new TransmitForm();
            TransmitFormW2Response transmitFormW2Response = new TransmitFormW2Response();
            var transmitFormW2ResponseJSON = string.Empty;
            if (submissionId != null && submissionId != Guid.Empty)
            {
                // Getting the RecordIds for SubmissionId
                transmitFormW2 = APISession.GetRecordIdsBySubmissionId(submissionId);

                // Generate JSON for TransmitFormW2
                var requestJson = JsonConvert.SerializeObject(transmitFormW2, Formatting.Indented);

                if (transmitFormW2 != null)
                {
                    using (var client = new PublicAPIClient())
                    {
                        //API URL to Transmit Form W-2 Return
                        string requestUri = "FormW2/Transmit";

                        //POST
                        APIGenerateAuthHeader.GenerateAuthHeader(client, requestUri, "POST");

                        //Get Response
                        var _response = client.PostAsJsonAsync(requestUri, transmitFormW2).Result;
                        if (_response != null && _response.IsSuccessStatusCode)
                        {
                            //Read Response
                            var createResponse = _response.Content.ReadAsAsync<TransmitFormW2Response>().Result;
                            if (createResponse != null)
                            {
                                transmitFormW2ResponseJSON = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                                transmitFormW2Response = new JavaScriptSerializer().Deserialize<TransmitFormW2Response>(transmitFormW2ResponseJSON);
                                if (transmitFormW2Response.SubmissionId != null && transmitFormW2Response.SubmissionId != Guid.Empty && transmitFormW2Response.StatusCode == (int)StatusCode.Success)
                                {
                                    //Updating Filing Status (Transmitted) for a specific SubmissionId in Session 
                                    APISession.UpdateFilingStatus(transmitFormW2Response.SubmissionId);
                                }
                            }
                        }
                        else
                        {
                            var createResponse = _response.Content.ReadAsAsync<Object>().Result;
                            transmitFormW2ResponseJSON = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                            transmitFormW2Response = new JavaScriptSerializer().Deserialize<TransmitFormW2Response>(transmitFormW2ResponseJSON);
                        }
                    }
                }
            }
            return PartialView(transmitFormW2Response);
        }
        #endregion

        #region Get Efile Status
        /// <summary>
        /// Function returns the Efile status of Form W-2
        /// </summary>
        /// <param name="submissionId">SubmissionId is passed to get the efile status</param>
        /// <returns>EfileStatusResponse</returns>
        public ActionResult _GetEfileStatusResponse(Guid submissionId)
        {
            EfileStatusResponse efileStatusResponse = new EfileStatusResponse();
            if (submissionId != null && submissionId != Guid.Empty)
            {
                var efileRequest = new EfileStatusGetRequest { SubmissionId = submissionId };
                var recordIds = APISession.GetRecordIdsBySubmissionId(submissionId);
                if (recordIds != null && recordIds.RecordIds != null && recordIds.RecordIds.Count > 0)
                {
                    efileRequest.RecordIds = recordIds.RecordIds;
                }
                var transmitFormW2ResponseJSON = string.Empty;

                // Request JSON
                var requestJson = JsonConvert.SerializeObject(efileRequest, Formatting.Indented);

                if (submissionId != null && submissionId != Guid.Empty)
                {
                    using (var client = new PublicAPIClient())
                    {
                        //POST
                        string requestUri = "FormW2/Status";

                        //Get Response
                        APIGenerateAuthHeader.GenerateAuthHeader(client, requestUri, "POST");

                        //Read Response
                        var _response = client.PostAsJsonAsync(requestUri, efileRequest).Result;
                        if (_response != null && _response.IsSuccessStatusCode)
                        {
                            var createResponse = _response.Content.ReadAsAsync<EfileStatusResponse>().Result;
                            if (createResponse != null)
                            {
                                transmitFormW2ResponseJSON = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                                efileStatusResponse = new JavaScriptSerializer().Deserialize<EfileStatusResponse>(transmitFormW2ResponseJSON);
                            }
                        }
                        else
                        {
                            var createResponse = _response.Content.ReadAsAsync<Object>().Result;
                            transmitFormW2ResponseJSON = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                            efileStatusResponse = new JavaScriptSerializer().Deserialize<EfileStatusResponse>(transmitFormW2ResponseJSON);
                        }
                    }
                }
            }
            return PartialView(efileStatusResponse);
        }
        #endregion
    }
}