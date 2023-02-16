using FH.App.Features.Training.AddDetails;
using FH.App.Features.Training.Create;
using FH.App.Features.Training.Delete;
using FH.App.Features.Training.Detail;
using FH.App.Features.Training.List;
using FH.Domain.Repositories;
using FH.Domain.RepositoryInterfaces;
using FH.Infrastructure;
using FH.Infrastructure.Context;
using FH.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<ITrainingRepository, TrainingRepository>();
builder.Services.AddScoped<IExerciseRepository, ExerciseRepository>();

builder.Services.AddScoped<CreateTrainingHandler>();
builder.Services.AddScoped<AddDetailTrainingHandler>();
builder.Services.AddScoped<DetailTrainingHandler>();
builder.Services.AddScoped<ListTrainingHandler>();
builder.Services.AddScoped<DeleteTrainingHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
    await initializer.InitializeAsync();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
