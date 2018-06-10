using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Stone.Lancamento.Persistence.Configuration;

namespace Stone.Lancamento.WebApi
{
    public class LancamentosDbContextFactory : IDesignTimeDbContextFactory<LancamentosDbContext>
    {        
        public LancamentosDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build(); 
            var builder = new DbContextOptionsBuilder<LancamentosDbContext>(); 
            var connectionString = configuration.GetConnectionString("DefaultConnection"); 
            builder.UseSqlServer(connectionString); 
            return new LancamentosDbContext(builder.Options);            
        }
    }
}