using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.CQRS.Cities
{
    public class CreateCityDTO
    {
        [Required(ErrorMessage = "the name can't be empty.")]
        public string Name { get; set; } = string.Empty;
        [Range(0,10000000000, ErrorMessage = "the population can't be more than 10.000.000.000 or be negative.")]
        public long Population { get; set; }
        [Range(1, int.MaxValue, ErrorMessage ="You have to chose a country.")]
        public long CountryId { get; set; }
    }
}
