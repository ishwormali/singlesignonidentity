
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdServerSelfHost
{
    using Microsoft.Owin.Hosting;
    using Serilog;
    using Server;

    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger=new LoggerConfiguration()
                .WriteTo
                .LiterateConsole(outputTemplate: "{Timestamp:HH:mm} [{Level}] ({Name:l}){NewLine} {Message}{NewLine}{Exception}")
                .CreateLogger();

            using (WebApp.Start<Startup>("http://localhost:5000"))
            {
                Console.WriteLine("server running.....");
                Console.ReadLine();

            }
        }
    }
}
