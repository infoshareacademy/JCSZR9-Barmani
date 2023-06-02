using DrinkItUpWebApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpWebApp.DAL.Repositories.Interfaces
{
	public interface IIngredientRepository : ICRUDRepository<Ingredient>, ISearchByNameQueryable<Ingredient>
	{

	}
}
