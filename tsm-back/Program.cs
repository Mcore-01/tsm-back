using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using tsm_back.Data;
using tsm_back.Middleware;
using tsm_back.Repositories;
using tsm_back.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        //var TSMContext = new TSMContext(true);
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<TSMContext>();
        builder.Services.AddScoped<UserService>();
        builder.Services.AddScoped<UserRepository>();
        builder.Services.AddScoped<BoardService>();
        builder.Services.AddScoped<BoardRepository>();
        builder.Services.AddScoped<ColumnService>();
        builder.Services.AddScoped<ColumnRepository>();
        builder.Services.AddScoped<TodoService>();
        builder.Services.AddScoped<TodoRepository>();
        builder.Services.AddScoped<StatisticsService>();
        builder.Services.AddScoped<LogRepository>();
        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = AuthOptions.ISSUER,
                    ValidateAudience = true,
                    ValidAudience = AuthOptions.AUDIENCE,
                    ValidateLifetime = true,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                };
            });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: "Development",
                              builder =>
                              {
                                  builder
                                      .WithOrigins("http://localhost:4200")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod()
                                      ;
                              });
            options.AddPolicy(
              "Development",
              builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
            );
        });
        builder.Services.AddControllers();
        var app = builder.Build();
        app.UseMiddleware<TodoLoggingMiddleware>();
        app.UseCors("Development");
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
public class AuthOptions
{
    public const string ISSUER = "TSMBack"; 
    public const string AUDIENCE = "TSMClient"; 
    const string KEY = "prostokeyfor1234231dsfsdq3shiphrovki014345";   
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}