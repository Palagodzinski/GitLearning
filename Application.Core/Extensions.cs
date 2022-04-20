using Application.Core.Services.Abstract;
using Application.Core.Services.Concrete;
using FluentValidation.AspNetCore;
using Hangfire;
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
            serviceCollection.AddScoped<IUser, User>();
            serviceCollection.AddScoped<IImageFormatValidator, ImageFormatValidator>();
            serviceCollection.AddScoped<IStringCreator, StringCreator>();
            serviceCollection.AddTransient<ICalculatorEngine, CalculatorEngine>();
            serviceCollection.AddTransient<IUserRepository, UserRepository>();
            serviceCollection.AddDbContext<DBaseContext>(opt =>
            {
                opt.UseSqlServer($"Server = localhost\\SQLEXPRESS; Database = model;User ID=admin;Password=Cdnoptima*1; Integrated Security=false;Trusted_Connection=False;", b => b.MigrationsAssembly("Application.Api"));
            });
            serviceCollection.AddHangfire(x => x.UseSqlServerStorage(
                "Server = localhost\\SQLEXPRESS; Database = model;User ID=admin;Password=Cdnoptima*1;" +
                "Integrated Security=false;Trusted_Connection=False;"));
            serviceCollection.AddHangfireServer();
            return serviceCollection;
        }
    }
}
