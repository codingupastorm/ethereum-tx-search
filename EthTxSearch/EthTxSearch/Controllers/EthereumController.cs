using EthTxSearch.EthClient;
using EthTxSearch.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EthTxSearch.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EthereumController : ControllerBase
    {
        private readonly IJsonRpcClient jsonRpcClient;

        public EthereumController(IJsonRpcClient jsonRpcClient)
        {
            this.jsonRpcClient = jsonRpcClient;
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> Search(uint blockHeight, string address)
        {
            // This could be stripped out to somewhere else but for such a small application I think it's fine here.

            if (!string.IsNullOrWhiteSpace(address) && !address.IsValidHex())
                return BadRequest("Address was in an invalid format.");

            string hexBlockHeight = blockHeight.UIntToHexString();

            JsonRpcBlockResponseResult blockResponse = await this.jsonRpcClient.GetBlockByHeightAsync(hexBlockHeight);
            SearchResultViewModel model = SearchResultViewModel.FromBlockResponse(blockResponse);

            // If address wasn't filled in, assume user wants the whole block.
            if (string.IsNullOrWhiteSpace(address))
                return Ok(model);

            // Assume that the jsonrpc results are lower case.
            model.Transactions = model.Transactions.Where(x => x.From == address || x.To == address);

            return Ok(model);
        }
    }
}
