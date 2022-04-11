using System;
namespace tcs_sample1.Interfaces
{
    public interface IVoyage
    {
        Models.Voyage UpdatePrice(Models.Voyage voyage);

        decimal GetAverage(string voyageCode, Models.Currency currency);
    }
}
