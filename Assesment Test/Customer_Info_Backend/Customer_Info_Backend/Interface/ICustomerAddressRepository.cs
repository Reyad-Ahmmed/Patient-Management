using Customer_Info_Backend.Models;

namespace Customer_Info_Backend.Interface
{
    public interface ICustomerAddressRepository
    {
        Task<List<CustomerAddresses>> GetCustomerAddressList();
        Task<CustomerAddresses> GetSingleCustomerAddress(int id);
        Task<int> SaveCustomerAddress(CustomerAddresses customerAddresses);
        Task<int> UpdateCustomerAddress(int id, CustomerAddresses customerAddresses);
        Task<int> DeleteCustomerAddress(int id);
    }
}
