using System.Threading.Tasks;

namespace StudIPDownloader.WebApi.Controllers
{
    public interface ILoginActivityService
    {
        Task<string> Login(string username, string password, string url);
    }
}