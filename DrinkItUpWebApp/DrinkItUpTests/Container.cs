﻿using AutoMapper;
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
                .AddDbContextFactory<DrinkContext>(options =>
                {
                    options.UseInMemoryDatabase("_", new InMemoryDatabaseRoot());
                })
                .AddScoped<IUnitRepository, UnitRepository>()
                .AddScoped<IIngredientRepository, IngredientRepository>()
                .AddScoped<IDrinkIngredientRepository, DrinkIngredientRepository>()
                .AddScoped<IDrinkRepository, DrinkRepository>()
                .AddScoped<IMainAlcoholRepository, MainAlcoholRepository>()
                .AddScoped<IDifficultyRepository, DifficultyRepository>()
                .AddScoped<SearchByIngredients>()
                .AddScoped<SearchByNameOrOneIngredient>()
                .AddScoped<GetDrinkDetails>()
                .AddScoped<MainAlcoholService>()
                .AddScoped<DifficultyService>()
                .AddScoped<IngredientService>()
                .AddScoped<DrinkService>()
                .AddScoped<ByCategoryService>()
                .AddScoped<UnitService>()
                .AddAutoMapper(typeof(Program).Assembly);

            _container = _services.BuildServiceProvider();
        }

        public UnitService GetUnitService()
        {
            return _container.GetRequiredService<UnitService>();
        }

        public DifficultyService GetDifficultyService()
        {
            return _container.GetRequiredService<DifficultyService>();
        }

        public MainAlcoholService GetMainAlcoholService()
        {
            return _container.GetRequiredService<MainAlcoholService>();
        }

        public IngredientService GetIngredientService()
        {
            return _container.GetRequiredService<IngredientService>();
        }

        public void EndOfTest()
        {
            var db = _container.GetRequiredService<DrinkContext>();
            db.Database.EnsureDeleted();
            db.ChangeTracker.Clear();
        }

        public void DetachModel()
        {
            var db = _container.GetRequiredService<DrinkContext>();
            db.ChangeTracker.Clear();
        }

    }
}
