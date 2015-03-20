using PopularizaceCz.Database.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PopularizaceCz.Database
{
    public class TalkDbModel : TalkDbEntity
    {
        public IReadOnlyCollection<OrganizationDbEntity> Organizers { get; set; }

        public IReadOnlyCollection<CategoryDbEntity> DirectCategories { get; set; }
    }
}