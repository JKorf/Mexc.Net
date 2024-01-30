//using CryptoExchange.Net.Interfaces;
//using Mexc.Net.Interfaces.Clients;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
//using System;

//namespace Mexc.Net.SymbolOrderBooks
//{
//    /// <summary>
//    /// Mexc order book factory
//    /// </summary>
//    public class MexcOrderBookFactory : IMexcOrderBookFactory
//    {
//        private readonly IServiceProvider _serviceProvider;

//        /// <summary>
//        /// ctor
//        /// </summary>
//        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
//        public MexcOrderBookFactory(IServiceProvider serviceProvider)
//        {
//            _serviceProvider = serviceProvider;
//        }

//        /// <inheritdoc />
//        public ISymbolOrderBook CreateSpot(string symbol, Action<BinanceOrderBookOptions>? options = null)
//            => new MexcSpotSymbolOrderBook(symbol,
//                                             options,
//                                             _serviceProvider.GetRequiredService<ILogger<MexcSpotSymbolOrderBook>>(),
//                                             _serviceProvider.GetRequiredService<IMexcRestClient>(),
//                                             _serviceProvider.GetRequiredService<IMexcSocketClient>());
//    }
//}
