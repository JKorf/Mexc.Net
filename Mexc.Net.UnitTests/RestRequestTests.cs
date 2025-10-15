using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using Mexc.Net.Clients;
using Mexc.Net.Enums;

namespace Mexc.Net.UnitTests
{
    [TestFixture]
    public class RestRequestTests
    {
        [Test]
        public async Task ValidateAccountCalls()
        {
            var client = new MexcRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new ApiCredentials("123", "456");
                opts.OutputOriginalData = true;
            });
            var tester = new RestRequestValidator<MexcRestClient>(client, "Endpoints/SpotApi/Account", "https://api.mexc.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.SpotApi.Account.GetAccountInfoAsync(), "GetAccountInfo");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetKycStatusAsync(), "GetKycStatus");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetUserAssetsAsync(), "GetUserAssets");
            await tester.ValidateAsync(client => client.SpotApi.Account.WithdrawAsync("ETH", "123", 1), "Withdraw");
            await tester.ValidateAsync(client => client.SpotApi.Account.CancelWithdrawAsync("123"), "CancelWithdraw");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetDepositHistoryAsync("123"), "GetDepositHistory");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetWithdrawHistoryAsync("123"), "GetWithdrawHistory");
            await tester.ValidateAsync(client => client.SpotApi.Account.GenerateDepositAddressAsync("123", "123"), "GenerateDepositAddress");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetDepositAddressesAsync("123", "123"), "GetDepositAddresses");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetWithdrawAddressesAsync("123"), "GetWithdrawAddresses");
            await tester.ValidateAsync(client => client.SpotApi.Account.TransferAsync("123", Enums.AccountType.Futures, Enums.AccountType.Spot, 1), "Transfer");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetTransferHistoryAsync(Enums.AccountType.Futures, Enums.AccountType.Spot), "GetTransferHistory");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetTransferAsync("123"), "GetTransfer", ignoreProperties: ["symbol"]);
            await tester.ValidateAsync(client => client.SpotApi.Account.GetAssetsForDustTransferAsync(), "GetAssetsForDustTransfer");
            await tester.ValidateAsync(client => client.SpotApi.Account.DustTransferAsync(["BTC"]), "DustTransfer");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetDustLogAsync(), "GetDustLog");
            await tester.ValidateAsync(client => client.SpotApi.Account.TransferInternalAsync("ETH", 1, Enums.TransferAccountType.Email, "123"), "TransferInternal");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetInternalTransferHistoryAsync("ETH"), "GetInternalTransferHistory");
            await tester.ValidateAsync(client => client.SpotApi.Account.SetMxDeductionAsync(true), "SetMxDeduction", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetMxDeductionStatusAsync(), "GetMxDeductionStatus", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.Account.StartUserStreamAsync(), "StartUserStream", nestedJsonProperty: "listenKey");
            await tester.ValidateAsync(client => client.SpotApi.Account.KeepAliveUserStreamAsync("123"), "KeepAliveUserStream");
            await tester.ValidateAsync(client => client.SpotApi.Account.StopUserStreamAsync("123"), "StopUserStream");
        }

        [Test]
        public async Task ValidateExchangeDataCalls()
        {
            var client = new MexcRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new ApiCredentials("123", "456");
                opts.OutputOriginalData = true;
            });
            var tester = new RestRequestValidator<MexcRestClient>(client, "Endpoints/SpotApi/ExchangeData", "https://api.mexc.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetServerTimeAsync(), "GetServerTime", skipResponseValidation: true);
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetApiSymbolsAsync(), "GetApiSymbols", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetExchangeInfoAsync(), "GetExchangeInfo", ignoreProperties: ["rateLimits", "exchangeFilters", "quoteAssetPrecision", "orderTypes", "filters"]);
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetOrderBookAsync("ETHUSDT"), "GetOrderBook", skipResponseValidation: true);
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetRecentTradesAsync("ETHUSDT"), "GetRecentTrades", ignoreProperties: ["id", "tradeType"]);
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetAggregatedTradeHistoryAsync("ETHUSDT"), "GetAggregatedTradeHistory", ignoreProperties: ["a", "f", "l"]);
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetKlinesAsync("ETHUSDT", Enums.KlineInterval.OneDay), "GetKlines");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetAveragePriceAsync("ETHUSDT"), "GetAveragePrice");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetTickerAsync("ETHUSDT"), "GetTicker", ignoreProperties: ["count"]);
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetTickersAsync(), "GetTickers", ignoreProperties: ["count"]);
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetPricesAsync(), "GetPrices");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetBookPricesAsync(), "GetBookPrices");
        }

        [Test]
        public async Task ValidateTradingCalls()
        {
            var client = new MexcRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new ApiCredentials("123", "456");
                opts.OutputOriginalData = true;
            });
            var tester = new RestRequestValidator<MexcRestClient>(client, "Endpoints/SpotApi/Trading", "https://api.mexc.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.SpotApi.Trading.PlaceTestOrderAsync("ETHUSDT", Enums.OrderSide.Buy, Enums.OrderType.Market), "PlaceTestOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.PlaceOrderAsync("ETHUSDT", Enums.OrderSide.Buy, Enums.OrderType.Market), "PlaceOrder", ignoreProperties: ["orderListId"]);
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelOrderAsync("ETHUSDT"), "CancelOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelAllOrdersAsync("ETHUSDT"), "CancelAllOrders", ignoreProperties: ["orderListId"]);
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOrderAsync("ETHUSDT"), "GetOrder", ignoreProperties: ["orderListId"]);
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOpenOrdersAsync("ETHUSDT"), "GetOpenOrders", ignoreProperties: ["orderListId"]);
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOrdersAsync("ETHUSDT"), "GetOrders", ignoreProperties: ["orderListId"]);
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetUserTradesAsync("ETHUSDT"), "GetUserTrades", ignoreProperties: ["orderListId"]);
        }


        [Test]
        public async Task ValidateFuturesAccountCalls()
        {
            var client = new MexcRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new ApiCredentials("123", "456");
                opts.OutputOriginalData = true;
            });
            var tester = new RestRequestValidator<MexcRestClient>(client, "Endpoints/FuturesApi/Account", "https://contract.mexc.com", IsAuthenticatedFutures);
            await tester.ValidateAsync(client => client.FuturesApi.Account.GetBalancesAsync(), "GetBalances", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.Account.GetTransferHistoryAsync(), "GetTransferHistory", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.Account.GetFundingHistoryAsync(), "GetFundingHistory", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.Account.GetTradingFeesAsync("123"), "GetTradingFees", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.Account.ChangeMarginAsync(123, 0.1m, ChangeType.Increase), "ChangeMargin");
            await tester.ValidateAsync(client => client.FuturesApi.Account.GetLeverageAsync("123"), "GetLeverage", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.Account.SetLeverageAsync(123), "SetLeverage");
            await tester.ValidateAsync(client => client.FuturesApi.Account.GetPositionModeAsync(), "GetPositionMode", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.Account.SetPositionModeAsync(PositionMode.OneWay), "SetPositionMode");
        }

        [Test]
        public async Task ValidateFuturesExchangeDataCalls()
        {
            var client = new MexcRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new ApiCredentials("123", "456");
                opts.OutputOriginalData = true;
            });
            var tester = new RestRequestValidator<MexcRestClient>(client, "Endpoints/FuturesApi/ExchangeData", "https://contract.mexc.com", IsAuthenticatedFutures); 
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetIndexPriceAsync("ETH_USDT"), "GetIndexPrice", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetMarkPriceAsync("ETH_USDT"), "GetMarkPrice", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetFundingRateAsync("ETH_USDT"), "GetFundingRate", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetRecentTradesAsync("ETH_USDT", 123), "GetRecentTrades", nestedJsonProperty: "data", ignoreProperties: ["O"]);
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetTickersAsync(), "GetTickers", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetRiskFundBalancesAsync(), "GetRiskFundBalances", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetRiskFundBalanceHistoryAsync("ETH_USDT"), "GetRiskFundBalanceHistory", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetFundingRateHistoryAsync("ETH_USDT"), "GetFundingRateHistory", nestedJsonProperty: "data");
        }

        [Test]
        public async Task ValidateFuturesTradingCalls()
        {
            var client = new MexcRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new ApiCredentials("123", "456");
                opts.OutputOriginalData = true;
            });
            var tester = new RestRequestValidator<MexcRestClient>(client, "Endpoints/FuturesApi/Trading", "https://contract.mexc.com", IsAuthenticatedFutures);
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetOpenOrdersAsync("ETH_USDT"), "GetOpenOrders", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetOrderHistoryAsync(), "GetOrderHistory", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetOrderByClientOrderIdAsync("ETH_USDT", "123"), "GetOrderByClientOrderId", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetOrderAsync("123"), "GetOrder", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetOrdersByIdAsync(["123"]), "GetOrdersById", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetOrderTradesAsync("123"), "GetOrderTrades", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetStopOrdersAsync(), "GetStopOrders", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetRiskLimitsAsync(), "GetRiskLimits", skipResponseValidation: true);
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetPositionHistoryAsync(), "GetPositionHistory", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetPositionsAsync(), "GetPositions", nestedJsonProperty: "data");
        }

        private bool IsAuthenticatedFutures(WebCallResult result)
        {
            return result.RequestHeaders?.Any(x => x.Key == "Signature") == true;
        }

        private bool IsAuthenticated(WebCallResult result)
        {
            return result.RequestBody?.Contains("signature") == true || result.RequestUrl.Contains("signature");
        }
    }
}
