using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using Nancy.Json;
using Microsoft.AspNetCore.Hosting;

namespace Customer_Info_Frontend.ViewModels
{
    public class CustomerVM:BaseModel
    {

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int CountryId { get; set; }
        public string CustomerName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public int MaritalStatus { get; set; }
        public string CustomerPhoto { get; set; }
        //----------------------------------------------------
        public IList<CustomerVM> Customers { get; set; }
        public CustomerVM Customer { get; set; }
        public List<string> Addresses { get; set; }
        public List<CustomerAddressVM> CustomerAddresses { get; set; }
        public CustomerVM()
        {
            
        }

        //----------------------------------------------------

        public IList<CustomerVM> GetCustomerList()
        {
            Customers = new List<CustomerVM>();
            try
            {
                HttpResponseMessage response = GetResponse(Api_Address, ApiController_Customer);

                if (response.IsSuccessStatusCode)
                {
                    var dataObjects = response.Content.ReadFromJsonAsync<IEnumerable<CustomerVM>>().Result;
                    Customers = (IList<CustomerVM>)dataObjects.ToList();
                }

                response.EnsureSuccessStatusCode();
                return Customers;
            }
            catch (Exception)
            {
                return Customers;
                throw;
            }
        }
        public CustomerVM GetSingleCustomer(int customer_id)
        {
            Customer = new CustomerVM();
            var customerAddress = new CustomerAddressVM();
            CustomerAddresses = new List<CustomerAddressVM>();
            try
            {
                HttpResponseMessage response = GetResponse(Api_Address, ApiController_Customer + "/" + customer_id.ToString());

                if (response.IsSuccessStatusCode)
                {

                    var dataObjects = response.Content.ReadFromJsonAsync<CustomerVM>().Result;
                    Customer = dataObjects;
                    var addressListlist = customerAddress.GetCustomerAddressList().Where(x=>x.CustomerId==Customer.CustomerId).ToList();
                    foreach (var address in addressListlist)
                    {
                        CustomerAddresses.Add(address);
                        
                    }
                }
                Customer.CustomerAddresses = CustomerAddresses;
                response.EnsureSuccessStatusCode();
                return Customer;
            }
            catch (Exception)
            {
                return Customer;
                throw;
            }
        }
        public void AddSingleCustomer(string customer)
        {
            CustomerVM obj = new CustomerVM();
            CustomerAddressVM customerAddress = new CustomerAddressVM();
            Random randId = new Random();
            List<CustomerVM> data = new JavaScriptSerializer().Deserialize<List<CustomerVM>>(customer);

            try
            {
                foreach (var cust in data)
                {
                    obj.CustomerName = cust.CustomerName;
                    obj.CountryId = cust.CountryId;
                    obj.CustomerId = randId.Next(1000, 99999);
                    obj.FatherName = cust.FatherName;
                    obj.MotherName = cust.MotherName;
                    obj.MaritalStatus = cust.MaritalStatus;
                    obj.CustomerPhoto=cust.CustomerPhoto;
                    Task<HttpResponseMessage> response1 = PostResponse(Api_Address, ApiController_Customer, obj);

                    foreach (var address in cust.Addresses)
                    {
                        customerAddress.CustomerAddress = address;
                        customerAddress.CustomerId = obj.CustomerId;
                        Task<HttpResponseMessage> response = PostResponse(Api_Address, ApiController_CustomerAddress, customerAddress);
                    }
                }
                
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void EditSingleCustomer(string customer)
        {
            CustomerVM obj = new CustomerVM();
            CustomerAddressVM customerAddress = new CustomerAddressVM();
            Random randId = new Random();
            List<CustomerVM> data = new JavaScriptSerializer().Deserialize<List<CustomerVM>>(customer);

            try
            {
                foreach (var cust in data)
                {
                    obj.Id = cust.Id;
                    obj.CustomerName = cust.CustomerName;
                    obj.CountryId = cust.CountryId;
                    obj.CustomerId = cust.CustomerId;
                    obj.FatherName = cust.FatherName;
                    obj.MotherName = cust.MotherName;
                    obj.MaritalStatus = cust.MaritalStatus;
                    obj.CustomerPhoto = cust.CustomerPhoto;
                    
                    Task<HttpResponseMessage> response1 = PutResponse(Api_Address, ApiController_Customer, cust.Id.ToString(), obj);

                    var customerAddressList = customerAddress.GetCustomerAddressList()
                        .Where(x => x.CustomerId == cust.CustomerId).ToList();

                    foreach (var d in customerAddressList)
                    {
                        customerAddress.DeleteSingleCustomerAddress(d.Id);
                    }

                    foreach (var address in cust.Addresses)
                    {

                        customerAddress.CustomerAddress = address;
                        customerAddress.CustomerId = obj.CustomerId;
                        Task<HttpResponseMessage> response = PutResponse(Api_Address, ApiController_CustomerAddress, cust.Id.ToString(), customerAddress);
                        Thread.Sleep(5);
                    }

                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        public void DeleteSingleCustomer(int id)
        {
            var address = new CustomerAddressVM();
            try
            {
                var custId = GetCustomerList().Where(x => x.CustomerId == id).FirstOrDefault();
                HttpResponseMessage response = DeleteResponse(Api_Address, ApiController_Customer, custId.Id.ToString());

                foreach (var custAddress in address.GetCustomerAddressList().Where(x => x.CustomerId == id).ToList())
                {
                    HttpResponseMessage response2 = DeleteResponse(Api_Address, ApiController_CustomerAddress, custAddress.Id.ToString());
                }

                if (response.IsSuccessStatusCode)
                {

                }
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
