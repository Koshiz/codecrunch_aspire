//var builder = DistributedApplication.CreateBuilder(args);

//builder.AddProject<Projects.BlazorApp1>("blazorapp1");

//builder.Build().Run();


using CV_test01.AppHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = DistributedApplication.CreateBuilder(args);

// Add Blazor project
builder.AddProject<Projects.BlazorApp1>("blazorapp1");

// Add services here
builder.Services.AddDbContext<CvContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

// Add HttpClient for use in Blazor
builder.Services.AddHttpClient();

// Add any other services you need (e.g., OpenAI service)
builder.Services.AddTransient<IOpenAiService, OpenAiService>();

// Build the application
builder.Build().Run();
