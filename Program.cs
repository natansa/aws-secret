using Amazon.SecretsManager;
using Secret.Aws;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAmazonSecretsManager, AmazonSecretsManagerClient>(aws => 
{
    return new AmazonSecretsManagerClient
    (
        awsAccessKeyId: "Coloque aqui sua Aws Access Key", 
        awsSecretAccessKey: "Coloque aqui sua Aws Secret Access Key", 
        region: Amazon.RegionEndpoint.USEast1
    );
});
builder.Services.AddTransient<ISecretManager, SecretManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
