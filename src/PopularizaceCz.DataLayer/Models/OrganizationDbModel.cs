using PopularizaceCz.Helpers;
using PopularizaceCz.DataLayer.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PopularizaceCz.DataLayer.Models
{
    public class OrganizationDbModel : OrganizationDbEntity
    {
        public OrganizationDbModel(OrganizationDbEntity e, IEnumerable<TalkDbEntity> talks)
        {
            e.MapTo(this);

            this.Talks = talks.ToList();
        }

        public IList<TalkDbEntity> Talks { get; set; }
    }
}