
using APP.Data;
using APP.Services;
using Microsoft.EntityFrameworkCore;

namespace APP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ====================================
            // 1. ĐĂNG KÝ CONTROLLERS
            // ====================================
            builder.Services.AddControllers();

            // ====================================
            // 2. ĐĂNG KÝ AUTH SERVICE
            // ====================================
            // Tạm dùng Singleton cho tài khoản mock trong RAM.
            builder.Services.AddSingleton<AuthService>();

            // ====================================
            // 3. CẤU HÌNH DATABASE POSTGRESQL
            // ====================================
            var connectionString = builder.Configuration
                .GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "ConnectionStrings:DefaultConnection is missing."
                );
            }

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            // ====================================
            // 4. CẤU HÌNH CORS
            // ====================================
            var allowedOrigins = builder.Configuration
                .GetSection("Cors:AllowedOrigins")
                .Get<string[]>() ?? Array.Empty<string>();

            if (allowedOrigins.Length == 0)
            {
                throw new InvalidOperationException(
                    "Cors:AllowedOrigins is missing."
                );
            }

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("FrontendPolicy", policy =>
                {
                    policy.WithOrigins(allowedOrigins)
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            // ====================================
            // 5. BUILD APPLICATION
            // ====================================
            var app = builder.Build();

            // ====================================
            // 6. MIDDLEWARE PIPELINE
            // ====================================
            if (!app.Environment.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            app.UseCors("FrontendPolicy");

            // Chưa bật Authentication Middleware
            // vì JWT/Identity chưa được cấu hình.
            app.UseAuthorization();

            // ====================================
            // 7. REGISTER ENDPOINTS
            // ====================================
            app.MapControllers();

            // ====================================
            // 8. START SERVER
            // ====================================
            app.Run();
        }
    }
}
