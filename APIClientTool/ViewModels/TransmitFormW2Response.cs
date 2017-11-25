using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIClientTool.ViewModels
{
    public class TransmitFormW2Response
    {
        public SuccessStatus SuccessStatus { get; set; }
        public ErrorStatus ErrorStatus { get; set; }
        public List<Error> Errors { get; set; }
        public int StatusCode { get; set; }
        public string StatusName { get; set; }
        public string StatusMessage { get; set; }
    }

    public class SuccessStatus
    {
        public List<Guid> RecordId { get; set; }
    }

    public class ErrorStatus
    {
        public Guid RecordId { get; set; }
        public List<Error> Errors { get; set; }
    }

}