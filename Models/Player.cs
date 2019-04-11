using System.ComponentModel.DataAnnotations;

namespace canadian_sportsball.Models
{
    public class Player
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int TeamId { get; set; }
    }
}