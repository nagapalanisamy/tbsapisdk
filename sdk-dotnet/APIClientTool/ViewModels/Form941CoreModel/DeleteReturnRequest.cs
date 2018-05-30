using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIClientTool.ViewModels.Form941CoreModel
{
    public class DeleteReturnRequest
    {
        /// <summary>
        /// Unique Id
        /// </summary>
        public Guid? SubmissionId { get; set; }
        /// <summary>
        /// List of record ids with comma as separator
        /// </summary>
        public string RecordIds { get; set; }
    }
}