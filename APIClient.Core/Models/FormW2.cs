using System.Runtime.Serialization;

namespace APIClient.Core.Models
{
    [DataContract]
    public class FormW2
    {
        [DataMember]
        public BusinessDetails Business { get; set; }
        [DataMember]
        public Employee Employee { get; set; }
        [DataMember]
        public W2FormDetails FormDetails { get; set; }
    }
}
