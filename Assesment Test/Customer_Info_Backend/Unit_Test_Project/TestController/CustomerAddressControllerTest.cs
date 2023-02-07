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
    public class CustomerAddressControllerTest : IClassFixture<AppInstance>
    {
        private readonly AppInstance _appInstance;
        public CustomerAddressControllerTest(AppInstance appInstance)
        {
            _appInstance = appInstance;
        }
        [Fact]

        public void GetCustomerAddressListForTest()
        {
            // Assign

            var mockRepo = new Mock<ICustomerAddressRepository>();
            mockRepo.Setup(repo => repo.GetCustomerAddressList()).ReturnsAsync(GetTestCustomerAddressList());
            var controller = new CustomerAddressesController(mockRepo.Object);

            // Act

            var result = controller.GetCustomerAddress().Result;

            //Assert

            var viewResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<ActionResult>(viewResult);
            Assert.Equal(200, viewResult.StatusCode);
            Assert.NotNull(result);
            mockRepo.Verify();
        }

        [Fact]
        public void GetSingleCustomerAddressForTest()
        {
            // Assign

            var mockRepo = new Mock<ICustomerAddressRepository>();
            mockRepo.Setup(repo => repo.GetSingleCustomerAddress(1)).ReturnsAsync(GetSingleCustomerAddressTest(1));
            var controller = new CustomerAddressesController(mockRepo.Object);

            // Act

            var result = controller.GetCustomerAddresses(1).Result;

            // Assert

            var viewResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<ActionResult>(viewResult);
            Assert.NotNull(model);
            Assert.Equal(200, viewResult.StatusCode);
            mockRepo.Verify();
        }

        [Fact]
        public void CreateCustomerAddressTest()
        {
            // Assign

            var mockRepo = new Mock<ICustomerAddressRepository>();
            var controller = new CustomerAddressesController(mockRepo.Object);
            var newCustomerAddress = CreateCustomerAddressFunction();

            // Act
            var result = controller.PostCustomerAddresses(newCustomerAddress).Result;

            // Assert

            var actualResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.NotNull(actualResult);
            Assert.Equal(201, actualResult.StatusCode);
            mockRepo.Verify();
        }

        [Fact]
        public void UpdateCustomerAddressForTest()
        {
            // Assign

            var mockRepo = new Mock<ICustomerAddressRepository>();
            var id = 1;
            var editCustomerAddress = EditCustomerAddressFunction();
            var controller = new CustomerAddressesController(mockRepo.Object);

            // Act

            var result = controller.PutCustomerAddresses(id, editCustomerAddress).Result;

            // Assert

            var actualResult = Assert.IsType<NoContentResult>(result);
            Assert.NotNull(actualResult);
            Assert.Equal(204, actualResult.StatusCode);
            mockRepo.Verify();
        }
        [Fact]
        public void DeleteCustomerAddressForTest()
        {
            // Assign

            var mockRepo = new Mock<ICustomerAddressRepository>();
            mockRepo.Setup(repo => repo.DeleteCustomerAddress(It.IsAny<int>()))
                .Verifiable();
            var controller = new CustomerAddressesController(mockRepo.Object);
            var id = GetSingleCustomerAddressTest(1).Id;

            // Act
            var result = controller.DeleteCustomerAddresses(id).Result;

            // Assert
            var actualResult = Assert.IsType<NoContentResult>(result);
            Assert.NotNull(actualResult);
            Assert.Equal(204, actualResult.StatusCode);
            mockRepo.Verify();
        }


        ////------------------------------------------------------------------
        private List<CustomerAddresses> GetTestCustomerAddressList()
        {
            return new List<CustomerAddresses>()
            {
                new CustomerAddresses(){Id=1, CustomerAddress="Dhaka, Bangladesh",CustomerId=012},
                new CustomerAddresses(){Id=2, CustomerAddress="Tokyo, Japan",CustomerId=013}
            };

        }
        private CustomerAddresses GetSingleCustomerAddressTest(int id)
        {
            var data = GetTestCustomerAddressList().Where(x => x.Id == id).FirstOrDefault();
            return data;
        }

        private CustomerAddresses CreateCustomerAddressFunction()
        {
            return new CustomerAddresses()
            {
                Id = 1,
                CustomerAddress = "Dhaka, Bangladesh",
                CustomerId = 012
            };
        }
        private CustomerAddresses EditCustomerAddressFunction()
        {
            return new CustomerAddresses()
            {
                Id = 1,
                CustomerAddress = "Dhaka, Bangladesh",
                CustomerId = 012

            };
        }
    }
}
