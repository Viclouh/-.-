
using API.Other;
using API.Swagger;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using System.Text;
using API.Database;
using API.Models;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Context context = new Context();
            Parsing parsing= new Parsing();
            if (context.Courses. Count() == 0 || context.Teachers.Count() == 0 || context.Disciplines.Count() == 0 
                || context.Cabinets.Count() == 0)
            {
                List<Task> tasks = new List<Task>();
                /*List<Task> tasks = parsing.ParseAllDataAsync();
                Task.WaitAll(tasks.ToArray());*/
                if(context.Courses.Count() == 0)
                {
                    Task CGTask = new Task(() => parsing.ParseCoursesAndGroups());
                    CGTask.Start();
                    tasks.Add(CGTask);
                    Task.WaitAny(CGTask);
                    context.Courses.AddRange(parsing.Courses);
                    context.Groups.AddRange(parsing.Groups);
                }
                if (context.Teachers.Count() == 0)
                {
                    Task TeacherTask = new Task(() => parsing.ParseTeachers());
                    TeacherTask.Start();
                    tasks.Add(TeacherTask);
                    Task.WaitAny(TeacherTask);
                    context.Teachers.AddRange(parsing.Teachers);
                }
				if (context.Cabinets.Count() == 0)
				{
					Task CabinetTask = new Task(() => parsing.ParseCabinets());
                    CabinetTask.Start();
					tasks.Add(CabinetTask);
					Task.WaitAny(CabinetTask);
					context.Cabinets.AddRange(parsing.Cabinets);
				}
				if (context.Disciplines.Count() == 0)
                {
					Task DisciplineTask = new Task(() => parsing.ParseDisciplines());
                    DisciplineTask.Start();
					tasks.Add(DisciplineTask);
					Task.WaitAny(DisciplineTask);
					context.Disciplines.AddRange(parsing.Disciplines);
                }

                Task.WaitAll(tasks.ToArray());
                context.SaveChanges();
            }

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // указывает, будет ли валидироваться издатель при валидации токена
                        ValidateIssuer = true,
                        // строка, представляющая издателя
                        ValidIssuer = AuthOptions.ISSUER,
                        // будет ли валидироваться потребитель токена
                        ValidateAudience = true,
                        // установка потребителя токена
                        ValidAudience = AuthOptions.AUDIENCE,
                        // будет ли валидироваться время существования
                        ValidateLifetime = true,
                        // установка ключа безопасности
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        // валидация ключа безопасности
                        ValidateIssuerSigningKey = true,
                    };
                });


            builder.Services.AddControllers();

            builder.Services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

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


                // Добавьте политику безопасности
                c.OperationFilter<AuthorizationOperationFilter>();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
    public class AuthOptions
    {
        public const string ISSUER = "localhost"; // издатель токена
        public const string AUDIENCE = "localhost"; // потребитель токена
        const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}