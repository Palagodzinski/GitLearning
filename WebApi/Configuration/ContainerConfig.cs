using Application.Api.Controllers;
using Application.Core;
using Application.Core.Services.Abstract;
using Application.Core.Services.Concrete;
using Autofac;
using Autofac.Integration.Mvc;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using System.Web.Mvc;

namespace Application.Api.Configuration
{
    public class ContainerConfig : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<JobManager>().Named<IJobManager>("HangfireManager").InstancePerLifetimeScope();
            containerBuilder.RegisterType<User>().As<IUser>().InstancePerDependency();
            containerBuilder.RegisterType<ImageFormatValidator>().As<IImageFormatValidator>().InstancePerDependency();
            containerBuilder.RegisterType<StringCreator>().As<IStringCreator>().InstancePerDependency();
            containerBuilder.RegisterType<CalculatorEngine>().As<ICalculatorEngine>().InstancePerDependency();
            containerBuilder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerDependency();
            containerBuilder.RegisterType<DBaseContext>().SingleInstance();
            containerBuilder.Register(x =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<DBaseContext>();
                optionsBuilder.UseSqlServer($"Server = localhost\\SQLEXPRESS; Database = model;" +
                    $"User ID=admin;Password=Cdnoptima*1;" +
                    $" Integrated Security=false;Trusted_Connection=False;", x => x
                     .MigrationsAssembly("Application.Api"))
                    .UseLazyLoadingProxies();
                return new DBaseContext(optionsBuilder.Options);
            }).AsSelf().SingleInstance();
            containerBuilder.RegisterType<UserController>().InstancePerRequest();
            containerBuilder.RegisterType<HangfireController>().InstancePerRequest();
            containerBuilder.RegisterType<SimpleController>().InstancePerRequest();
               
            base.Load(containerBuilder);
        }
    }
}
