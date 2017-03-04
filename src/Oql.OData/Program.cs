using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using System.Linq;

namespace Oql.OData
{
    public class Program
    {


        public static void Main(string[] args)
        {

            IWebHostBuilder host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                ;

            if (args.Any())
            {
                host = host.UseUrls($"http://{args.First()}:{args.ElementAt(1)}");
            }

            host.Build().Run();
        }
    }
}
