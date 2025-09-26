using System.ComponentModel.DataAnnotations;

namespace Demo.Application.CQRS.Cities
{
    public class CityDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        
        public long Population { get; set; }
        public int CountryId { get; set; }

        public string Country { get; set; }
    }
}
