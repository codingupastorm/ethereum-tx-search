using EthTxSearch.EthClient;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EthTxSearch.Tests
{
    public class InfuraClientTests
    {
        private readonly InfuraClient client;

        public InfuraClientTests()
        {
            this.client = new InfuraClient();
        }

        [Fact]
        public async Task GetBlockByHashIsCorrectAsync()
        {
            const string knownBlockHeightHex = "0x8B99C9";
            const string knownBlockHash = "0x6dbde4b224013c46537231c548bd6ff8e2a2c927c435993d351866d505c523f1";
            const int knownBlockTxCount = 232;

            JsonRpcBlockResponseResult response = await this.client.GetBlockByHeightAsync(knownBlockHeightHex);

            Assert.Equal(knownBlockHash, response.Hash);
            Assert.Equal(knownBlockTxCount, response.Transactions.Count);
        }

        [Fact(Skip = "Don't need to run every time.")]
        public async Task GetRandomBlocksSuccessFully()
        {
            // Not deterministic but just checks that generally, we can get blocks without errors.
            var random = new Random();

            for (int i = 0; i < 100; i++)
            {
                uint blockNum = (uint) random.Next(100, 9_000_000);
                string blockNumHex = blockNum.UIntToHexString();
                JsonRpcBlockResponseResult response = await this.client.GetBlockByHeightAsync(blockNumHex);
                Assert.NotNull(response);
                Thread.Sleep(1_000); // Haven't checked if there is an API limit.
            }
        }
    }
}
