using BlurApi.Data;
using BlurApi.Models;
using BlurApi.Models.Requests;
using BlurApi.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlurApi.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class CompaniesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CompaniesController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _db.Companies.ToListAsync();
            return GenericResponse.JustData( companies );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany(Guid id)
        {
            var company = await _db.Companies.FindAsync(id);
            if (company == null) return NotFound();
            return GenericResponse.JustData(company);

        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Email kontrolü
            if (await _db.Companies.AnyAsync(c => c.Email == dto.Email))
            {
                
                ModelState.AddModelError("Email", "Bu e-posta zaten kayıtlı.");
                return ValidationProblem(ModelState);
            }

            var company = new Company
            {
                Title = dto.Title,
                Phone = dto.Phone,
                Email = dto.Email,
                Balance = dto.Balance,
                Address = dto.Address,
                TaxNumber = dto.TaxNumber,
                TaxAddress = new TaxAddress
                {
                    Province = dto.TaxAddress.Province,
                    District = dto.TaxAddress.District
                },
                Verified = true,          // otomatik onaylı
                CreatedAt = DateTime.UtcNow
            };

            _db.Companies.Add(company);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCompany), new { id = company.Id }, company);
        }

        [HttpPut("{id}/toggle")]
        public async Task<IActionResult> ToggleCompanyStatus(Guid id)
        {
            var company = await _db.Companies.FindAsync(id);
            if (company == null) return NotFound();

            company.Disabled = !company.Disabled;
            await _db.SaveChangesAsync();
            return Ok(company);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            var company = await _db.Companies.FindAsync(id);
            if (company == null) return NotFound();

            _db.Companies.Remove(company);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("seed")]
        public async Task<IActionResult> SeedCompanies()
        {
            if (await _db.Companies.AnyAsync())
                return BadRequest(new { message = "Şirketler zaten mevcut." });

            var seedData = new List<Company>
    {
        new Company
        {
            Title = "Acme Ltd.",
            Phone = "905551112233",
            Email = "acme@example.com",
            Balance = 1000,
            Address = "İstanbul, Türkiye",
            TaxNumber = 1234567890,
            TaxAddress = new TaxAddress { Province = "İstanbul", District = "Kadıköy" },
            Verified = true
        },
        new Company
        {
            Title = "Beta Co.",
            Phone = "905552223344",
            Email = "beta@example.com",
            Balance = 500,
            Address = "Ankara, Türkiye",
            TaxNumber = 2345678901,
            TaxAddress = new TaxAddress { Province = "Ankara", District = "Çankaya" },
            Verified = true
        },
        new Company
        {
            Title = "Gamma Inc.",
            Phone = "905553334455",
            Email = "gamma@example.com",
            Balance = 750,
            Address = "İzmir, Türkiye",
            TaxNumber = 3456789012,
            TaxAddress = new TaxAddress { Province = "İzmir", District = "Konak" },
            Verified = true
        },
        new Company
        {
            Title = "Delta LLC",
            Phone = "905554445566",
            Email = "delta@example.com",
            Balance = 1200,
            Address = "Bursa, Türkiye",
            TaxNumber = 4567890123,
            TaxAddress = new TaxAddress { Province = "Bursa", District = "Osmangazi" },
            Verified = true
        },
        new Company
        {
            Title = "Epsilon Ltd.",
            Phone = "905555556677",
            Email = "epsilon@example.com",
            Balance = 800,
            Address = "Antalya, Türkiye",
            TaxNumber = 5678901234,
            TaxAddress = new TaxAddress { Province = "Antalya", District = "Muratpaşa" },
            Verified = true
        },
        new Company
        {
            Title = "Zeta Co.",
            Phone = "905556667788",
            Email = "zeta@example.com",
            Balance = 950,
            Address = "Adana, Türkiye",
            TaxNumber = 6789012345,
            TaxAddress = new TaxAddress { Province = "Adana", District = "Seyhan" },
            Verified = true
        },
        new Company
        {
            Title = "Eta Inc.",
            Phone = "905557778899",
            Email = "eta@example.com",
            Balance = 1100,
            Address = "Konya, Türkiye",
            TaxNumber = 7890123456,
            TaxAddress = new TaxAddress { Province = "Konya", District = "Meram" },
            Verified = true
        },
        new Company
        {
            Title = "Theta LLC",
            Phone = "905558889900",
            Email = "theta@example.com",
            Balance = 670,
            Address = "Samsun, Türkiye",
            TaxNumber = 8901234567,
            TaxAddress = new TaxAddress { Province = "Samsun", District = "Atakum" },
            Verified = true
        },
        new Company
        {
            Title = "Iota Ltd.",
            Phone = "905559990011",
            Email = "iota@example.com",
            Balance = 430,
            Address = "Trabzon, Türkiye",
            TaxNumber = 9012345678,
            TaxAddress = new TaxAddress { Province = "Trabzon", District = "Ortahisar" },
            Verified = true
        },
        new Company
        {
            Title = "Kappa Co.",
            Phone = "905550001122",
            Email = "kappa@example.com",
            Balance = 1500,
            Address = "Eskişehir, Türkiye",
            TaxNumber = 1123456789,
            TaxAddress = new TaxAddress { Province = "Eskişehir", District = "Odunpazarı" },
            Verified = true
        }
    };

            _db.Companies.AddRange(seedData);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Seed veriler eklendi.", count = seedData.Count });
        }
    }
}
