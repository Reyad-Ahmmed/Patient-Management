using Customer_Info_Backend.Controllers;
using Customer_Info_Backend.Interface;
using Customer_Info_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Unit_Test_Project.TestController
{
    public class CountryControllerTest : IClassFixture<AppInstance>
    {
        private readonly AppInstance _appInstance;
        public CountryControllerTest(AppInstance appInstance)
        {
            _appInstance = appInstance;
        }
        [Fact]

        public void GetCountryListForTest()
        {
            // Assign

            var mockRepo = new Mock<ICountryRepository>();
            mockRepo.Setup(repo => repo.GetCountryList()).ReturnsAsync(GetTestCountryList());
            var controller = new CountriesController(mockRepo.Object);

            // Act

            var result = controller.GetCountry().Result;

            //Assert

            var viewResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<ActionResult>(viewResult);
            Assert.Equal(200, viewResult.StatusCode);
            Assert.NotNull(result);
            mockRepo.Verify();
        }

        [Fact]
        public void GetSingleCountryForTest()
        {
            // Assign

            var mockRepo = new Mock<ICountryRepository>();
            mockRepo.Setup(repo => repo.GetSingleCountry(1)).ReturnsAsync(GetSingleCountryTest(1));
            var controller = new CountriesController(mockRepo.Object);

            // Act

            var result = controller.GetCountries(1).Result;

            // Assert

            var viewResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<ActionResult>(viewResult);
            Assert.NotNull(model);
            Assert.Equal(200, viewResult.StatusCode);
            mockRepo.Verify();
        }

        [Fact]
        public void CreateCountryTest()
        {
            // Assign

            var mockRepo = new Mock<ICountryRepository>();
            var controller = new CountriesController(mockRepo.Object);
            var newCountry = CreateCountryFunction();

            // Act
            var result = controller.PostCountries(newCountry).Result;

            // Assert

            var actualResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.NotNull(actualResult);
            Assert.Equal(201, actualResult.StatusCode);
            mockRepo.Verify();
        }

        [Fact]
        public void UpdateCountryForTest()
        {
            // Assign

            var mockRepo = new Mock<ICountryRepository>();
            var id = 1;
            var editCountry = EditCountryFunction();
            var controller = new CountriesController(mockRepo.Object);

            // Act

            var result = controller.PutCountries(id, editCountry).Result;

            // Assert

            var actualResult = Assert.IsType<NoContentResult>(result);
            Assert.NotNull(actualResult);
            Assert.Equal(204, actualResult.StatusCode);
            mockRepo.Verify();
        }
        [Fact]
        public void DeleteCountryForTest()
        {
            // Assign

            var mockRepo = new Mock<ICountryRepository>();
            mockRepo.Setup(repo => repo.DeleteCountry(It.IsAny<int>()))
                .Verifiable();
            var controller = new CountriesController(mockRepo.Object);
            var id = GetSingleCountryTest(1).Id;

            // Act
            var result = controller.DeleteCountries(id).Result;

            // Assert
            var actualResult = Assert.IsType<NoContentResult>(result);
            Assert.NotNull(actualResult);
            Assert.Equal(204, actualResult.StatusCode);
            mockRepo.Verify();
        }


        ////------------------------------------------------------------------
        private List<Countries> GetTestCountryList()
        {
            return new List<Countries>()
            {
                new Countries(){Id=1, CountryName="Bangladesh"},
                new Countries(){Id=2, CountryName="Japan"}
            };

        }
        private Countries GetSingleCountryTest(int id)
        {
            var data = GetTestCountryList().Where(x => x.Id == id).FirstOrDefault();
            return data;
        }

        private Countries CreateCountryFunction()
        {
            return new Countries()
            {
                Id = 1,
                CountryName = "Bangladesh"
            };
        }
        private Countries EditCountryFunction()
        {
            return new Countries()
            {
                Id = 1,
                CountryName = "Japan"
               
            };
        }
    }
}
