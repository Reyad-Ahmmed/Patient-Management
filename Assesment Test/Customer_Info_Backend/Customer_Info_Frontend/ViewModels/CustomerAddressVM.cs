using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Customer_Info_Frontend.ViewModels
{
    public class CustomerAddressVM:BaseModel
    {

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerAddress { get; set; }

        //----------------------------------------------------
        public IList<CustomerAddressVM> CustomerAddresses { get; set; }
        public CustomerAddressVM CustomerAddres { get; set; }

        //----------------------------------------------------

        public IList<CustomerAddressVM> GetCustomerAddressList()
        {
            CustomerAddresses = new List<CustomerAddressVM>();
            try
            {
                HttpResponseMessage response = GetResponse(Api_Address, ApiController_CustomerAddress);

                if (response.IsSuccessStatusCode)
                {
                    var dataObjects = response.Content.ReadFromJsonAsync<IEnumerable<CustomerAddressVM>>().Result;
                    CustomerAddresses = (IList<CustomerAddressVM>)dataObjects.ToList();
                }

                response.EnsureSuccessStatusCode();
                return CustomerAddresses;
            }
            catch (Exception)
            {
                return CustomerAddresses;
                throw;
            }
        }
        public CustomerAddressVM GetSingleCustomerAddress(int customer_address_id)
        {
            CustomerAddres = new CustomerAddressVM();
            try
            {
                HttpResponseMessage response = GetResponse(Api_Address, ApiController_CustomerAddress + "/" + customer_address_id.ToString());

                if (response.IsSuccessStatusCode)
                {
                    var dataObjects = response.Content.ReadFromJsonAsync<CustomerAddressVM>().Result;
                    CustomerAddres = dataObjects;
                }

                response.EnsureSuccessStatusCode();
                return CustomerAddres;
            }
            catch (Exception)
            {
                return CustomerAddres;
                throw;
            }
        }
        public void AddSingleCustomerAddress(CustomerAddressVM customer_address)
        {
            CustomerAddressVM obj = new CustomerAddressVM();
            obj = customer_address;

            try
            {
                Task<HttpResponseMessage> response = PostResponse(Api_Address, ApiController_CustomerAddress, obj);

                if (response.IsCompleted)
                {

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void EditSingleCustomerAddress(CustomerAddressVM customer_address)
        {
            try
            {
                Task<HttpResponseMessage> response = PutResponse(Api_Address, ApiController_CustomerAddress, customer_address.Id.ToString(), customer_address);

                if (response.IsCompleted)
                {

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void DeleteSingleCustomerAddress(int id)
        {
            try
            {
                HttpResponseMessage response = DeleteResponse(Api_Address, ApiController_CustomerAddress, id.ToString());

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
