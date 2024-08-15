using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using SendGrid;
using astro_backend;
using astro_backend.Services;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from the .env file before building configuration
Env.Load();

// Rebuild the configuration to ensure environment variables are picked up
builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddControllers() // add controllers for api endpoints 
    .AddJsonOptions(options =>
    {
        //prevents endless loop of referring to each other
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));


// Add SendGrid as service
// builder.Services.Configure<SendGridOptions>(builder.Configuration.GetSection("SendGrid"));
var ApiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
var FromEmail = Environment.GetEnvironmentVariable("SENDGRID_FROMEMAIL");
builder.Services.Configure<SendGridOptions>(options =>
{
    options.ApiKey = ApiKey;
    options.FromEmail = FromEmail;
});
builder.Services.AddSingleton<EmailSender>();

// JWT configuration
var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // ValidateIssuer = true,
        ValidateIssuer = false,
        // ValidateAudience = true,
        // ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        // ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
        // ClockSkew = TimeSpan.Zero // Reduce the allowed clock skew (default is 5 mins)
    };
});

builder.Services.AddAuthorization();


// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});


var app = builder.Build();


// Test the database connection
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        // Attempt to open a connection to the database
        dbContext.Database.OpenConnection();
        dbContext.Database.CloseConnection();
        Console.WriteLine("Connection to the database was successful!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while connecting to the database: {ex.Message}");
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast = Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast")
// .WithOpenApi();


// Use CORS
app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }
