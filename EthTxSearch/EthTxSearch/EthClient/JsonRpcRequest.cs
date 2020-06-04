namespace EthTxSearch.EthClient
{
    /// <summary>
    /// Used as the body of the post for Infura requests.
    /// </summary>
    public class JsonRpcRequest
    {
        public string Jsonrpc { get; set; }
        public string Method { get; set; }
        public object[] Params { get; set; }
        public uint Id { get; set; }
    }
}
