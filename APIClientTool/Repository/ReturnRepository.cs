using APIClientTool.Models;
using APIClientTool.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static APIClientTool.ViewModels.EntityBase;

namespace APIClientTool.Repository
{
    public class ReturnRepository
    {

        #region Constructor
        public ReturnRepository()
        {
        }
        #endregion

        #region Save API Response
        public void SaveAPIResponse(W2CreateReturnResponse returnResponse)
        {
            if (returnResponse != null)
            {
                using (TaxBanditsAPIClientEntities dbContext = new TaxBanditsAPIClientEntities())
                {
                    var apiResponse = new APIResponse();
                    apiResponse.Code = returnResponse.StatusCode;
                    apiResponse.Message = returnResponse.StatusMessage;
                    apiResponse.Name = returnResponse.StatusName;
                    apiResponse.Submission_Id = returnResponse.SubmissionId;
                    dbContext.APIResponses.Add(apiResponse);
                    dbContext.SaveChanges();

                    if (returnResponse.FormW2Record != null)
                    {
                        if (returnResponse.FormW2Record.SuccessRecords != null && returnResponse.FormW2Record.SuccessRecords.Count > 1)
                        {
                            foreach (var successRecord in returnResponse.FormW2Record.SuccessRecords)
                            {
                                var successStatus = new SuccessStatu();
                                successStatus.Record_Id = successRecord.RecordId;
                                successStatus.Sequence = successRecord.Sequence;
                                successStatus.Response_Id = apiResponse.Reponse_Id;
                                dbContext.SuccessStatus.Add(successStatus);
                                dbContext.SaveChanges();
                            }
                        }

                        if (returnResponse.FormW2Record.ErrorRecords != null && returnResponse.FormW2Record.ErrorRecords.Count > 1)
                        {
                            foreach (var errorRecord in returnResponse.FormW2Record.ErrorRecords)
                            {
                                var errorStatus = new ErrorStatu();
                                errorStatus.Sequence = errorRecord.Sequence;
                                errorStatus.Response_Id = apiResponse.Reponse_Id;
                                dbContext.ErrorStatus.Add(errorStatus);
                                dbContext.SaveChanges();

                                if (errorRecord.Errors != null && errorRecord.Errors.Count > 1)
                                {
                                    foreach (var error in errorRecord.Errors)
                                    {
                                        var recordError = new RecordError();
                                        recordError.Code = error.Code;
                                        recordError.Error_Status_Id = errorStatus.Error_Status_Id;
                                        recordError.Message = error.Message;
                                        recordError.Name = error.Name;
                                        dbContext.RecordErrors.Add(recordError);
                                        dbContext.SaveChanges();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region Save API Error Response
        public void SaveAPIErrorResponse(ErrorResponse errorResponse)
        {
            if (errorResponse != null && errorResponse.Errors != null && errorResponse.Errors.Any())
            {
                using (TaxBanditsAPIClientEntities dbContext = new TaxBanditsAPIClientEntities())
                {
                    foreach (var error in errorResponse.Errors)
                    {
                        var recordError = new RecordError();
                        recordError.Code = error.Code;
                        recordError.Message = error.Message;
                        recordError.Name = error.Name;
                        //recordError.Type = error.Type;
                        dbContext.RecordErrors.Add(recordError);
                        dbContext.SaveChanges();
                    }
                }
            }
        }
        #endregion

        #region Get API Response 
        public List<TransmitFormW2> GetAPIResponse()
        {
            List<TransmitFormW2> transmitFormW2list = new List<TransmitFormW2>();
            using (TaxBanditsAPIClientEntities dbContext = new TaxBanditsAPIClientEntities())
            {
                var apiResponse = dbContext.APIResponses.Where(a => a.Code == (int)StatusCode.Success && a.Submission_Id != Guid.Empty).ToList();
                foreach (var submission in apiResponse)
                {
                    var transmitFormW2 = new TransmitFormW2();
                    transmitFormW2.SubmissionId = submission.Submission_Id ?? Guid.Empty;
                    transmitFormW2list.Add(transmitFormW2);
                }
            }
            return transmitFormW2list;
        }
        #endregion
    }
}