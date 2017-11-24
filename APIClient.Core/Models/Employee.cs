
using System.Runtime.Serialization;

namespace APIClient.Core.Models
{
    [DataContract]
    public class Employee : AddressDetails
    {
        [DataMember]
        public string SSN { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Suffix { get; set; }

    }
}
