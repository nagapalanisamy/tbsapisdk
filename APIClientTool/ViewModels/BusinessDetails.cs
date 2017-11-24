namespace APIClientTool.ViewModels
{
    public class BusinessDetails : AddressDetails
    {
        public string BusinessName { get; set; }
        public string TradeName { get; set; }
        public string EIN { get; set; }
        public string ContactName { get; set; }
        public string BusinessType { get; set; }
        public SigningAuthority SigningAuthority { get; set; }
        public string KindOfEmployer { get; set; }
        public string EmploymentCode { get; set; }

    }
}
