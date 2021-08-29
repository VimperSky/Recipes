using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Recipes.Infrastructure;

namespace Recipes.WebApi
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            StartDataDbInitializer.CreateDbIfNotExists(host);

            host.Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}