using System;
using System.ComponentModel.DataAnnotations;

namespace Poseidon.Shared.InputModels
{
    public class TradeInputModel
    {
        public int Id { get; set; }
        
        [MaxLength(100)]
        public string Account { get; set; }
        
        [MaxLength(100)]
        public string Type { get; set; }

        [Range(0, double.MaxValue)]
        public double? BuyQuantity { get; set; }
        
        [Range(0, double.MaxValue)]
        public double? SellQuantity { get; set; }
        
        [DataType(DataType.Currency)]
        [Range(0.0, 1000000000000)]
        public decimal? BuyPrice { get; set; }
        
        [DataType(DataType.Currency)]
        [Range(0.0, 1000000000000)]
        public decimal? SellPrice { get; set; }

        [DataType(DataType.Date)]
        public DateTime? TradeDate { get; set; }
        
        [MaxLength(100)]
        public string Security { get; set; }
        
        [MaxLength(100)]
        public string Status { get; set; }
        
        [MaxLength(100)]
        public string Trader { get; set; }
        
        [MaxLength(100)]
        public string Benchmark { get; set; }
        
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