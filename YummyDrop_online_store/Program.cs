using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using YummyDrop_online_store.Data;
using YummyDrop_online_store.Services.GeneratorService;
using YummyDrop_online_store.Services.RandomizeService;
using Microsoft.Extensions.DependencyInjection;
using YummyDrop_online_store.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();


builder.Services.AddHttpClient();
builder.Services.AddScoped<IRandomizeService, RandomizeSerivce>();

builder.Services.AddScoped<IGeneratorService, GeneratorService>();



//add controllers
builder.Services.AddControllers().AddJsonOptions(options => {    
        options.JsonSerializerOptions.WriteIndented = true;
    });
 
//builder.Services.AddScoped<YummyAPIController>();




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
//app.MapFallbackToPage("/_Host");

//add routing
app.UseRouting();
//add endpoints
app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
    endpoints.MapFallbackToPage("/_Host");
});


app.Run();
