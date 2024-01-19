using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccess.Contexts
{
    public class ETradeContextFactory : IDesignTimeDbContextFactory<ETradeContext> // ETradeContext objesini oluşturup kullanılmasını sağlayan fabrika class'ı,
                                                                                   // scaffolding işlemleri için bu class oluşturulmalıdır
    {
        public ETradeContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ETradeContext>();

            // Microsoft SQL Server LocalDB için connection string tanımı
            optionsBuilder.UseSqlServer("server=(localdb)\\mssqllocaldb;database=ETradeDB;trusted_connection=true;multipleactiveresultsets=true;trustservercertificate=true;");
            
            // önce veritabanımızın (development veritabanı kullanılması daha uygundur) connection string'ini içeren bir obje oluşturuyoruz

            return new ETradeContext(optionsBuilder.Options); // daha sonra yukarıda oluşturduğumuz obje üzerinden ETradeContext tipinde bir obje dönüyoruz
        }
    }
}
