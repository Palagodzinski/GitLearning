using Application.Core.Services.Abstract;
using Application.Core.Services.Concrete;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddRegisteredCoreServices(this IServiceCollection serviceCollection)
        {
            //serviceCollection.AddHangfire(x =>
            //x.UseSqlServerStorage(
            //    "Server = localhost\\SQLEXPRESS; Database = model;User ID=admin;Password=Cdnoptima*1;" +
            //    "Integrated Security=false;Trusted_Connection=False;")
            //    .UseColouredConsoleLogProvider()
            //    );

            //serviceCollection.AddHangfireServer();
            //serviceCollection.AddHttpContextAccessor();



            serviceCollection.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage("Server = localhost\\SQLEXPRESS; Database = model;User ID=admin;Password=Cdnoptima*1;" +
                    "Integrated Security=false;Trusted_Connection=False;",
                    new SqlServerStorageOptions
                    {
                        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                        QueuePollInterval = TimeSpan.Zero,
                        UseRecommendedIsolationLevel = true,
                        DisableGlobalLocks = true
                    }));
            serviceCollection.AddHangfireServer();
            serviceCollection.AddHttpContextAccessor();
          
            return serviceCollection;
        }
    }
}
