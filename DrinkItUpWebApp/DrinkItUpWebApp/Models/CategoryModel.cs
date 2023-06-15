namespace DrinkItUpWebApp.Models
{
	public class CategoryModel
	{
		public string CategoryName { get; set; }

		public string Value { get; set; }

		public IEnumerable<string> Values { get; set; }
		public IEnumerable<DrinkSearchModel> Results { get; set; }
	}
}
