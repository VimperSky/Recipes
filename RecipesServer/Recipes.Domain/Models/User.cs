using System.Collections.Generic;

namespace Recipes.Domain.Models
{
    public class User
    {
        public int Id { get; init; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }

        public ICollection<Recipe> Recipes { get; set; }

        public ICollection<Activity> Activities { get; set; }
    }
}