namespace Poseidon.API.Models
{
    public class RatingInputModel
    {
        public string MoodysRating { get; set; }
        public string SandPrating { get; set; }
        public string FitchRating { get; set; }
        public short? OrderNumber { get; set; }   
    }
}