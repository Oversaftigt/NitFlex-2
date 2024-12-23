using Dapr.Workflow;
using Microsoft.EntityFrameworkCore;

using RentalMicroservice.Data;
using RentalMicroservice.Repositories;
using RentalMicroservice.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Dapr
builder.Services.AddControllers().AddDapr();
builder.Services.AddDaprClient();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//EF
builder.Services.AddDbContext<RentalDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDB")));

//Dependency Injection

builder.Services.AddScoped<IRentalRepository, RentalRepository>();
builder.Services.AddScoped<IRentalService, RentalService>();


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("https://localhost:7261") // Blazor WebAssembly-URL
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

app.MapControllers();
app.MapSubscribeHandler();
app.UseCloudEvents();

app.Run();
