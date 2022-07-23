using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.Entities;

namespace DAL.DbContexts
{
    public class TodebCampDbContext:DbContext
    {
        private IConfiguration _configuration;
        public TodebCampDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // base.OnConfiguring(optionsBuilder.UseSqlServer("Server=TRAYTL0279\\SQLEXPRESS;Database=TodebCamp;Trusted_Connection=True;"));
           var connectionString = _configuration.GetConnectionString("MsComm");
           base.OnConfiguring(optionsBuilder.UseSqlServer(connectionString));
        }
    }
}
