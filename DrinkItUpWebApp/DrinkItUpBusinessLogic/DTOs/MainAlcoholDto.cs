namespace DrinkItUpBusinessLogic.DTOs
{
    public class MainAlcoholDto
    {
        public int MainAlcoholId { get; set; }
        public string Name { get; set; }

        public static implicit operator List<object>(MainAlcoholDto v)
        {
            throw new NotImplementedException();
        }
    }
}