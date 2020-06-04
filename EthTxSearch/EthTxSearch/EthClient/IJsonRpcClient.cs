using System.Threading.Tasks;

namespace EthTxSearch.EthClient
{
    public interface IJsonRpcClient
    {
        /// <summary>
        /// Get the details of a block.
        /// </summary>
        /// <param name="blockHeight">The number of the block to query for in hex format.</param>
        /// <param name="showDetails">Whether to get the details for each transaction in the block. Defaults to true.</param>
        /// <returns>A blocks details.</returns>
        Task<JsonRpcBlockResponseResult> GetBlockByHeightAsync(string blockHeight, bool showDetails = true);
    }
}
