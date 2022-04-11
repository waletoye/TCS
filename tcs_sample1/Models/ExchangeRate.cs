using System;
namespace tcs_sample1.Models
{
    public class ExchangeRate
    {
        public Currency Currency1 { get; set; }
        public decimal Coefficient1 { get; set; }

        public Currency Currency2 { get; set; }
        public decimal Coefficient2 { get; set; }
    }
}
