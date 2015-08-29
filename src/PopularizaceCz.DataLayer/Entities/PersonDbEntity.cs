namespace PopularizaceCz.DataLayer.Entities
{
    public class PersonDbEntity : IDbEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}