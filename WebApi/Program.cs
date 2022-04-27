using Application.Core;
using Application.Core.Services.Abstract;
using Application.Core.Services.Concrete;
using BenchmarkDotNet.Running;
using FluentValidation.AspNetCore;
using Hangfire;
using Microsoft.EntityFrameworkCore;


IJobManager _jobManager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRegisteredCoreServices();
builder.Services.AddMvc().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Program>());
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//BenchmarkRunner.Run(typeof(Program).Assembly);
//BenchmarkRunner.Run<StringCreator>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHangfireDashboard("/mydashboard");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

 _jobManager = builder.Services.BuildServiceProvider().CreateScope()
    .ServiceProvider.GetRequiredService<IJobManager>();
_jobManager.VerifyDelays();

app.Run();

