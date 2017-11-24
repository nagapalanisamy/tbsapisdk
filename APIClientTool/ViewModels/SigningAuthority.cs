
using System.Runtime.Serialization;

namespace APIClientTool.ViewModels
{
    public class SigningAuthority
    {
        public string Name { get; set; }
        public string DayTimePhone { get; set; }
        public string BusinessMembers { get; set; }
        public string SignatureType { get; set; }
        public string PinNumber { get; set; }
    }
}
