using Azure.Core.Serialization;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

// When using AspNetCore integration, the default is to disallow synchronous IO.
// Set AllowSynchronousIO to true to allow synchronous IO when using HttpResponseData.
// builder.Services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);

// Configure the serializer to use Newtonsoft.Json
builder.Services.Configure<WorkerOptions>(workerOptions =>
{
    var settings = NewtonsoftJsonObjectSerializer.CreateJsonSerializerSettings();
    settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    settings.NullValueHandling = NullValueHandling.Ignore;

    workerOptions.Serializer = new NewtonsoftJsonObjectSerializer(settings);
});

builder.Services.AddMvc().AddNewtonsoftJson();

builder.Build().Run();