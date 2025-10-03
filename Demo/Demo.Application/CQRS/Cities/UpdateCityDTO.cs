using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.CQRS.Cities
{
    public class UpdateCityDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public long Population { get; set; }

        public int CountryId { get; set; }
    }
}
