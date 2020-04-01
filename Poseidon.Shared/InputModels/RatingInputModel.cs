using System.ComponentModel.DataAnnotations;

namespace Poseidon.Shared.InputModels
{
    public class RatingInputModel
    {
        public int Id { get; set; }
 
        [MaxLength(100)]
        public string MoodysRating { get; set; }
        
        [MaxLength(100)]
        public string SandPrating { get; set; }
        
        [MaxLength(100)]
        public string FitchRating { get; set; }
        
        [Range(0, short.MaxValue)]
        public int? OrderNumber { get; set; }
    }
}