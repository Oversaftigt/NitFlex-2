using Blazored.SessionStorage;
using BlazorNitflex;
using BlazorNitflex.Handlers;
using BlazorNitflex.Services;
using BlazorNitflex.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<AuthenticationHandler>();

//httpclients
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//Moviemicroservice Uri
builder.Services.AddHttpClient("movieclient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7116");
}).AddHttpMessageHandler<AuthenticationHandler>();

//Rentalmicroservice Uri
builder.Services.AddHttpClient("rentalclient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7209");
}).AddHttpMessageHandler<AuthenticationHandler>();

//Identitymicroservice Uri
builder.Services.AddHttpClient("identityclient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7238");
}).AddHttpMessageHandler<AuthenticationHandler>();

builder.Services.AddHttpClient("unauthenticatedIdentityclient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7238");
}).AddHttpMessageHandler<AuthenticationHandler>();

//DI services
builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IRentalService, RentalService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<CustomAuthenticationStateProvider>();

//Blazored session to store jwt
builder.Services.AddBlazoredSessionStorageAsSingleton();

//Radzen 
builder.Services.AddScoped<NotificationService>();
builder.Services.AddRadzenComponents();

builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
