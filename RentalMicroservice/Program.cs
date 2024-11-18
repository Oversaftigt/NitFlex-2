using Microsoft.EntityFrameworkCore;
using RentalMicroservice.Data;
using RentalMicroservice.Repositories;
using RentalMicroservice.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddDapr();
builder.Services.AddDaprClient();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//EF
builder.Services.AddDbContext<RentalDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("AzureSql")));

//Dependency Injection

builder.Services.AddScoped<IRentalRepository, RentalRepository>();
builder.Services.AddScoped<IRentalService, RentalService>();

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
app.MapSubscribeHandler();
app.UseCloudEvents();

app.Run();
