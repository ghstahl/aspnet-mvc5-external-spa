using System.Threading.Tasks;

namespace VirtCo.Providers.Stores
{
    public interface IRemoteRazorLocationStore : IRazorLocationStore
    {
        Task LoadRemoteDataAsync(string url);
    }
}