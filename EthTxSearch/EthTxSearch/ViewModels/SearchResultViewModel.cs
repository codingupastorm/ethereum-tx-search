using EthTxSearch.EthClient;
using System.Collections.Generic;
using System.Linq;

namespace EthTxSearch.ViewModels
{
    public class SearchResultViewModel
    {
        public string BlockHash { get; set; }
        public uint BlockNumber { get; set; }

        public IEnumerable<TransactionViewModel> Transactions { get; set; }

        public static SearchResultViewModel FromBlockResponse(JsonRpcBlockResponseResult apiBlockResponse)
        {
            return new SearchResultViewModel
            {
                BlockHash = apiBlockResponse.Hash,
                BlockNumber = apiBlockResponse.Number.HexToUint(),
                Transactions = apiBlockResponse.Transactions.Select(x=> TransactionViewModel.FromTransactionResponse(x))
            };
        }
    }

    public class TransactionViewModel
    {
        public uint Gas { get; set; }
        public string TxHash { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Value { get; set; }

        public static TransactionViewModel FromTransactionResponse(JsonRpcTransactionResponse apiTransactionResponse)
        {
            return new TransactionViewModel
            {
                From = apiTransactionResponse.From,
                To = apiTransactionResponse.To,
                Gas = apiTransactionResponse.Gas.HexToUint(),
                TxHash = apiTransactionResponse.Hash,
                Value = apiTransactionResponse.Value.HexToEthValue()
            };
        }
    }
}
