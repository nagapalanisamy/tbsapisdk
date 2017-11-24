
using System.Runtime.Serialization;

namespace APIClient.Core.Models
{
    [DataContract]
    public class SigningAuthority
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string DayTimePhone { get; set; }
        [DataMember]
        public string BusinessMembers { get; set; }
        [DataMember]
        public string SignatureType { get; set; }
        [DataMember]
        public string PinNumber { get; set; }
    }
}
