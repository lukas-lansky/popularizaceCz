using System;

namespace PopularizaceCz.DataLayer.Entities
{
    public class TalkRecordingDbEntity : IDbEntity
    {
        public int Id {
            get { return this.TalkRecordingId; }
            set { this.TalkRecordingId = value; }
        }

        public int TalkRecordingId { get; set; }

        public int TalkId { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public string YouTubeVideoId { get; set; }
    }
}