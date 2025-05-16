var builder = WebApplication.CreateBuilder(args);

// Configure PostgreSQL connection string (override via environment variables in prod)
var connectionString = builder.Configuration.GetConnectionString("PostgresDb");

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
