using Microsoft.Net.Http.Headers;
using ThirdPartyApiUsageDemo.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ISwapiRepository, SwapiRepository>();
builder.Services.AddHttpClient("Swapi", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://swapi.dev/api/");

    httpClient.DefaultRequestHeaders.Add(
        HeaderNames.Accept, "application/json");
    //include any headers required by the api you're contacting
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
