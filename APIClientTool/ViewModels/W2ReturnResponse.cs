using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIClientTool.ViewModels
{
    public class W2CreateReturnResponse
    {
        public Guid SubmissionId { get; set; }
        public FormW2Records FormW2Record { get; set; }
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public string StatusName { get; set; }
        public List<Error> Errors { get; set; }
    }

    public class FormW2Records
    {
        public List<FormW2RecordSuccessStatus> SuccessRecords { get; set; }
        public List<FormW2RecordErrorStatus> ErrorRecords { get; set; }
    }
    public class FormW2RecordErrorStatus
    {
        public string Sequence { get; set; }
        public List<Error> Errors { get; set; }
    }

    public class FormW2RecordSuccessStatus
    {
        public string Sequence { get; set; }
        public Guid? RecordId { get; set; }
        public string RecordStatus { get; set; }
        public string CreatedTs { get; set; }
        public string UpdatedTs { get; set; }
    }
    public class Error
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Message { get; set; }

        public string Type { get; set; }
    }

    public class Messages
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Message { get; set; }

        public string Type { get; set; }
    }
}