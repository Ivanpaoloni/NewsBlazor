using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using NewsBlazor.Services;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using NewsBlazor.Auth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddAuthorizationCore();
//servicio de authentication falso
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProviderFalse>();
builder.Services.AddScoped<JWTAuthenticationProvider>();
builder.Services.AddScoped<ILoginService, JWTAuthenticationProvider>(provider => provider.GetRequiredService<JWTAuthenticationProvider>());

//servicios de api news.
builder.Services.AddSingleton<NewsService>();
builder.Services.AddSingleton<CategoryService>();
builder.Services.AddScoped<UserService>();
//builder.Services.AddScoped<AuthenticationStateProvider, JWTAuthenticationProvider>(provider => provider.GetRequiredService<JWTAuthenticationProvider>());

//servicio de api weather
builder.Services.AddScoped<WeatherService>();

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
