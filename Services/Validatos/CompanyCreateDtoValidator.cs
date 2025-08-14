using BlurApi.Services.DTOs;
using FluentValidation;

namespace BlurApi.Services.Validatos
{
    public class CompanyCreateDtoValidator : AbstractValidator<CompanyCreateDto>
    {
        public CompanyCreateDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Şirket adı zorunludur.");

            RuleFor(x => x.Phone)
                .NotEmpty()
                .Matches(@"^90\d{10}$").WithMessage("Telefon 90 ile başlamalı ve 12 haneli olmalıdır.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress().WithMessage("Geçerli bir e-posta giriniz.");

            RuleFor(x => x.Balance)
                .GreaterThanOrEqualTo(0).WithMessage("Bakiye negatif olamaz.")
                .Must(b => decimal.Round(b, 2) == b)
                .WithMessage("Bakiye en fazla iki ondalık basamak içerebilir.");

            RuleFor(x => x.TaxNumber)
                .InclusiveBetween(1000000000, 9999999999)
                .WithMessage("Vergi numarası 10 haneli olmalıdır.");

            RuleFor(x => x.TaxAddress).NotNull().WithMessage("Vergi adresi zorunludur.");
            RuleFor(x => x.TaxAddress.Province).NotEmpty().WithMessage("Vergi ili zorunludur.");
            RuleFor(x => x.TaxAddress.District).NotEmpty().WithMessage("Vergi ilçesi zorunludur.");
        }
    }
}
