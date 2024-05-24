using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetApps.api.Models;

namespace PetApps.api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetTypes> PetTypes { get; set; }
        public DbSet<Banner> Banner { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<OrderDetails> OrderDetail { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Province> Province { get; set; }


    }
}
