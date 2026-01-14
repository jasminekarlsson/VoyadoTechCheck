using TechCheck.WebAPI.Interface;
using TechCheck.WebAPI.Models.Configuration;
using TechCheck.WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();

builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<ISearchcollector, GoogleService>();
builder.Services.AddScoped<ISearchcollector, BingService>();

builder.Services.Configure<GoogleApiOptions>(
    builder.Configuration.GetSection("GoogleApi"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWasmClient", policy =>
    {
        policy.WithOrigins("https://localhost:7059") // URL of your WASM client
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowWasmClient");

app.UseHttpsRedirection();

app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

await app.RunAsync();

public static partial class Program { }