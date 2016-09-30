namespace CoursesAuth {
    using System;
    using System.IO;
    using Microsoft.AspNetCore.Hosting;

    public class Program {
        public static void Main(string[] args) {
            Console.Title = "CoursesAuth";

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
