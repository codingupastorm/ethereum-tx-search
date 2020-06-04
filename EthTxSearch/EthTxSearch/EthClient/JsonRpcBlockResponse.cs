using System.Collections.Generic;

namespace EthTxSearch.EthClient
{
    /// <summary>
    /// Holds data returned from a JsonRpc interface.
    /// </summary>
    public class JsonRpcBlockResponse
    {
        public JsonRpcBlockResponseResult Result { get; set; }
    }

    /// <summary>
    /// Holds information about a block returned from a JsonRpc interface.
    /// </summary>
    public class JsonRpcBlockResponseResult
    {
        public string Hash { get; set; }

        public string Number { get; set; }
        public List<JsonRpcTransactionResponse> Transactions { get; set; }
    }

    /// <summary>
    /// Holds JsonRpc information about a transaction returned from some node or API.
    /// </summary>
    public class JsonRpcTransactionResponse
    {
        public string BlockHash { get; set; }

        public string BlockNumber { get; set; }

        public string Gas { get; set; }

        public string Hash { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string Value { get; set; }
    }
}
