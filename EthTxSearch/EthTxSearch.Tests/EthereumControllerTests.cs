using EthTxSearch.Controllers;
using EthTxSearch.EthClient;
using EthTxSearch.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EthTxSearch.Tests
{
    public class EthereumControllerTests
    {
        private const string BlockHash = "0x642cea18f1ff575a7664fcdbb47ed6d04ce9cdf74fa26622f8baff21ddcda903";
        private const string TxHash1 = "0xb5178424f997184202e95eb8282ef90b6c0350c62608c9f0ec9cc6b6d1c9b351";
        private const string TxHash2 = "0xb5178424f997184202e95eb8282ef90b6c0350c62608c9f0ec9cc6b6d1c9b352";
        private const string TxHash3 = "0xb5178424f997184202e95eb8282ef90b6c0350c62608c9f0ec9cc6b6d1c9b353";
        private const string Addr1 = "0x5b68c0b1f4cf680b408afd94f21abe0d59a255eb";
        private const string Addr2 = "0x60c1b4310398d642f1bbc738a7cb0e0e64da6d98";
        private const string Addr3 = "0x7f6fd88068b58c95b0a2181a8e93f26568a86c03";
        private const string BlockNumber = "0x7";
        private const string Gas = "0xf000";
        private const string Value = "0x0";

        [Fact]
        public async Task BadRequestForInvalidAddressFormat()
        {
            var jsonRpcMock = new Mock<IJsonRpcClient>();

            var controller = new EthereumController(jsonRpcMock.Object);

            var result = await controller.Search(0, "rahrah");

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DoesntFilterTransactionsForEmptyAddress()
        {
            var jsonRpcMock = new Mock<IJsonRpcClient>();

            var jsonRpcResponse = new JsonRpcBlockResponseResult
            {
                Hash = BlockHash,
                Number = BlockNumber,
                Transactions = new List<JsonRpcTransactionResponse>
                {
                    new JsonRpcTransactionResponse
                    {
                        BlockNumber = BlockNumber,
                        BlockHash = BlockHash,
                        Hash = TxHash1,
                        From = Addr1,
                        To = Addr2,
                        Value = Value,
                        Gas = Gas

                    },
                    new JsonRpcTransactionResponse
                    {
                        BlockNumber = BlockNumber,
                        BlockHash = BlockHash,
                        Hash = TxHash2,
                        From = Addr1,
                        To = Addr2,
                        Value = Value,
                        Gas = Gas
                    },
                    new JsonRpcTransactionResponse
                    {
                        BlockNumber = BlockNumber,
                        BlockHash = BlockHash,
                        Hash = TxHash3,
                        From = Addr1,
                        To = Addr2,
                        Value = Value,
                        Gas = Gas
                    }
                }
            };

            jsonRpcMock.Setup(x => x.GetBlockByHeightAsync(BlockNumber, true))
                .Returns(Task.FromResult(jsonRpcResponse));

            var controller = new EthereumController(jsonRpcMock.Object);

            var result = (OkObjectResult) await controller.Search(7, "");

            var returnedJson = (SearchResultViewModel) result.Value;
            
            Assert.Equal(jsonRpcResponse.Transactions.Count, returnedJson.Transactions.Count());
        }

        [Fact]
        public async Task FiltersTransactionsForAddress()
        {
            var jsonRpcMock = new Mock<IJsonRpcClient>();

            var jsonRpcResponse = new JsonRpcBlockResponseResult
            {
                Hash = BlockHash,
                Number = BlockNumber,
                Transactions = new List<JsonRpcTransactionResponse>
                {
                    new JsonRpcTransactionResponse
                    {
                        BlockNumber = BlockNumber,
                        BlockHash = BlockHash,
                        Hash = TxHash1,
                        From = Addr1,
                        To = Addr3,
                        Value = Value,
                        Gas = Gas

                    },
                    new JsonRpcTransactionResponse
                    {
                        BlockNumber = BlockNumber,
                        BlockHash = BlockHash,
                        Hash = TxHash2,
                        From = Addr1,
                        To = Addr2,
                        Value = Value,
                        Gas = Gas
                    },
                    new JsonRpcTransactionResponse
                    {
                        BlockNumber = BlockNumber,
                        BlockHash = BlockHash,
                        Hash = TxHash3,
                        From = Addr3,
                        To = Addr2,
                        Value = Value,
                        Gas = Gas
                    }
                }
            };

            jsonRpcMock.Setup(x => x.GetBlockByHeightAsync(BlockNumber, true))
                .Returns(Task.FromResult(jsonRpcResponse));

            var controller = new EthereumController(jsonRpcMock.Object);

            var result = (OkObjectResult)await controller.Search(7, Addr3);

            var returnedJson = (SearchResultViewModel)result.Value;

            Assert.Equal(2, returnedJson.Transactions.Count());
        }
    }
}
