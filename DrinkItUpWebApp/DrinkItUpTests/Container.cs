using AutoMapper;
using DrinkItUpBusinessLogic;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;

namespace DrinkItUpTests
{
    public class Container
    {
        private IServiceCollection _services;
        private ServiceProvider _container;

        public Container()
        {
             _services = new ServiceCollection()
                .AddDbContext<DrinkContext>(options =>
                {
                    options.UseInMemoryDatabase("_", new InMemoryDatabaseRoot());
                })
                .AddScoped<IUnitRepository, UnitRepository>()
                .AddScoped<IIngredientRepository, IngredientRepository>()
                .AddScoped<IDrinkIngredientRepository, DrinkIngredientRepository>()
                .AddScoped<UnitService>()
                .AddAutoMapper(typeof(Program).Assembly);

            _container = _services.BuildServiceProvider();
        }

        public UnitService GetUnitService()
        {
            return _container.GetRequiredService<UnitService>();
        }

        public void EndOfTest()
        {
            var db = _container.GetRequiredService<DrinkContext>();
            db.Dispose();
        }

        public void DetachModel()
        {
            var db = _container.GetRequiredService<DrinkContext>();
            db.ChangeTracker.Clear();
        }

    }
}
