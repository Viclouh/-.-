
using API.Services;
using API.Swagger;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using System.Text;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<Database.Context>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetSection("Database").GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // ���������, ����� �� �������������� �������� ��� ��������� ������
                        ValidateIssuer = true,
                        // ������, �������������� ��������
                        ValidIssuer = AuthOptions.ISSUER,
                        // ����� �� �������������� ����������� ������
                        ValidateAudience = true,
                        // ��������� ����������� ������
                        ValidAudience = AuthOptions.AUDIENCE,
                        // ����� �� �������������� ����� �������������
                        ValidateLifetime = true,
                        // ��������� ����� ������������
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        // ��������� ����� ������������
                        ValidateIssuerSigningKey = true,
                    };
                });


            builder.Services.AddControllers();
            builder.Services.AddScoped<LessonService>();
            builder.Services.AddScoped<GroupService>();
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<TeacherService>();
            builder.Services.AddScoped<AudienceService>();
            builder.Services.AddScoped<WeekService>();
            builder.Services.AddScoped<GroupTeacherService>();
            builder.Services.AddScoped<ParserService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AppCORSPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod().SetIsOriginAllowedToAllowWildcardSubdomains();
                });
            });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Your API",
                    Version = "v1"
                });

                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });


                // �������� ������������
                c.OperationFilter<AuthorizationOperationFilter>();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                
            }
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseCors("AppCORSPolicy");

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }

    public class AuthOptions
    {
        public const string ISSUER = "AKVT.Raspisanie API"; // �������� ������
        public const string AUDIENCE = "AKVT.Raspisanie Client"; // ����������� ������
        const string KEY = "mysupersecret_secretkey!1233";   // ���� ��� ��������
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}