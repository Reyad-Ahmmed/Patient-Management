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
    public class CustomerControllerTest : IClassFixture<AppInstance>
    {
        private readonly AppInstance _appInstance;
        public CustomerControllerTest(AppInstance appInstance)
        {
            _appInstance = appInstance;
        }
        [Fact]

        public void GetCustomerListForTest()
        {
            // Assign

            var mockRepo = new Mock<ICustomerRepository>();
            mockRepo.Setup(repo => repo.GetCustomerList()).ReturnsAsync(GetTestCustomerList());
            var controller = new CustomersController(mockRepo.Object);

            // Act

            var result = controller.GetCustomer().Result;

            //Assert

            var viewResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<ActionResult>(viewResult);
            Assert.Equal(200, viewResult.StatusCode);
            Assert.NotNull(result);
            mockRepo.Verify();
        }

        [Fact]
        public void GetSingleCustomerForTest()
        {
            // Assign

            var mockRepo = new Mock<ICustomerRepository>();
            mockRepo.Setup(repo => repo.GetSingleCustomer(1)).ReturnsAsync(GetSingleCustomerTest(1));
            var controller = new CustomersController(mockRepo.Object);

            // Act

            var result = controller.GetCustomers(1).Result;

            // Assert

            var viewResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<ActionResult>(viewResult);
            Assert.NotNull(model);
            Assert.Equal(200, viewResult.StatusCode);
            mockRepo.Verify();
        }

        [Fact]
        public void CreateCustomerTest()
        {
            // Assign

            var mockRepo = new Mock<ICustomerRepository>();
            var controller = new CustomersController(mockRepo.Object);
            var newCustomer = CreateCustomerFunction();

            // Act
            var result = controller.PostCustomers(newCustomer).Result;

            // Assert

            var actualResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.NotNull(actualResult);
            Assert.Equal(201, actualResult.StatusCode);
            mockRepo.Verify();
        }

        [Fact]
        public void UpdateCustomerForTest()
        {
            // Assign

            var mockRepo = new Mock<ICustomerRepository>();
            var id = 1;
            var editCustomer = EditCustomerFunction();
            var controller = new CustomersController(mockRepo.Object);

            // Act

            var result = controller.PutCustomers(id, editCustomer).Result;

            // Assert

            var actualResult = Assert.IsType<NoContentResult>(result);
            Assert.NotNull(actualResult);
            Assert.Equal(204, actualResult.StatusCode);
            mockRepo.Verify();
        }
        [Fact]
        public void DeleteCustomerForTest()
        {
            // Assign

            var mockRepo = new Mock<ICustomerRepository>();
            mockRepo.Setup(repo => repo.DeleteCustomer(It.IsAny<int>()))
                .Verifiable();
            var controller = new CustomersController(mockRepo.Object);
            var id = GetSingleCustomerTest(1).Id;

            // Act
            var result = controller.DeleteCustomers(id).Result;

            // Assert
            var actualResult = Assert.IsType<NoContentResult>(result);
            Assert.NotNull(actualResult);
            Assert.Equal(204, actualResult.StatusCode);
            mockRepo.Verify();
        }


        ////------------------------------------------------------------------
        private List<Customers> GetTestCustomerList()
        {
            return new List<Customers>()
            {
                new Customers(){Id=1, CustomerId=012,CustomerName="demo customer",FatherName="demo father",MotherName="demo mother",MaritalStatus=2,CountryId=1,CustomerPhoto=""},
                new Customers(){Id=2, CustomerId=013,CustomerName="demo customer",FatherName="demo father",MotherName="demo mother",MaritalStatus=2,CountryId=1,CustomerPhoto=""}
            };

        }
        private Customers GetSingleCustomerTest(int id)
        {
            var data = GetTestCustomerList().Where(x => x.Id == id).FirstOrDefault();
            return data;
        }

        private Customers CreateCustomerFunction()
        {
            return new Customers()
            {
                Id = 1,
                CustomerId = 012,
                CustomerName = "demo customer",
                FatherName = "demo father",
                MotherName = "demo mother",
                MaritalStatus = 2,
                CountryId = 1,
                CustomerPhoto = ""
            };
        }
        private Customers EditCustomerFunction()
        {
            return new Customers()
            {
                Id = 1,
                CustomerId = 012,
                CustomerName = "demo customer edited",
                FatherName = "demo father edited",
                MotherName = "demo mother edited",
                MaritalStatus = 1,
                CountryId = 2,
                CustomerPhoto = ""

            };
        }
    }
}
