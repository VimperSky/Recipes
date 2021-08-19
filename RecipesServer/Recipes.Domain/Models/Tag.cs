namespace Recipes.Domain.Models
{
    public class Tag
    {
        public int Id { get; init; }
        
        public string Value { get; set; }

        public static Tag Create(string value)
        {
           return new Tag
           {
               Value = value
           };
        }
    }
}