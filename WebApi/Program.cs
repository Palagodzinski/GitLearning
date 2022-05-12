using Application.Core;
using Application.Core.Services.Abstract;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.SqlServer;
using Application.Api.Configuration;
using Newtonsoft.Json;


var builder = WebApplication.CreateBuilder(args);
string connectionString = 
    "Server = localhost\\SQLEXPRESS; Database = model;User ID=admin;Password=Cdnoptima*1;" +
    "Integrated Security=false;Trusted_Connection=False;";
var sqlStorage = new SqlServerStorage(connectionString);
JobStorage.Current = sqlStorage;
builder.Services.AddHangfire(configuration => configuration
.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
.UseSimpleAssemblyNameTypeSerializer()
.UseRecommendedSerializerSettings()
.UseSqlServerStorage(connectionString, new SqlServerStorageOptions
{
    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
    QueuePollInterval = TimeSpan.Zero,
    UseRecommendedIsolationLevel = true,
    DisableGlobalLocks = true
}));

builder.Services.AddHangfireServer();
builder.Services.AddRegisteredCoreServices();
builder.Services.AddMvc().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
    .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Program>());
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//BenchmarkRunner.Run(typeof(Program).Assembly);
//BenchmarkRunner.Run<StringCreator>();

//Adding AutoFac 
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new ContainerConfig());
});

var app = builder.Build();

//Setting HangFire ReccuringJob
ILifetimeScope autofacRoot = app.Services.GetAutofacRoot();
GlobalConfiguration.Configuration.UseAutofacActivator(autofacRoot);
var jobManager = autofacRoot.ResolveNamed<IJobManager>("HangfireManager");
RecurringJob.AddOrUpdate(() => jobManager.VerifyDelays(), Cron.Daily);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHangfireDashboard("/mydashboard");
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHangfireDashboard();
});
app.MapControllers();
app.Run();