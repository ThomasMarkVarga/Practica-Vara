namespace WebAPI_DB.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public required string  CIF { get; set; }
        public required string Name { get; set; }
        public string Address {  get; set; } = string.Empty;
        public string County { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
