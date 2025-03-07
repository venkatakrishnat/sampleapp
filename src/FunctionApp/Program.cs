using Azure.Identity;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Configuration.AddAzureAppConfiguration(options =>
 {
     var appConfigEndpoint = builder.Configuration.GetValue<string>("APP_CONFIGURATION_ENDPOINT");
     var clientId = builder.Configuration.GetValue<string>("MANAGED_IDENTITY_CLIENT_ID");

     if (string.IsNullOrEmpty(appConfigEndpoint))
     {
         throw new Exception("Missing Azure App Configuration endpoint");
     }

     if (string.IsNullOrEmpty(clientId))
     {
         throw new Exception("Missing Managed Identity client ID");
     }

     var token = new ChainedTokenCredential(
        new VisualStudioCredential(),
        new ManagedIdentityCredential(clientId));

     options.Connect(new Uri(appConfigEndpoint), token);
     options.ConfigureKeyVault(keyVaultOptions =>
     {
         keyVaultOptions.SetCredential(token);
     });
 });

// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

builder.Build().Run();
