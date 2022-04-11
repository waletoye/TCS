using System;
using System.Collections.Generic;

namespace tcs_sample1.Models
{
    public class Context
    {
        internal static void LoadExchangeRates()
        {
            //usd
            Models.DataStore.ExchangeRates.Add(new Models.ExchangeRate
            {
                Currency1 = Models.Currency.USD,
                Coefficient1 = 1,
                Currency2 = Models.Currency.DKK,
                Coefficient2 = 100,
            });
            Models.DataStore.ExchangeRates.Add(new Models.ExchangeRate
            {
                Currency1 = Models.Currency.USD,
                Coefficient1 = 1,
                Currency2 = Models.Currency.EUR,
                Coefficient2 = 50,
            });


            //eur
            Models.DataStore.ExchangeRates.Add(new Models.ExchangeRate
            {
                Currency1 = Models.Currency.EUR,
                Coefficient1 = 1,
                Currency2 = Models.Currency.DKK,
                Coefficient2 = 30,
            });
            Models.DataStore.ExchangeRates.Add(new Models.ExchangeRate
            {
                Currency1 = Models.Currency.EUR,
                Coefficient1 = 1,
                Currency2 = Models.Currency.USD,
                Coefficient2 = 0.02m,
            });


            //dkk
            Models.DataStore.ExchangeRates.Add(new Models.ExchangeRate
            {
                Currency1 = Models.Currency.DKK,
                Coefficient1 = 1,
                Currency2 = Models.Currency.EUR,
                Coefficient2 = 0.3m,
            });
            Models.DataStore.ExchangeRates.Add(new Models.ExchangeRate
            {
                Currency1 = Models.Currency.DKK,
                Coefficient1 = 1,
                Currency2 = Models.Currency.USD,
                Coefficient2 = 0.01m,
            });
        }

        /// <summary>
        /// Initialize data
        /// </summary>
        internal static void LoadData()
        {
            Models.DataStore.Voyages ??= new Dictionary<string, Stack<Models.Voyage>>();

            if (Models.DataStore.ExchangeRates == null)
            {
                Models.DataStore.ExchangeRates = new List<Models.ExchangeRate>();
                Models.Context.LoadExchangeRates();
            }

            if (Models.DataStore.SupportedCurrencies == null)
            {
                Models.DataStore.SupportedCurrencies = new List<Models.Currency>();
                Models.DataStore.SupportedCurrencies.Add(Models.Currency.USD);
                Models.DataStore.SupportedCurrencies.Add(Models.Currency.EUR);
                Models.DataStore.SupportedCurrencies.Add(Models.Currency.DKK);
            }
        }
    }
}
