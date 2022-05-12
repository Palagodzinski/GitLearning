using Application.Core.Services.Abstract;
using Application.Core.Services.Concrete;
using FluentValidation.AspNetCore;
using Hangfire;
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
            serviceCollection.AddHangfire(x =>
            x.UseSqlServerStorage(
                "Server = localhost\\SQLEXPRESS; Database = model;User ID=admin;Password=Cdnoptima*1;" +
                "Integrated Security=false;Trusted_Connection=False;")
                .UseColouredConsoleLogProvider()
                );

            serviceCollection.AddHangfireServer();
            serviceCollection.AddHttpContextAccessor();

            return serviceCollection;
        }


    }
}
