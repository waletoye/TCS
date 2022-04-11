using System;
using System.Collections.Generic;

namespace tcs_sample1.Models
{
    public class DataStore
    {
        internal static Dictionary<string, Stack<Voyage>> Voyages { get; set; }
        internal static List<ExchangeRate> ExchangeRates { get; set; }

        internal static List<Currency> SupportedCurrencies { get; set; }
    }
}
