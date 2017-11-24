using System.Runtime.Serialization;

namespace APIClient.Core.Models
{
    [DataContract]
    public class BusinessDetails : AddressDetails
    {
        [DataMember]
        public string BusinessName { get; set; }
        [DataMember]
        public string TradeName { get; set; }
        [DataMember]
        public string EIN { get; set; }
        [DataMember]
        public string ContactName { get; set; }
        [DataMember]
        public string BusinessType { get; set; }
        [DataMember]
        public SigningAuthority SigningAuthority { get; set; }
        [DataMember]
        public string KindOfEmployer { get; set; }
        [DataMember]
        public string EmploymentCode { get; set; }

    }
}
