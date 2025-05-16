using Amazon.SQS;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PostgresDb");

builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseNpgsql(connectionString));

// Register AWS SQS client
builder.Services.AddAWSService<IAmazonSQS>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
