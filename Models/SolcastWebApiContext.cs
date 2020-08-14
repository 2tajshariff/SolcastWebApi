using Microsoft.EntityFrameworkCore;
using SolcastWebApi.Models;

namespace SolcastWebApi.Models{
    public class SolcastWebApiContext : DbContext{
        public SolcastWebApiContext(DbContextOptions<SolcastWebApiContext> options) : base(options) {}

        public DbSet<User> Users{get;set;}
        
        protected override void OnModelCreating(ModelBuilder builder)  
        {  
            base.OnModelCreating(builder);  
        }  
  
        public override int SaveChanges()  
        {  
            ChangeTracker.DetectChanges();  
            return base.SaveChanges();  
        }
    }
}