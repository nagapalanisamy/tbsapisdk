namespace APIClientTool.ViewModels
{
    public class AddressDetails
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public bool IsForeignAddress { get; set; }
        public string EmailAddress { get; set; }
        public string FaxNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneExtn { get; set; }
    }
}
