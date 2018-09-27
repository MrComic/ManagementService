using System;
using Xunit;
using ManagementService.Data;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using ManagementService.Web;
using Microsoft.AspNetCore.TestHost;

namespace ManagementService.UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

            var webHostBuilder = new WebHostBuilder();
            webHostBuilder.UseKestrel()
                          .UseContentRoot(Directory.GetCurrentDirectory())
                          .UseIISIntegration()
                          .UseStartup<Startup>()
                          .Build()
                          .Run();
           var testServer = new TestServer(webHostBuilder);
            var response = testServer.CreateClient().GetAsync("/api/user/TestMenu").Result;
            response.EnsureSuccessStatusCode();

            //var result = response.Content.ReadAsAsync<Data[]>().Result;


        }
    }
}
