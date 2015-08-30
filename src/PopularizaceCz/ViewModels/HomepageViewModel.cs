using PopularizaceCz.DataLayer.Entities;
using PopularizaceCz.DataLayer.Models;
using System.Collections.Generic;

namespace PopularizaceCz.ViewModels
{
    public sealed class HomepageViewModel
    {
        public IEnumerable<TalkDbEntity> UpcomingTalks { get; set; }

        public IDictionary<PersonDbEntity, int> FrequentSpeakers { get; set; }

        public IDictionary<CategoryDbEntity, int> FrequentCategories { get; set; }

        public IDictionary<OrganizationDbEntity, int> FrequentOrganizers { get; set; }
    }
}
