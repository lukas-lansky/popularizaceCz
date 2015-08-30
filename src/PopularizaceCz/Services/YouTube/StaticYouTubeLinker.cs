namespace PopularizaceCz.Services.YouTube
{
    public sealed class StaticYouTubeLinker : IYouTubeLinker
    {
        public string GetVideoLink(string videoId)
        {
            return $"https://www.youtube.com/watch?v={videoId}";
        }

        public string GetImageLink(string videoId)
        {
            return $"http://img.youtube.com/vi/{videoId}/hqdefault.jpg";
        }
    }
}
