using APIClientTool.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIClientTool.Utilities
{
    public class APISession
    {
        #region Session Property for W2 Return
        public static List<W2CreateReturnResponse> W2ReturnResponses
        {
            get
            {
                if(HttpContext.Current.Session["W2CreateReturnResponse"] != null)
                {
                    return (List<W2CreateReturnResponse>)HttpContext.Current.Session["W2CreateReturnResponse"];
                }
                else
                {
                    return new List<W2CreateReturnResponse>();
                }
            }
            set
            {
                if(value != null)
                {
                    HttpContext.Current.Session["W2CreateReturnResponse"] = value;
                }
            }
        }
        #endregion

        #region Add API Response
        public static void AddAPIResponse(W2CreateReturnResponse returnResponse)
        {           
            if (returnResponse != null && returnResponse.SubmissionId != Guid.Empty)
            {
                List<W2CreateReturnResponse> returnResponses = W2ReturnResponses;
                returnResponses.Add(returnResponse);
                W2ReturnResponses = returnResponses;
            }
        }
        #endregion

        #region Get API Response 
        public static List<EFileStatus> GetAPIResponse()
        {
            List<EFileStatus> formW2list = new List<EFileStatus>();
            List<W2CreateReturnResponse> returnResponses = W2ReturnResponses;
            if (returnResponses != null && returnResponses.Count > 0)
            {
                var apiResponse = returnResponses.Where(a => a.StatusCode == (int)StatusCode.Success && a.SubmissionId != Guid.Empty).ToList();
                foreach (var submission in apiResponse)
                {
                    var formW2 = new EFileStatus();
                    formW2.SubmissionId = submission.SubmissionId;
                    formW2.IsReturnTransmitted = submission.IsReturnTransmitted;
                    formW2list.Add(formW2);
                }
            }
            return formW2list;
        }
        #endregion

        #region Get RecordIDs by SubmissionId
        public static TransmitFormW2 GetRecordIdsBySubmissionId(Guid submissionId)
        {
            TransmitFormW2 transmitFormW2 = new TransmitFormW2();
            List<W2CreateReturnResponse> returnResponses = W2ReturnResponses;
            if (submissionId != Guid.Empty)
            {
                transmitFormW2.SubmissionId = submissionId;
                if (returnResponses != null && returnResponses.Count > 0)
                {
                    var returnResponse = returnResponses.Where(r => r.SubmissionId == submissionId).SingleOrDefault();
                    if(returnResponse != null && returnResponse.FormW2Records != null 
                        && returnResponse.FormW2Records.SuccessRecords != null && returnResponse.FormW2Records.SuccessRecords.Count > 0)
                    {
                        var recordIds = returnResponse.FormW2Records.SuccessRecords.Select(r => r.RecordId).ToList();
                        if (recordIds != null && recordIds.Any())
                        {
                            transmitFormW2.RecordIds = new List<Guid>();
                            foreach (var recordId in recordIds)
                            {
                                transmitFormW2.RecordIds.Add(recordId ?? Guid.Empty);
                            }
                        }
                    }
                }
            }
            return transmitFormW2;
        }
        #endregion

        #region Update Filing Status
        public static bool UpdateFilingStatus(Guid submissionId)
        {
            bool isUpdated = false;
            if (submissionId != Guid.Empty)
            {
                if (W2ReturnResponses != null && W2ReturnResponses.Count > 0)
                {
                    var apiResponse = W2ReturnResponses.Where(a => a.SubmissionId == submissionId).SingleOrDefault();
                    apiResponse.IsReturnTransmitted = true;
                    isUpdated = true;
                }
            }

            return isUpdated;
        }
        #endregion
    }
}