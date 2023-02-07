using Customer_Info_Backend.Models;

namespace Customer_Info_Backend.Interface
{
    public interface ICountryRepository
    {
        Task<List<Countries>> GetCountryList();
        Task<Countries> GetSingleCountry(int id);
        Task<int> SaveCountry(Countries countries);
        Task<int> UpdateCountry(int id, Countries countries);
        Task<int> DeleteCountry(int id);
    }
}
