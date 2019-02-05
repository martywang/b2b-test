using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Audatex.B2B.SDK.FNOL
{
	public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)				
				.UseStartup<Startup>();
    }
}
