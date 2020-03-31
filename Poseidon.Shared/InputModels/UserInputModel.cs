using System.ComponentModel.DataAnnotations;

namespace Poseidon.Shared.InputModels
{
    public class UserInputModel
    {
        public int Id { get; set; }
        
        [MaxLength(100)]
        public string Username { get; set; }
        
        [MaxLength(100)]
        public string Password { get; set; }
        
        [MaxLength(100)]
        public string FullName { get; set; }
        
        [MaxLength(100)]
        public string Role { get; set; }
    }
}