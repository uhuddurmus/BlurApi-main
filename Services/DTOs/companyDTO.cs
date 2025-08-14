namespace BlurApi.Services.DTOs
{
    public class CompanyCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal Balance { get; set; } = 0;
        public string Address { get; set; } = string.Empty;
        public long TaxNumber { get; set; }
        public TaxAddressDto TaxAddress { get; set; } = new TaxAddressDto();
    }

    public class TaxAddressDto
    {
        public string Province { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
    }
}
