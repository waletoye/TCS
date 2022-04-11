using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using tcs_sample1.Utilties;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace tcs_sample1.Controllers
{
    [Route("api/[controller]")]
    public class VoyageController : Controller
    {
        private readonly ILogger<VoyageController> _logger;
        private readonly Interfaces.IVoyage _service;

        public VoyageController(ILogger<VoyageController> logger, Interfaces.IVoyage service)
        {
            _logger = logger;
            _service = service;

            _logger.LogInformation(LogEvents.InitalizeDB, "initializing DB");
            Models.Context.LoadData();
        }


        [HttpGet("{voyageCode}, {currency}")]
        [Route("GetAverage")]
        public IActionResult GetAverage(string voyageCode, Models.Currency currency)
        {
            if (!Models.DataStore.SupportedCurrencies.Contains(currency))
            {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError,
                        "Currency not supported");

                //return NoContent();
            }


            try
            {
                _logger.LogInformation(LogEvents.GetItem, "Getting average price for voyage:{voyageCode}", voyageCode);
                var average = _service.GetAverage(voyageCode, currency);

                _logger.LogInformation(LogEvents.GetItem, "{voyageCode} FOUND", voyageCode);

                return Ok(currency + " " + average.ToString("N"));
            }
            catch (Exception ex)
            {
                _logger.LogWarning(LogEvents.GetItemNotFound, ex, "voyage:({voyageCode}) not found", voyageCode);

                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, "prices not found");
            }
        }


        [HttpPost]
        [Route("UpdatePrice")]
        private IActionResult UpdatePrice(Models.Voyage voyage)
        {
            if (voyage == default)
                return BadRequest(ModelState);

            _logger.LogInformation(LogEvents.UpdateItem, "Updating prices with voyage:{voyageCode}", voyage.VoyageCode);

            try
            {
                var result = _service.UpdatePrice(voyage);
                // return  Ok(result);

                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status201Created, "Price added");

            }
            catch (Exception ex)
            {
                _logger.LogWarning(LogEvents.UpdateItemNotFound, ex, "voyage:({voyageCode})", voyage.VoyageCode);

                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError,
                       "item could not be updated");
            }
        }


        [HttpPost]
        [Route("UpdatePrice")]
        public IActionResult UpdatePrice(string voyageCode, decimal price, Models.Currency currency, DateTimeOffset timestamp)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var voyage = new Models.Voyage
            {
                VoyageCode = voyageCode,
                Price = price,
                Currency = currency,
                Timestamp = timestamp
            };


            _logger.LogInformation(LogEvents.UpdateItem, "Updating prices with voyage:{voyageCode}", voyage.VoyageCode);

            try
            {
                var result = _service.UpdatePrice(voyage);

                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status201Created, "Price added");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(LogEvents.UpdateItemNotFound, ex, "voyage:({voyageCode})", voyage.VoyageCode);

                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError,
                       "item could not be updated");
            }
        }
    }
}
