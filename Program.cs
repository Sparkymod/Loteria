using Loteria;
using Serilog;
using Loteria.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Loteria.Data.Extensions;
using Loteria.Data.Services;
using Loteria.Data.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddAllServicesAvailable();

builder.Host.UseSerilog(Settings.InitializeSerilog());

var app = builder.Build();

DotEnv.Load();
Settings.InitDatabase();
Generator gen = new();
gen.Start();

//Extractor ext = new();
//ext.ExecuteScript();


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();