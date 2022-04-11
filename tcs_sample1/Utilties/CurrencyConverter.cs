using System;
using System.Linq;

namespace tcs_sample1.Utilties
{
    public class CurrencyConverter
    {
        public static decimal ConvertCurrency(decimal price, Models.Currency fromCurrency, Models.Currency toCurrency)
        {
            if (fromCurrency == toCurrency)
                return price;

            decimal? coefficient = Models.DataStore.ExchangeRates.Where(x => x.Currency1 == fromCurrency && x.Currency2 == toCurrency).Select(x => x.Coefficient2).FirstOrDefault();

            if (coefficient == null)
                return 0;


            decimal results = price * coefficient.Value;

            return results;
        }
    }
}
