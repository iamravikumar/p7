using System;
using System.ComponentModel.DataAnnotations;

namespace Poseidon.Shared.InputModels
{
    public class CurvePointInputModel : IInputModel
    {
        public int Id { get; set; }
        
        [Range(0, short.MaxValue)]
        public int? CurveId { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime? AsOfDate { get; set; }
        
        [Range(0.0, double.MaxValue)]
        public double? Term { get; set; }
        
        [Range(0.0, double.MaxValue)]
        public double? Value { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime? CreationDate { get; set; }
    }
}