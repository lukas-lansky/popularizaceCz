using PopularizaceCz.DataLayer.Entities;
using PopularizaceCz.DataLayer.Models;
using System.Collections.Generic;

namespace PopularizaceCz.ViewModels
{
    public sealed class HomepageViewModel
    {
        public IEnumerable<TalkDbEntity> UpcomingTalks { get; set; }

        public IEnumerable<PersonDbModel> FrequentSpeakers { get; set; }

        public IEnumerable<OrganizationDbModel> FrequentOrganizers { get; set; }
    }
}
