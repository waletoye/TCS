using System;
using System.Collections.Generic;
using System.Linq;
using tcs_sample1.Interfaces;
using tcs_sample1.Models;
using tcs_sample1.Utilties;

namespace tcs_sample1.Services
{
    public class VoyageService : IVoyage
    {
        decimal IVoyage.GetAverage(string voyageCode, Currency currency)
        {
            decimal result = 0;

            var voyages = Models.DataStore.Voyages.Where(x => x.Key == voyageCode)
                .Select(x => x.Value).FirstOrDefault();


            if (voyages == null)
            {
                return 0;
            }
            else
            {
                foreach (var voyage in voyages)
                    result += Utilties.CurrencyConverter.ConvertCurrency(voyage.Price, voyage.Currency, currency);

                return result / voyages.Count;
            }
        }
       

        Models.Voyage IVoyage.UpdatePrice(Models.Voyage voyage)
        {
            Models.DataStore.Voyages.TryGetValue(voyage.VoyageCode, out Stack<Models.Voyage> existingVoyages);

            if (existingVoyages != null)
            {
                existingVoyages.Push(new Models.Voyage
                {
                    VoyageCode = voyage.VoyageCode,
                    Currency = voyage.Currency,
                    Timestamp = voyage.Timestamp,
                    Price = voyage.Price,
                });
            }
            else
            {
                var voyages = new Stack<Models.Voyage>();

                voyages.Push(new Models.Voyage
                {
                    VoyageCode = voyage.VoyageCode,
                    Currency = voyage.Currency,
                    Timestamp = voyage.Timestamp,
                    Price = voyage.Price,
                });

                Models.DataStore.Voyages.Add(voyage.VoyageCode, voyages);
            }


            return voyage;
        }
    }
}
