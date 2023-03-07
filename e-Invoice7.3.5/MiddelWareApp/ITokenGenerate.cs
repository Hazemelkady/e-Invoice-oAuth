using System.Threading.Tasks;
namespace MiddelWareApp
{
    interface ITokenGenerate<T> where T : class
    {
        Task<TResult> GenerateToken<TResult>(string _TokenBaseAddres);
    }
}
