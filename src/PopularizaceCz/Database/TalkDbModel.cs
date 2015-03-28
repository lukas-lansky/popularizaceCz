using PopularizaceCz.Database.Infrastructure;
using PopularizaceCz.Helpers;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PopularizaceCz.Database
{
    public class TalkDbModel : TalkDbEntity
    {
        public TalkDbModel(TalkDbEntity e, VenueDbEntity venue, IEnumerable<PersonDbEntity> speakers)
        {
            e.MapTo(this);

            this.Venue = venue;

            this.Speakers = speakers.ToList();
        }

        public IReadOnlyCollection<PersonDbEntity> Speakers { get; set; }

        public IReadOnlyCollection<OrganizationDbEntity> Organizers { get; set; }

        public IReadOnlyCollection<CategoryDbEntity> DirectCategories { get; set; }

        public VenueDbEntity Venue { get; set; }
    }
}