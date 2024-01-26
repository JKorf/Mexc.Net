using CryptoExchange.Net.Objects;
using Mexc.Net.Enums;
using Mexc.Net.Objects.Models;
using Mexc.Net.Objects.Models.Spot;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mexc.Net.Interfaces.Clients.SpotApi
{
    public interface IMexcRestClientSpotApiTrading
    {
        /// <summary>
        /// Place a new test order. Only validates the rules, doesn't actually place any order
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#test-new-order" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="side">Order side</param>
        /// <param name="type">Order type</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="quoteQuantity">Quote quantity</param>
        /// <param name="price">Limit price</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancelation Token</param>
        /// <returns></returns>
        Task<WebCallResult> PlaceTestOrderAsync(string symbol, OrderSide side, OrderType type, decimal? quantity = null, decimal? quoteQuantity = null, decimal? price = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Place a new order
        /// <para><a href="https://mexcdevelop.github.io/apidocs/spot_v3_en/#new-order" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="side">Order side</param>
        /// <param name="type">Order type</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="quoteQuantity">Quote quantity</param>
        /// <param name="price">Limit price</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancelation Token</param>
        /// <returns></returns>
        Task<WebCallResult<MexcOrder>> PlaceOrderAsync(string symbol, OrderSide side, OrderType type, decimal? quantity = null, decimal? quoteQuantity = null, decimal? price = null, string? clientOrderId = null, CancellationToken ct = default);
    }
}
