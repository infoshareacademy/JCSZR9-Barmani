namespace DrinkItUp.BusinessLogic.Model
{
    public class Difficulty
    {

        public int Id { get; set; }

        public string Level { get; set; }


        public Difficulty(int id, string level)
        {
            Id = id;
            Level = level;
        }
    }
}