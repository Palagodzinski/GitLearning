using Application.Core.Services.Abstract;
using Application.Core.Services.Concrete;
using FluentValidation.AspNetCore;
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
            //Scoped is a single instance for the duration of the scoped request
            //Transient is a single instance per code request.
            serviceCollection.AddScoped<IUser, User>();
            serviceCollection.AddScoped<IImageFormatValidator, ImageFormatValidator>();
            serviceCollection.AddScoped<IStringCreator, StringCreator>();
            serviceCollection.AddTransient<ICalculatorEngine, CalculatorEngine>();
            serviceCollection.AddTransient<IUserRepository, UserRepository>();
         //   serviceCollection.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("test"));
            serviceCollection.AddDbContext<DBaseContext>(opt =>
            {
                opt.UseSqlServer($"Server = localhost\\SQLEXPRESS; Database = model; Integrated Security=true",b=>b.MigrationsAssembly("Application.Api"));
            });
          
            return serviceCollection;
        }
    }
}
