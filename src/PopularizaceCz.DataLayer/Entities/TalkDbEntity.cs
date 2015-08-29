using System;

namespace PopularizaceCz.DataLayer.Entities
{
    public class TalkDbEntity : IDbEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public DateTime Start { get; set; }

        public int VenueId { get; set; }
    }
}