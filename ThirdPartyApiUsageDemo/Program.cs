using Microsoft.Net.Http.Headers;
using ThirdPartyApiUsageDemo.Clients;
using ThirdPartyApiUsageDemo.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ISwapiClientRepository, SwapiClientRepository>();
builder.Services.AddTransient<IUserSqlRepository, UserSqlRepository>();

//HttpClient is the class used to make calls to other web APIs.
//You can use HttpClient with dependency injection by adding it like this.
builder.Services.AddHttpClient("Swapi", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://swapi.dev/api/");

    httpClient.DefaultRequestHeaders.Add(
        HeaderNames.Accept, "application/json");
    //include any headers required by the api you're contacting
    //if you're including API keys, make sure to put them in your secrets.json and grab them out of there.
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
