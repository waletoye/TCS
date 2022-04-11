using System;
using System.ComponentModel.DataAnnotations;

namespace tcs_sample1.Models
{
    public class Voyage
    {
        [Required]
        public string VoyageCode { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public Currency Currency { get; set; }

        [Required]
        public DateTimeOffset Timestamp { get; set; }
    }
}
