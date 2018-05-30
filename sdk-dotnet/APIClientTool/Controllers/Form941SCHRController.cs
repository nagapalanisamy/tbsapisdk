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
using APIClientTool.ViewModels;

namespace APIClientTool.Controllers
{
    public class Form941SCHRController : Controller
    {
        #region Form 941SCHR View Get Method
        // GET: Form941SCHR
        [Route("form941schr")]
        public ActionResult Index(bool? id)
        {
            Form941SchRRecords form941SCHRRecords = new Form941SchRRecords();
            bool _prePopulate = id ?? false;
            if (_prePopulate)
            {
                PrePopulate(form941SCHRRecords);
            }
            return View(form941SCHRRecords);
        }
        #endregion

        #region Pre Populate
        /// <summary>
        /// Pre Populate date
        /// </summary>
        /// <param name="form941"></param>
        private static void PrePopulate(Form941SchRRecords form941SchRRecords)
        {
            //Mapping Form 941 
            form941SchRRecords.Sequence = "Record1";
            form941SchRRecords.RecordId = null;
            //Mapping Return Header
            form941SchRRecords.ReturnHeader = new Form941SchRReturnHeader
            {
                ReturnType = null,
                Qtr = "Q1",
                TaxYr = "2018",
                Business = new Business
                {
                    BusinessId = null,
                    BusinessNm = "Test Business",
                    EINorSSN = "003333330",
                    IsEIN = true,
                    BusinessType = "ESTE",
                    ContactNm = "John Doe",
                    Email = "employer@company.com",
                    Fax = "1234567890",
                    TradeNm = "Kodak",
                    IsBusinessTerminated = false,
                    Phone = "1234566890",
                    PhoneExtn = "12345",
                    IsForeign = false,
                    USAddress = new USAddress
                    {
                        Address1 = "Address Line 1",
                        City = "Rockhill",
                        State = "SC",
                        ZipCd = "29730"
                    },
                    SigningAuthority = new APIClientTool.ViewModels.SigningAuthority
                    {
                        BusinessMemberType = "ADMINISTRATOR",
                        Phone = "1234564390",
                        Name = "John"
                    },
                    KindOfEmployer = null,
                    KindOfPayer = null,
                    ForeignAddress = null
                },
                BusinessStatusDetails = new BusinessStatusDetails
                {
                    IsBusinessClosed = false,
                    IsBusinessTransferred = false,
                    IsSeasonalEmployer = false,
                    BusinessClosedDetails = null,
                    BusinessTransferredDetails = null
                },
                IsThirdPartyDesignee = true,
                ThirdPartyDesignee = new ThirdPartyDesignee
                {
                    Name = "John Doe",
                    Phone = "1234567890",
                    PIN = "12345"
                },
                SignatureDetails = new SignatureDetails
                {
                    SignatureType = "ONLINE_SIGN_PIN",
                    OnlineSignaturePIN = new OnlineSignaturePIN { PIN = "1234567890" }
                }
            };

            //Mapping Return Data
            form941SchRRecords.ReturnData = new Form941SchRReturnData
            {
                DepositScheduleType = new DepositScheduleType
                {
                    DepositorType = DepositorType.MONTHLY.ToString(),
                    MonthlyDepositor = new MonthlyDepositor
                    {
                        TaxLiabilityMonth1 = 4,
                        TaxLiabilityMonth2 = 345,
                        TaxLiabilityMonth3 = 43
                    }
                },
                AggregateForm941Data = new Form941Details
                {
                    EmployeeCnt = 3,
                    WagesAmt = 5750000M,
                    FedIncomeTaxWHAmt = 13499.76M,
                    WagesNotSubjToSSMedcrTaxInd = null,
                    Line5aInitialAmt = 57000M,
                    Line5bInitialAmt = 0M,
                    Line5cInitialAmt = 57500M,
                    Line5dInitialAmt = 0M,
                    Line5a = 7130M,
                    Line5b = 0M,
                    Line5c = 1667.50M,
                    Line5d = 0M,
                    Line5e = 8797.50M,
                    TaxOnUnreportedTips3121qAmt = 0M,
                    CurrentQtrFractionsCentsAmt = 0M,
                    CurrentQuarterSickPaymentAmt = 0M,
                    CurrQtrTipGrpTermLifeInsAdjAmt = 0M,
                    Line12 = 22297.26M,
                    Line11 = 0M,
                    Line14 = 0M,
                    Line15 = 0M
                }
            };
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

            // Generate JSON for Form 941SCHR
            var requestJson = JsonConvert.SerializeObject(form941SchRReturnList, Formatting.Indented);

            using (var client = new PublicAPIClient())
            {
                //API URL to Create Form 941SCHR Return
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

        #region Transmit Return
        /// <summary>
        /// Function transmit the Form 941 Return to Efile
        /// </summary>
        /// <param name="submissionId">SubmissionId passed to transmit the 941 return</param>
        /// <returns>TransmitFormW2Response</returns>
        public ActionResult _TransmitReturn(Guid submissionId)
        {
            var transmitForm941SchR = new TransmitForm();
            var transmitForm941SchRResponse = new TransmitForm941SchRResponse();
            var transmitForm941SchRResponseJSON = string.Empty;
            if (submissionId != null && submissionId != Guid.Empty)
            {
                // Getting the RecordIds for SubmissionId
                transmitForm941SchR = APISession.GetRecordIdsBySubmissionId(submissionId);

                // Generate JSON for TransmitForm 941
                var requestJson = JsonConvert.SerializeObject(transmitForm941SchR, Formatting.Indented);

                if (transmitForm941SchR != null)
                {
                    using (var client = new PublicAPIClient())
                    {
                        //API URL to Transmit Form 941 Return
                        string requestUri = "Form941SCHR/Transmit";

                        //POST
                        APIGenerateAuthHeader.GenerateAuthHeader(client, requestUri, "POST");

                        //Get Response
                        var _response = client.PostAsJsonAsync(requestUri, transmitForm941SchR).Result;
                        if (_response != null && _response.IsSuccessStatusCode)
                        {
                            //Read Response
                            var createResponse = _response.Content.ReadAsAsync<TransmitForm941SchRResponse>().Result;
                            if (createResponse != null)
                            {
                                transmitForm941SchRResponseJSON = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                                transmitForm941SchRResponse = new JavaScriptSerializer().Deserialize<TransmitForm941SchRResponse>(transmitForm941SchRResponseJSON);
                                if (transmitForm941SchRResponse.SubmissionId != null && transmitForm941SchRResponse.SubmissionId != Guid.Empty && transmitForm941SchRResponse.StatusCode == (int)StatusCode.Success)
                                {
                                    //Updating Filing Status (Transmitted) for a specific SubmissionId in Session 
                                    APISession.UpdateFilingStatus(transmitForm941SchRResponse.SubmissionId);
                                }
                            }
                        }
                        else
                        {
                            var createResponse = _response.Content.ReadAsAsync<Object>().Result;
                            transmitForm941SchRResponseJSON = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                            transmitForm941SchRResponse = new JavaScriptSerializer().Deserialize<TransmitForm941SchRResponse>(transmitForm941SchRResponseJSON);
                        }
                    }
                }
            }
            return PartialView(transmitForm941SchRResponseJSON);
        }
        #endregion

        #region Delete Return
        /// <summary>
        /// Function transmit the Form 941 Return to Efile
        /// </summary>
        /// <param name="submissionId">SubmissionId passed to transmit the 941 return</param>
        /// <returns>TransmitFormW2Response</returns>
        public ActionResult Delete(Guid submissionId)
        {
            var deleteReturnRequest = new DeleteReturnRequest();
            var deleteReturnResponse = new DeleteReturnResponse();
            var deleteReturnResponseJSON = string.Empty;
            if (submissionId != null && submissionId != Guid.Empty)
            {
                deleteReturnRequest.SubmissionId = submissionId;
                // Getting the RecordIds for SubmissionId
                deleteReturnRequest.RecordIds = APISession.GetComaseperatedRecordIdsBySubmissionId(submissionId);

                if (!string.IsNullOrEmpty(deleteReturnRequest.RecordIds))
                {
                    using (var client = new PublicAPIClient())
                    {
                        //API URL to Transmit Form 941 Return
                        string requestUri = "Form941SCHR/Delete";

                        //POST
                        APIGenerateAuthHeader.GenerateAuthHeader(client, requestUri, "POST");

                        //Get Response
                        var _response = client.PostAsJsonAsync(requestUri, deleteReturnRequest).Result;
                        if (_response != null && _response.IsSuccessStatusCode)
                        {
                            //Read Response
                            var createResponse = _response.Content.ReadAsAsync<DeleteReturnResponse>().Result;
                            if (createResponse != null)
                            {
                                deleteReturnResponseJSON = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                                deleteReturnResponse = new JavaScriptSerializer().Deserialize<DeleteReturnResponse>(deleteReturnResponseJSON);
                                if (deleteReturnResponse != null && deleteReturnResponse.StatusCode == (int)StatusCode.Success)
                                {
                                    //Todo Remove Submission and RecordId from session
                                }
                            }
                        }
                        else
                        {
                            var createResponse = _response.Content.ReadAsAsync<Object>().Result;
                            deleteReturnResponseJSON = JsonConvert.SerializeObject(createResponse, Formatting.Indented);
                            deleteReturnResponse = new JavaScriptSerializer().Deserialize<DeleteReturnResponse>(deleteReturnResponseJSON);
                        }
                    }
                }
            }
            return PartialView(deleteReturnResponseJSON);
        }
        #endregion

    }
}