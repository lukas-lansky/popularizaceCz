using System.Linq;
using System.Collections.Generic;
using PopularizaceCz.DataLayer.Entities;
using PopularizaceCz.Helpers;

namespace PopularizaceCz.DataLayer.Models
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