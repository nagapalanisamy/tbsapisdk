using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIClientTool.ViewModels
{
    public class EFileStatus : EntityBase
    {
        public Guid SubmissionId { get; set; }
        public Guid RecordId { get; set; }
        public bool IsReturnTransmitted { get; set; }
    }
}