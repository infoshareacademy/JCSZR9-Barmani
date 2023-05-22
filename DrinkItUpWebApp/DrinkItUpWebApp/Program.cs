using DrinkItUpBusinessLogic;
using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
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
                .AddRazorRuntimeCompilation(); // Wprowadzanie zmian w HTMLu na bie¿¹co!

            // DbContext
            builder.Services.AddDbContext<DrinkContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DrinkContextCS")));

            //Services
            builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();

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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=AboutUs}/{id?}");

            app.Run();
        }
    }
}