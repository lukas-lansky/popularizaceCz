using PopularizaceCz.Database;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PopularizaceCz.ViewModels
{
    public sealed class HomepageViewModel
    {
        public IEnumerable<TalkDbEntity> UpcomingTalks { get; set; }
    }
}
