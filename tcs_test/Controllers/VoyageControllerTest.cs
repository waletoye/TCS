using System;
using Microsoft.Extensions.Logging;
using Moq;
using tcs_sample1.Controllers;
using tcs_sample1.Interfaces;
using tcs_sample1.Models;
using Xunit;

namespace tcs_test.Controllers
{
    public class VoyageControllerTest
    {
        private readonly Mock<IVoyage> _mockRepo;
        private readonly VoyageController _controller;

        public VoyageControllerTest()
        {
            _mockRepo = new Mock<IVoyage>();
            var _logger = new Logger<VoyageController>(new Microsoft.Extensions.Logging.Abstractions.NullLoggerFactory());

            _controller = new VoyageController(_logger, _mockRepo.Object);
        }


        [Fact]
        public void GetAverage_Success()
        {
            var result = _controller.GetAverage("451S", Currency.USD);

            Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result);
        }

        [Fact]
        public void UpdatePrice_Success()
        {
            var result = _controller.UpdatePrice("451S", 109.5m, Currency.EUR, DateTimeOffset.Now);

            var viewResult = Assert.IsAssignableFrom<Microsoft.AspNetCore.Mvc.IActionResult>(result);

            //var testVoyage = Assert.IsType<Voyage>(result);
            //Assert.Equal(voyage.VoyageCode, testVoyage.VoyageCode);
        }


        [Fact]
        public void UpdatePrice_BadRequest()
        {
            var badResponse = _controller.UpdatePrice("451S", 109.5m, Currency.EUR, DateTimeOffset.Now);

            Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestObjectResult>(badResponse);
        }
    }
}
