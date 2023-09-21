using Microsoft.EntityFrameworkCore;
using TechTask.Dal;
using TechTask.Dal.Repositories;
using TechTask.Dal.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>((options) =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

builder.Services.AddScoped(typeof(ICrudRepository<>), typeof(Repository<>));
builder.Services.AddControllers();

builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();