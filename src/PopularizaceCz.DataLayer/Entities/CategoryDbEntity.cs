namespace PopularizaceCz.DataLayer.Entities
{
    public class CategoryDbEntity : IDbEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}