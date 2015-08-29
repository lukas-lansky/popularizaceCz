using System.Linq;
using System.Collections.Generic;
using PopularizaceCz.DataLayer.Entities;
using PopularizaceCz.Helpers;

namespace PopularizaceCz.DataLayer.Models
{
    public class TalkDbModel : TalkDbEntity
    {
        public TalkDbModel()
        {

        }

        public TalkDbModel(TalkDbEntity e, VenueDbEntity venue, IEnumerable<PersonDbEntity> speakers)
        {
            e.MapTo(this);

            this.Venue = venue;

            this.Speakers = speakers.ToList();
        }

        public IList<PersonDbEntity> Speakers { get; set; }

        public IList<OrganizationDbEntity> Organizers { get; set; }

        public IList<CategoryDbEntity> DirectCategories { get; set; }

        public IList<TalkRecordingDbEntity> Recordings { get; set; }

        public VenueDbEntity Venue { get; set; }
    }
}