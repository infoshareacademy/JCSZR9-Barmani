using DrinkItUpBusinessLogic;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpBusinessLogic.MailKitSender;
using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using DrinkItUpWebApp.Middleware.Authorization;
using DrinkItUpWebApp.Middleware.CustomExceptionMiddleware;
using Microsoft.EntityFrameworkCore;

namespace DrinkItUpWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews()
                .AddRazorRuntimeCompilation(); // Wprowadzanie zmian w HTMLu przy zbudowanej solucji!

            builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

            // DbContext
            builder.Services.AddDbContext<DrinkContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DrinkContextCS")));

            //Services
            builder.Services.AddScoped<ISearchByIngredients, SearchByIngredients>();
            builder.Services.AddScoped<ISearchByNameOrOneIngredient, SearchByNameOrOneIngredient>();
            builder.Services.AddScoped<IGetDrinkDetails, GetDrinkDetails>();
            builder.Services.AddScoped<IUnitService, UnitService>();
            builder.Services.AddScoped<IMainAlcoholService, MainAlcoholService>();
            builder.Services.AddScoped<IDifficultyService, DifficultyService>();
            builder.Services.AddScoped<IIngredientService, IngredientService>();
            builder.Services.AddScoped<IDrinkService, DrinkService>();
            builder.Services.AddScoped<IByCategoryService, ByCategoryService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

            // Repositories
            builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
            builder.Services.AddScoped<IDrinkRepository, DrinkRepository>();
            builder.Services.AddScoped<IDrinkIngredientRepository, DrinkIngredientRepository>();
            builder.Services.AddScoped<IUnitRepository, UnitRepository>();
            builder.Services.AddScoped<IMainAlcoholRepository, MainAlcoholRepository>();
            builder.Services.AddScoped<IDifficultyRepository, DifficultyRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();

            //MailSender
            var emailConfig = builder.Configuration.GetSection("MailSettings").Get<MailSettings>();
            builder.Services.AddSingleton(emailConfig);
            builder.Services.AddSingleton<IEmailService, EmailService>();
            //AutoMapper
            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<JwtMiddleware>();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=AgeVerification}/{id?}");

            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.Run();
        }
    }
}
public partial class Program { }