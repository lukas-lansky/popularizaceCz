using PopularizaceCz.Database.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PopularizaceCz.Database
{
    public class TalkDbEntity : IDbEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Start { get; set; }

        public int VenueId { get; set; }
    }
}