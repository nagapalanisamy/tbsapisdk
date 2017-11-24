
using System.Runtime.Serialization;

namespace APIClient.Core.Models
{
    [DataContract]
    public class AddressDetails
    {
        [DataMember]
        public string AddressLine1 { get; set; }
        [DataMember]
        public string AddressLine2 { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public string ZipCode { get; set; }
        [DataMember]
        public bool IsForeignAddress { get; set; }
        [DataMember]
        public string EmailAddress { get; set; }
        [DataMember]
        public string FaxNumber { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
        [DataMember]
        public string PhoneExtn { get; set; }
    }
}
