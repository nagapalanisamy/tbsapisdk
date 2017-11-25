using System;

namespace APIClientTool.ViewModels
{
    public class BusinessDetails
    {
        public Guid BusinessId { get; set; }
        public string BusinessNm { get; set; }
        public string TradeNm { get; set; }
        public string EIN { get; set; }
        public string ContactNm { get; set; }
        public string BusinessType { get; set; }
        public SigningAuthority SigningAuthority { get; set; }
        public string KindOfEmployer { get; set; }
        public string EmploymentCd { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string USState { get; set; }
        public string Country { get; set; }
        public string USZip { get; set; }
        public bool IsForeign { get; set; }
        public string PostalCd { get; set; }
        public string ProvinceState { get; set; }

        public string Email { get; set; }
        public string Fax { get; set; }
        public string Phone { get; set; }
        public string PhoneExtn { get; set; }

    }
}
