using System.Threading.Tasks;

namespace Ecommerce.api.Search.Interfaces
{
    public interface ISearchService
    {
      Task<(bool isSuccess,dynamic searchResult)>  SearchAsync(int customerId);
    }
}
