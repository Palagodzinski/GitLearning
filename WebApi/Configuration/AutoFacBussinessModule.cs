using Application.Core.Services.Abstract;
using Application.Core.Services.Concrete;
using Autofac;

namespace Application.Api
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<JobManager>().As<IJobManager>().SingleInstance();
            builder.RegisterType<User>().As<IUser>().InstancePerDependency();
            builder.RegisterType<ImageFormatValidator>().As<IImageFormatValidator>().InstancePerDependency();
            builder.RegisterType<StringCreator>().As<IStringCreator>().InstancePerDependency();
            builder.RegisterType<CalculatorEngine>().As<ICalculatorEngine>().InstancePerDependency();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerDependency();
        }
    }
}
