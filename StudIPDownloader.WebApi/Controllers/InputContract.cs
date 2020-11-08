using System;

namespace StudIPDownloader.WebApi.Controllers
{
    public class InputContract
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }
        public TimeSpan TimeSpan { get; set; }

        public override string ToString()
        {
            return $"{nameof(Username)}: {Username}, {nameof(Url)}: {Url}, {nameof(TimeSpan)}: {TimeSpan}";
        }
    }
}