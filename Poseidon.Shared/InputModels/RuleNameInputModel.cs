using System.ComponentModel.DataAnnotations;

namespace Poseidon.Shared.InputModels
{
    public class RuleNameInputModel
    {
        public int Id { get; set; }
        
        [MaxLength(100)]
        public string Name { get; set; }
        
        [MaxLength(100)]
        public string Description { get; set; }
        
        [MaxLength(100)]
        public string Json { get; set; }
        
        [MaxLength(100)]
        public string Template { get; set; }

        [MaxLength(100)]
        public string SqlStr { get; set; }
        
        [MaxLength(100)]
        public string SqlPart { get; set; }
    }
}