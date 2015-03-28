using PopularizaceCz.Database.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PopularizaceCz.Database
{
    public class VenueDbEntity : IDbEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}