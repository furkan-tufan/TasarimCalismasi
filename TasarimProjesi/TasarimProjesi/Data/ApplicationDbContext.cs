using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TasarimProjesi.Models;

namespace TasarimProjesi.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Request> Request { get; set; }
        public DbSet<Purchasing> Purchasing { get; set; }
        public DbSet<PurchasingItem> PurchasingItem { get; set; }


    }
}