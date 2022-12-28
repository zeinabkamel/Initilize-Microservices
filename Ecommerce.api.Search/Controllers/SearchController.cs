using Ecommerce.api.Search.Interfaces;
using Ecommerce.api.Search.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Ecommerce.api.Search.Controllers
{
    /// <summary>
    /// 
    /// </summary>

    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;
        private readonly ILogger<SearchController> _logger;
        public SearchController(ISearchService searchService, ILogger<SearchController> logger)
        {
            _searchService = searchService;
            _logger = logger;   
        }
        [HttpPost]
        public async Task<IActionResult> SearchAsync( SearchTerm searchTerm)
        {
           var SearchRes= await _searchService.SearchAsync(searchTerm.CustomerId);

           if(SearchRes.isSuccess)
            {
                return Ok(SearchRes.searchResult);
            }
            else
            {
                return NotFound();
            }
        }
          
    }
}
