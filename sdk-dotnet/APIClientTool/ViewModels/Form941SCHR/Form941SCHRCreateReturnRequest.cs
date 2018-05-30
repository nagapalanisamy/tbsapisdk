using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using APIClientTool.ViewModels.Form941CoreModel;

namespace APIClientTool.ViewModels.Form941SCHR
{
    public class Form941SCHRCreateReturnRequest
    {
        /// <summary>
        /// Form 941SCHR Records
        /// </summary>
        public List<Form941SCHRRecords> Form941SCHRRecords { get; set; }
        /// <summary>
        /// Submission Id
        /// </summary>
        public Guid SubmissionId { get; set; }
    }
}