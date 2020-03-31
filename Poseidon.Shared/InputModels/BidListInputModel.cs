using System;
using System.ComponentModel.DataAnnotations;

namespace Poseidon.Shared.InputModels
{
    public class BidListInputModel : IInputModel
    {
        public int Id { get; set; }
        
        [MaxLength(100)]
        public string Account { get; set; }
        
        [MaxLength(100)]
        public string Type { get; set; }
        
        [Range(0.0, double.MaxValue)]
        public double? BidQuantity { get; set; }
        
        [Range(0.0, double.MaxValue)]
        public double? AskQuantity { get; set; }
        
        [Range(0.0, double.MaxValue)]
        public double? Bid { get; set; }
        
        [Range(0.0, double.MaxValue)]
        public double? Ask { get; set; }
        
        [MaxLength(100)]
        public string Benchmark { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime? BidListDate { get; set; }
        
        [MaxLength(100)]
        public string Commentary { get; set; }
        
        [MaxLength(100)]
        public string Security { get; set; }
        
        [MaxLength(100)]
        public string Status { get; set; }
        
        [MaxLength(100)]
        public string Trader { get; set; }
        
        [MaxLength(100)]
        public string Book { get; set; }
        
        [MaxLength(100)]
        public string CreationName { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime? CreationDate { get; set; }
        
        [MaxLength(100)]
        public string RevisionName { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime? RevisionDate { get; set; }
        
        [MaxLength(100)]
        public string DealName { get; set; }
        
        [MaxLength(100)]
        public string DealType { get; set; }
        
        [MaxLength(100)]
        public string SourceListId { get; set; }
        
        [MaxLength(100)]
        public string Side { get; set; }
    }
}