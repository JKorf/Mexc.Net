using CryptoExchange.Net.Objects.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mexc.Net
{
    internal static class MexcErrors
    {
        public static ErrorMapping SpotErrors { get; } = new ErrorMapping([
            new ErrorInfo(ErrorType.Unauthorized, false, "Unauthorized", "401"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Access denied", "403"),
            new ErrorInfo(ErrorType.Unauthorized, false, "API key invalid", "10072", "700001"),
            new ErrorInfo(ErrorType.Unauthorized, false, "No permissions for symbol", "30020"),
            new ErrorInfo(ErrorType.Unauthorized, false, "IP not allowed", "700006"),
            new ErrorInfo(ErrorType.Unauthorized, false, "No permissions for endpoint", "700007"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Signature verification failed", "602", "700002"),

            new ErrorInfo(ErrorType.InvalidTimestamp, false, "Invalid timestamp", "10073", "700003"),

            new ErrorInfo(ErrorType.RateLimitRequest, false, "Too many requests", "429"),

            new ErrorInfo(ErrorType.SystemError, true, "Internal system error", "500"),
            new ErrorInfo(ErrorType.SystemError, true, "Service unavailable", "503"),

            new ErrorInfo(ErrorType.Timeout, false, "Gateway timeout", "504"),

            new ErrorInfo(ErrorType.InvalidParameter, false, "Parameter error", "33333", "700008", "730002", "700004"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Receive window must be less than 60 seconds", "700005"),

            new ErrorInfo(ErrorType.MissingParameter, false, "Asset can't be null", "10222"),
            new ErrorInfo(ErrorType.MissingParameter, false, "Parameter value not set", "44444"),

            new ErrorInfo(ErrorType.InvalidQuantity, false, "Quantity can't be null", "10095"),
            new ErrorInfo(ErrorType.InvalidQuantity, false, "Quantity decimal precision invalid", "10096"),
            new ErrorInfo(ErrorType.InvalidQuantity, false, "Quantity invalid", "10097", "10102"),
            new ErrorInfo(ErrorType.InvalidQuantity, false, "Quantity too low", "30002"),
            new ErrorInfo(ErrorType.InvalidQuantity, false, "Quantity too high", "30003", "30029"),

            new ErrorInfo(ErrorType.InvalidPrice, false, "Price invalid", "30010"),

            new ErrorInfo(ErrorType.InsufficientBalance, false, "Insufficient balance", "10101", "30004"),

            new ErrorInfo(ErrorType.UnknownOrder, false, "Unknown order", "-2011", "30026"),

            new ErrorInfo(ErrorType.UnknownSymbol, false, "Unknown symbol","30021", "730001", "-1121"),

            new ErrorInfo(ErrorType.UnknownAsset, false, "Unknown asset", "10232"),

            new ErrorInfo(ErrorType.UnavailableSymbol, false, "Trading currently disabled", "30016"),

            new ErrorInfo(ErrorType.RejectedOrderConfiguration, false, "API trading not supported", "10007"),
            new ErrorInfo(ErrorType.RejectedOrderConfiguration, false, "Market orders not currently allowed on symbol", "30018"),
            new ErrorInfo(ErrorType.RejectedOrderConfiguration, false, "Market orders not currently allowed on symbol from API", "30019"),
            new ErrorInfo(ErrorType.RejectedOrderConfiguration, false, "Order type not currently allowed", "30041"),

            ]);

        public static ErrorMapping FuturesErrors { get; } = new ErrorMapping([
            new ErrorInfo(ErrorType.Unauthorized, true, "Unauthorized", "401"),
            new ErrorInfo(ErrorType.Unauthorized, true, "API key expired", "402"),
            new ErrorInfo(ErrorType.Unauthorized, true, "IP address not allowed", "406"),
            new ErrorInfo(ErrorType.Unauthorized, true, "Insufficient permissions", "701", "702", "703", "704"),
            new ErrorInfo(ErrorType.Unauthorized, true, "Trading forbidden", "6001"),

            new ErrorInfo(ErrorType.SystemError, true, "System error", "500"),
            new ErrorInfo(ErrorType.SystemError, true, "System busy", "501"),

            new ErrorInfo(ErrorType.RateLimitRequest, true, "Too many requests", "510", "2037"),

            new ErrorInfo(ErrorType.InvalidParameter, true, "Parameter error", "600"),
            new ErrorInfo(ErrorType.InvalidParameter, true, "Invalid order direction", "2001"),
            new ErrorInfo(ErrorType.InvalidParameter, true, "Invalid open type", "2002"),
            new ErrorInfo(ErrorType.InvalidParameter, true, "Leverage ratio error", "2006"),
            new ErrorInfo(ErrorType.InvalidParameter, true, "Too many orders in batch operation", "2013", "2014", "2034"),
            new ErrorInfo(ErrorType.InvalidParameter, true, "Price or quantity decimal precision invalid", "2015"),
            new ErrorInfo(ErrorType.InvalidParameter, true, "Incorrect position type", "2022"),
            new ErrorInfo(ErrorType.InvalidParameter, true, "Order type invalid", "2029"),
            new ErrorInfo(ErrorType.InvalidParameter, true, "Invalid clientOrderId", "2030"),
            new ErrorInfo(ErrorType.InvalidParameter, true, "Trigger price type error", "3001"),
            new ErrorInfo(ErrorType.InvalidParameter, true, "Trigger type error", "3002"),
            new ErrorInfo(ErrorType.InvalidParameter, true, "Time range invalid", "6003"),

            new ErrorInfo(ErrorType.InvalidStopParameters, true, "Trigger price error", "3004"),
            new ErrorInfo(ErrorType.InvalidStopParameters, true, "Take profit and stop loss price can't both be empty", "5001"),
            new ErrorInfo(ErrorType.InvalidStopParameters, true, "Take profit and stop loss price invalid", "5003"),
            new ErrorInfo(ErrorType.InvalidStopParameters, true, "Take profit and stop loss quantity invalid", "5004"),

            new ErrorInfo(ErrorType.UnknownOrder, true, "Stop limit order not found", "5002"),

            new ErrorInfo(ErrorType.UnknownSymbol, true, "Symbol does not exist", "1001"),

            new ErrorInfo(ErrorType.UnknownAsset, true, "Unsupported asset", "4001"),

            new ErrorInfo(ErrorType.UnavailableSymbol, true, "Symbol not currently trading", "1002"),
            new ErrorInfo(ErrorType.UnavailableSymbol, true, "Symbol not available", "6005"),

            new ErrorInfo(ErrorType.InvalidQuantity, true, "Quantity invalid", "1004", "2011"),
            new ErrorInfo(ErrorType.InvalidQuantity, true, "Order quantity too low", "2008"),
            new ErrorInfo(ErrorType.InvalidQuantity, true, "Order quantity too high", "2028"),

            new ErrorInfo(ErrorType.InvalidPrice, true, "Price too low", "2004"),
            new ErrorInfo(ErrorType.InvalidPrice, true, "Price error", "2007"),
            new ErrorInfo(ErrorType.InvalidPrice, true, "Price lower than liquidation price", "2032"),
            new ErrorInfo(ErrorType.InvalidPrice, true, "Price higher than liquidation price", "2033"),

            new ErrorInfo(ErrorType.NoPosition, true, "No open position", "2009"),

            new ErrorInfo(ErrorType.MaxPosition, true, "Position limit reached", "2025", "2031", "2038"),

            new ErrorInfo(ErrorType.InsufficientBalance, true, "Insufficient balance", "2005"),
            new ErrorInfo(ErrorType.InsufficientBalance, true, "Maximum available margin exceeded", "2018"),

            new ErrorInfo(ErrorType.RateLimitOrder, true, "Too many open orders", "2036"),

            ]);
    }
}
