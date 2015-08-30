using PopularizaceCz.DataLayer.Entities;
using PopularizaceCz.DataLayer.Models;
using PopularizaceCz.Services.YouTube;
using System.Linq;

namespace PopularizaceCz.ViewModels
{
    public sealed class TalkViewModel
    {
        public UserDbEntity CurrentUser { get; set; }

        public TalkDbModel DbModel { get; set; }
        
        public string YouTubeVideoUrl { get; set; }

        public string YouTubeImageUrl { get; set; }
    }
}
