namespace API.Models
{
    public class Cabinet
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int CabinetTypeId { get; set; }
        public CabinetType CabinetType { get;set; }
    }
}
