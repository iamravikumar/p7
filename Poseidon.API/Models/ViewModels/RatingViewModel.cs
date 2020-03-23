namespace Poseidon.API.Models
{
    public class RatingViewModel
    {
        public short Id { get; set; }
        public string MoodysRating { get; set; }
        public string SandPrating { get; set; }
        public string FitchRating { get; set; }
        public short? OrderNumber { get; set; }
    }
}