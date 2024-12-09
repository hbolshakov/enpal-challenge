using System.Net.Http.Headers;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Enpal.HomeChallenge.Api.Tests;

public class TestApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private IContainer? _dbContainer;

    public HttpClient GetClient()
    {
        var client = CreateDefaultClient();
        ClientOptions.HandleCookies = false;
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        return client;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Environment.SetEnvironmentVariable("ConnectionStrings__DbConnectionString", "Host=localhost;Port=5477;Database=coding-challenge;Username=postgres;Password=mypassword123!;");
        builder.UseTestServer();
    }

    public async Task InitializeAsync()
    {
        var dockerImage = new ImageFromDockerfileBuilder()
            .WithDockerfileDirectory(CommonDirectoryPath.GetSolutionDirectory(),
                "Enpal.HomeChallenge.Infrastructure/Data/Database")
            .WithDockerfile("Dockerfile")
            .Build();

        await dockerImage.CreateAsync().ConfigureAwait(false);

        var container = new ContainerBuilder()
            .WithImage(dockerImage)
            .WithPortBinding(5477, 5432)
            .Build();

        await container.StartAsync().ConfigureAwait(false);

        await Task.Delay(TimeSpan.FromSeconds(5));

        _dbContainer = container;
    }

    public new async Task DisposeAsync()
    {
        if (_dbContainer is not null)
        {
            await _dbContainer.StopAsync();
        }
    }
}