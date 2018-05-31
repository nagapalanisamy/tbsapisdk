using System;

namespace APIClientTool.ViewModels
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