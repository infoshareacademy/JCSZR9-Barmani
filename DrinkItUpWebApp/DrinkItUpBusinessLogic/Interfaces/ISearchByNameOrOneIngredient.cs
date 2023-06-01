using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpBusinessLogic.Interfaces
{
	public interface ISearchByNameOrOneIngredient
	{
		void SearchByName(string input);

		void SearchByOneIngredient(string input);
	}
}
