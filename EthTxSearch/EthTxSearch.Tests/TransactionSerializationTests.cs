﻿using EthTxSearch.EthClient;
using Newtonsoft.Json;
using Xunit;

namespace EthTxSearch.Tests
{
    public class TransactionSerializationTests
    {
        private const string SampleTransactionJson =
            @"{
                ""blockHash"": ""0xb3b20624f8f0f86eb50dd04688409e5cea4bd02d700bf6e79e9384d47d6a5a35"",
                ""blockNumber"": ""0x5bad55"",
                ""from"": ""0xfbb1b73c4f0bda4f67dca266ce6ef42f520fbb98"",
                ""gas"": ""0x249f0"",
                ""gasPrice"": ""0x174876e800"",
                ""hash"": ""0x8784d99762bccd03b2086eabccee0d77f14d05463281e121a62abfebcf0d2d5f"",
                ""input"": ""0x6ea056a9000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000bd8d7fa6f8cc00"",
                ""nonce"": ""0x5e4724"",
                ""r"": ""0xd1556332df97e3bd911068651cfad6f975a30381f4ff3a55df7ab3512c78b9ec"",
                ""s"": ""0x66b51cbb10cd1b2a09aaff137d9f6d4255bf73cb7702b666ebd5af502ffa4410"",
                ""to"": ""0x4b9c25ca0224aef6a7522cabdbc3b2e125b7ca50"",
                ""transactionIndex"": ""0x0"",
                ""v"": ""0x25"",
                ""value"": ""0x0""
            }";

        private const string SampleTransactionBlockHash = "0xb3b20624f8f0f86eb50dd04688409e5cea4bd02d700bf6e79e9384d47d6a5a35";
        private const string SampleTransactionBlockNumber = "0x5bad55";
        private const string SampleTransaction1Value = "0x0";

        [Fact]
        public void CanSerializeSampleTransaction()
        {
            JsonRpcTransactionResponse response = JsonConvert.DeserializeObject<JsonRpcTransactionResponse>(SampleTransactionJson);
            Assert.Equal(SampleTransactionBlockHash, response.BlockHash);
            Assert.Equal(SampleTransactionBlockNumber, response.BlockNumber);
            Assert.Equal(SampleTransaction1Value, response.Value);
        }
    }
}
