using System.ComponentModel.DataAnnotations;

namespace canadian_sportsball.Models
{
    public class Game
    {
        public int Id { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Team1Id { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Team2Id { get; set; }
    }
}