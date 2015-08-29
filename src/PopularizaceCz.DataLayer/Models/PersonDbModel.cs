using System.Linq;
using System.Collections.Generic;
using PopularizaceCz.DataLayer.Entities;
using PopularizaceCz.Helpers;

namespace PopularizaceCz.DataLayer.Models
{
    public class PersonDbModel : PersonDbEntity
    {
        public PersonDbModel(PersonDbEntity e, IEnumerable<TalkDbEntity> talks, IEnumerable<OrganizationDbEntity> orgs)
        {
            e.MapTo(this);

            this.Talks = talks.ToList();
            this.Organizations = orgs.ToList();
        }

        public IReadOnlyCollection<TalkDbEntity> Talks { get; set; }

        public IReadOnlyCollection<OrganizationDbEntity> Organizations { get; set; }
    }
}