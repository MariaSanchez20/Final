namespace TenantAPI.Models
{
    public class Apartment
    {
        public int Id { get; set; }
        public string OwnerName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
