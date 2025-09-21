using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Exceptions
{
    public class CityNotFoundException : Exception
    {
        public CityNotFoundException() : base()
        { }

        public CityNotFoundException(string message) : base(message)
        { }
    }
}
