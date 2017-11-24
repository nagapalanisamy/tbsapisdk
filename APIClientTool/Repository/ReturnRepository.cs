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
        public ReturnRepository()
        {
        }

        public void SaveAPIResponse(W2ReturnResponse returnResponse)
        {
            if (returnResponse != null)
            {
                using (TaxBanditsAPIClientEntities dbContext = new TaxBanditsAPIClientEntities())
                {
                    var apiResponse = new APIResponse();
                    apiResponse.Code = returnResponse.Code;
                    apiResponse.Message = returnResponse.Message;
                    apiResponse.Name = returnResponse.Name;
                    apiResponse.SubmissionId = returnResponse.SubmissionId;
                    dbContext.APIResponses.Add(apiResponse);
                    dbContext.SaveChanges();

                    if(returnResponse.FormW2Record != null)
                    {
                        if(returnResponse.FormW2Record.SuccessRecords != null && returnResponse.FormW2Record.SuccessRecords.Count > 1)
                        {
                            foreach(var successRecord in returnResponse.FormW2Record.SuccessRecords)
                            {
                                var successStatus = new SuccessStatu();
                                successStatus.Record_Id = successRecord.RecordId;
                                successStatus.Sequence = successRecord.Sequence;
                                successStatus.Response_Id = apiResponse.ReponseId;
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
                                errorStatus.Response_Id = apiResponse.ReponseId;
                                dbContext.ErrorStatus.Add(errorStatus);
                                dbContext.SaveChanges();

                                if(errorRecord.Errors != null && errorRecord.Errors.Count > 1)
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
    }
}