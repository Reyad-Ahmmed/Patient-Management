using Customer_Info_Backend.Models;

namespace Customer_Info_Backend.Interface
{
    public interface ICustomerRepository
    {
        Task<List<Customers>> GetCustomerList();
        Task<Customers> GetSingleCustomer(int id);
        Task<int> SaveCustomer(Customers customers);
        Task<int> UpdateCustomer(int id, Customers customers);
        Task<int> DeleteCustomer(int id);
    }
}
