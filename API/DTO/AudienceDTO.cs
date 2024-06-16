using API.Models;

namespace API.DTO
{
    public class AudienceDTO
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public AudienceType? AudienceType { get; set; }
    }
}
