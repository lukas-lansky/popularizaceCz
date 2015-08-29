using System;

namespace PopularizaceCz.DataLayer.Entities
{
    public class TalkRecordingDbEntity : IDbEntity
    {
        public int Id {
            get { return this.TalkId; }
            set { this.TalkId = value; }
        }

        public int TalkId { get; set; }

        public string Url { get; set; }

        public string YouTubeVideoId { get; set; }
    }
}