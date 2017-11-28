using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIClientTool.ViewModels
{
    public class TransmitFormW2
    {
        public Guid SubmissionId { get; set; }

        public List<Guid> RecordIds { get; set; }
    }
}