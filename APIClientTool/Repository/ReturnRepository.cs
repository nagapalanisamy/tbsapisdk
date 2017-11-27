using APIClientTool.Models;
using APIClientTool.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
                    apiResponse.Is_Return_Transmitted = false;
                    dbContext.APIResponses.Add(apiResponse);
                    dbContext.SaveChanges();

                    if (returnResponse.FormW2Records != null)
                    {
                        if (returnResponse.FormW2Records.SuccessRecords != null && returnResponse.FormW2Records.SuccessRecords.Count > 0)
                        {
                            foreach (var successRecord in returnResponse.FormW2Records.SuccessRecords)
                            {
                                var successStatus = new SuccessStatu();
                                successStatus.Record_Id = successRecord.RecordId;
                                successStatus.Sequence = successRecord.Sequence;
                                successStatus.Response_Id = apiResponse.Reponse_Id;
                                dbContext.SuccessStatus.Add(successStatus);
                                dbContext.SaveChanges();
                            }
                        }

                        if (returnResponse.FormW2Records.ErrorRecords != null && returnResponse.FormW2Records.ErrorRecords.Count > 0)
                        {
                            foreach (var errorRecord in returnResponse.FormW2Records.ErrorRecords)
                            {
                                var errorStatus = new ErrorStatu();
                                errorStatus.Sequence = errorRecord.Sequence;
                                errorStatus.Response_Id = apiResponse.Reponse_Id;
                                dbContext.ErrorStatus.Add(errorStatus);
                                dbContext.SaveChanges();

                                if (errorRecord.Errors != null && errorRecord.Errors.Count > 0)
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

        //#region Save API Error Response
        //public void SaveAPIErrorResponse(ErrorResponse errorResponse)
        //{
        //    if (errorResponse != null && errorResponse.Errors != null && errorResponse.Errors.Any())
        //    {
        //        using (TaxBanditsAPIClientEntities dbContext = new TaxBanditsAPIClientEntities())
        //        {
        //            foreach (var error in errorResponse.Errors)
        //            {
        //                var recordError = new RecordError();
        //                recordError.Code = error.Code;
        //                recordError.Message = error.Message;
        //                recordError.Name = error.Name;
        //                //recordError.Type = error.Type;
        //                dbContext.RecordErrors.Add(recordError);
        //                dbContext.SaveChanges();
        //            }
        //        }
        //    }
        //}
        //#endregion

        #region Get API Response 
        public List<EFileStatus> GetAPIResponse()
        {
            List<EFileStatus> formW2list = new List<EFileStatus>();
            using (TaxBanditsAPIClientEntities dbContext = new TaxBanditsAPIClientEntities())
            {
                var apiResponse = dbContext.APIResponses.Where(a => a.Code == (int)StatusCode.Success && a.Submission_Id != Guid.Empty).ToList();
                foreach (var submission in apiResponse)
                {
                    var formW2 = new EFileStatus();
                    formW2.SubmissionId = submission.Submission_Id ?? Guid.Empty;
                    formW2.IsReturnTransmitted = submission.Is_Return_Transmitted ?? false;
                    formW2.IsDeleted = submission.Is_Deleted ?? false;
                    formW2.CreatedTimeStamp = submission.Created_Time_Stamp ?? DateTime.Now;
                    formW2.UpdatedTimeStamp = submission.Updated_Time_Stamp ?? DateTime.Now;
                    formW2list.Add(formW2);
                }
            }
            return formW2list;
        }
        #endregion

        #region Get API Response 
        public TransmitFormW2 GetRecordIdsBySubmissionId(Guid submissionId)
        {
            TransmitFormW2 transmitFormW2 = new TransmitFormW2();
            if (submissionId != Guid.Empty)
            {
                transmitFormW2.SubmissionId = submissionId;
                transmitFormW2.RecordIds = new List<Guid>();
                using (TaxBanditsAPIClientEntities dbContext = new TaxBanditsAPIClientEntities())
                {
                    var recordIds = (from api in dbContext.APIResponses
                                     join ss in dbContext.SuccessStatus on api.Reponse_Id equals ss.Response_Id
                                     where api.Submission_Id == submissionId && ss.Record_Id != Guid.Empty
                                     select new
                                     {
                                         Record_Id = ss.Record_Id ?? Guid.Empty
                                     }).ToList();
                    if (recordIds != null && recordIds.Any())
                    {
                        foreach (var recordId in recordIds)
                        {
                            transmitFormW2.RecordIds.Add(recordId.Record_Id);
                        }
                    }
                }
            }
            return transmitFormW2;
        }
        #endregion

        #region Get API Response 
        public bool UpdateFilingStatus(Guid submissionId)
        {
            bool isUpdated = false;
            if (submissionId != Guid.Empty)
            {
                using (TaxBanditsAPIClientEntities dbContext = new TaxBanditsAPIClientEntities())
                {
                    var apiResponse = dbContext.APIResponses.Where(a => a.Submission_Id == submissionId).SingleOrDefault();
                    apiResponse.Is_Return_Transmitted = true;
                    apiResponse.Updated_Time_Stamp = DateTime.Now;
                    dbContext.SaveChanges();
                    isUpdated = true;
                }
            }

            return isUpdated;
        }
        #endregion
    }
}