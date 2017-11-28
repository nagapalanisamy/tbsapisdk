using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIClientTool.ViewModels
{
    public class W2CreateReturnRequest
    {
        /// <summary>
        /// This action will look for the Form W-2 Form details to create the return
        /// </summary>
        public List<FormW2> W2Forms { get; set; }
    }

}