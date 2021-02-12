using DBContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace DentalClinicAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var dbContext = new DentalClinicDBContext())
            {
                dbContext.Database.EnsureCreated();
                dbContext.SaveChanges();
            }
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
