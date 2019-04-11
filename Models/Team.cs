using System.ComponentModel.DataAnnotations;

namespace canadian_sportsball.Models
{
    public class Team
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Mascot { get; set; }
    }
}