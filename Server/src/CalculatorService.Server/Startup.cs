using CalculatorService.Server.Application.Calculator.Add;
using CalculatorService.Server.Application.Calculator.Sub;
using CalculatorService.Server.Application.DTOs;
using CalculatorService.Server.Application.Journal.Behaviors;
using CalculatorService.Server.Application.Journal.Interfaces;
using CalculatorService.Server.Application.Journal.Services;
using CalculatorService.Server.Filters;
using CalculatorService.Server.Responses;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace CaltulatorService.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ConcurrentDictionary<string, ICollection<JournalOperation>>>();
            services.AddScoped<IJournalService, InMemoryJournalService>();

            services.AddMediatR(typeof(SubHandler));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TrackOperationBehavior<,>));

            services
                .AddControllers(opt =>
                {
                    opt.Filters.Add<ExceptionFilter>();
                })
                .ConfigureApiBehaviorOptions(opt =>
                {
                    opt.InvalidModelStateResponseFactory = c =>
                    {
                        var error = ErrorResponse.BadRequestErrorResponse(c);
                        return new BadRequestObjectResult(error);
                    };
                })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AddRequestValidator>());


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CalculatorService.Server", Version = "v1" });

                // When running integration tests the XML won't be copied to the test assembly, and we can just ignore it
                if (!Environment.IsEnvironment("IntegrationTests"))
                {
                    var xmlDocFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlDocFile);
                    c.IncludeXmlComments(xmlPath);
                }
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSerilogRequestLogging(opt =>
            {
                opt.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
                    diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
                    diagnosticContext.Set("RequestId", httpContext.TraceIdentifier);
                };
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CalculatorService.Server v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
