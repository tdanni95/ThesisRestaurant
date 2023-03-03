using System.Text.Json.Serialization;
using ThesisRestaurant.Api;
using ThesisRestaurant.Application;
using ThesisRestaurant.Infrastructure;

//dotnet user-secrets init --project .\ThesisRestaurant\
//dotnet user-secrets set --project .\ThesisRestaurant\ "JwtSettings:Servert" "supersecretkeyforthesis"

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddControllers()
    .AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddPresentation(builder.Configuration)
    .AddApplication()
    .AddInfrastructure(builder.Configuration);



var app = builder.Build();
app.UseExceptionHandler("/error");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder.Configuration.GetValue<string>("corsname")!);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
