using System.Text.Json;
using Enpal.HomeChallenge.Api.Converters;
using Enpal.HomeChallenge.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
        options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter());
    });

builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<Program>());

builder.Services
    .AddMediatr()
    .AddInfrastructureServices()
    .AddRepositories();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();


app.Run();

public partial class Program;