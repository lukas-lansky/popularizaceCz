using PopularizaceCz.Database.Infrastructure;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PopularizaceCz.Database
{
    public class PersonDbModel : PersonDbEntity
    {
        public PersonDbModel(PersonDbEntity e, IEnumerable<TalkDbEntity> talks, IEnumerable<OrganizationDbEntity> orgs)
        {
            this.Id = e.Id;
            this.Name = e.Name;
            // this.Talks = talks.ToList();
            // this.Organizations = orgs.ToList();
        }

        public IReadOnlyCollection<TalkDbEntity> Talks { get; set; }

        public IReadOnlyCollection<OrganizationDbEntity> Organizations { get; set; }
    }
}