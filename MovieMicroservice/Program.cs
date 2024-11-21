using Dapr.Workflow;
using Microsoft.EntityFrameworkCore;
using MovieMicroservice.Activities.External;
using MovieMicroservice.Activities.Internal;
using MovieMicroservice.Data;
using MovieMicroservice.Repositories;
using MovieMicroservice.Services;
using MovieMicroservice.Workflows;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllers().AddDapr();
builder.Services.AddDaprClient();
builder.Services.AddDaprWorkflow(options =>
{
    options.RegisterWorkflow<FetchMovieWorkflow>();

    options.RegisterActivity<GetMovieLinkActivity>();
    options.RegisterActivity<GetUserInfoActivity>();
    options.RegisterActivity<CheckValidRentalForUserActivity>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//EF
builder.Services.AddDbContext<MovieDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("AzureSql")));

//Dependency injection
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IMovieService, MovieService>();

var app = builder.Build();

app.MapDefaultEndpoints();

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
