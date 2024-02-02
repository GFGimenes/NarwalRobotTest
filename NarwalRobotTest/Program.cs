using NarwalRobotTest.Data;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using System;
using NarwalRobotTest.Models;
using NarwalRobotTest;
using NarwalRobotTest.Models.Dictionaries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IValidator<Robot>, RobotValidator>();

builder.Services.AddScoped<ArmElbowMovimentDictionary, ArmElbowMovimentDictionary>();
builder.Services.AddScoped<ArmWristMovimentDictionary, ArmWristMovimentDictionary>();
builder.Services.AddScoped<HeadRotationMovimentDictionary, HeadRotationMovimentDictionary>();
builder.Services.AddScoped<HeadTiltMovimentDictionary, HeadTiltMovimentDictionary>();
builder.Services.AddScoped<ApiContext, ApiContext>();

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<ExceptionFilter>();
});

builder.Services.AddDbContext<ApiContext>
    (opt => {
        opt.UseInMemoryDatabase("ProductDb");
        opt.EnableSensitiveDataLogging();
        }
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
