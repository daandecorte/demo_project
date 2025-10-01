using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.CQRS.Cities
{
    public class CreateCityDTO
    {   

        public string Name { get; set; } = string.Empty;


        public long Population { get; set; }

        public string Country { get; set; }
    }
}
