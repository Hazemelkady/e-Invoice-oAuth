using System.Threading.Tasks;

namespace MiddelWareApp
{
    public interface IHttpClientOperation<T> where T : class
    {
        Task<T> GetAsyncResult(string url);
        Task<TResult> PostAsyncRequwst<TResult>(string url, T ObjectRequestToPost);
    }



}
