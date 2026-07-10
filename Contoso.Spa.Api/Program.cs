using Contoso.Spa.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions
(
    options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = SerializationOptions.Default.PropertyNameCaseInsensitive;
        foreach (var converter in SerializationOptions.Default.Converters)
            options.JsonSerializerOptions.Converters.Add(converter);
    }
);

builder.Services
    .AddAutoMapperServices()
    .AddSpaFlowServices();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
