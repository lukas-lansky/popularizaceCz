using System;

namespace PopularizaceCz.DataLayer.Entities
{
    public class UserDbEntity : IDbEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsAdmin { get; set; }
    }
}