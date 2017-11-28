using System;

namespace APIClientTool.ViewModels
{
    public class FormW2
    {
        public string Sequence { get; set; }
        public BusinessDetails Business { get; set; }
        public Employee Employee { get; set; }
        public FormW2Details FormDetails { get; set; }
        public int TaxYear { get; set; }
        public Guid RecordId { get; set; }
    }
}
