namespace PopularizaceCz.Services.YouTube
{
    public interface IYouTubeLinker
    {
        string GetVideoLink(string videoId);

        string GetImageLink(string videoId);
    }
}
