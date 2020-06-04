using Flurl;
using Flurl.Http;
using System.Threading.Tasks;

namespace EthTxSearch.EthClient
{
    public class InfuraClient : IJsonRpcClient
    {
        // We can easily make these configurable if we want to use a testnet, or a different version of API etc.
        private const string Endpoint = "https://mainnet.infura.io";
        private const string Version = "v3";
        private const string ProjectId = "22b2ebe2940745b3835907b30e8257a4";

        private const string JsonRpcValue = "2.0";
        private const string GetBlockByHeightMethod = "eth_getBlockByNumber";
        private const uint Id = 1;

        /// <inheritdoc />
        public async Task<JsonRpcBlockResponseResult> GetBlockByHeightAsync(string blockHeight, bool showDetails = true)
        {
            var infuraBlockResponse = await Endpoint
                .AppendPathSegment(Version)
                .AppendPathSegment(ProjectId)
                .PostJsonAsync(new JsonRpcRequest
                {
                    Jsonrpc = JsonRpcValue,
                    Method = GetBlockByHeightMethod,
                    Params = new object[]
                    {
                        blockHeight,
                        showDetails
                    },
                    Id = Id
                }).ReceiveJson<JsonRpcBlockResponse>();

            return infuraBlockResponse.Result;
        }
    }
}
