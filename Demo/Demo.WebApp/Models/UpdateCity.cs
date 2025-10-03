using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApp.Models
{
    public class UpdateCity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "De naam mag niet leeg zijn.")]
        public string Name { get; set; } = string.Empty;

        [Range(0, 10000000000, ErrorMessage = "Het aantal inwoners mag niet groter zijn dan 10.000.000.000.")]
        public long Population { get; set; }

        [Required(ErrorMessage = "Er moet een land gekozen worden.")]
        [Range(1, int.MaxValue, ErrorMessage = "Er moet een land gekozen worden.")]
        public int CountryId { get; set; }
    }
}
