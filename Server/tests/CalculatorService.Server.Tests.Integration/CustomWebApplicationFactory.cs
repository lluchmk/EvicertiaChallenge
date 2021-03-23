using CalculatorService.Server.Application.DTOs;
using CaltulatorService.Server;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace CalculatorService.Server.Tests.Integration
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<Startup>
        where TStartup : class
    {
        public ConcurrentDictionary<string, ICollection<JournalOperation>> Journal;

        public CustomWebApplicationFactory()
        {
            Journal = new();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("IntegrationTests");
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton(Journal);
            });
        }
    }
}
