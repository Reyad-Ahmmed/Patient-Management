using Customer_Info_Backend.Context;
using Customer_Info_Backend.Interface;
using Customer_Info_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Customer_Info_Backend.Repository
{
    public class CountryRepository:ICountryRepository
    {
        private readonly CustomerDbContext _context;

        public CountryRepository(CustomerDbContext context)
        {
            _context=context;
        }

        public async Task<List<Countries>> GetCountryList()
        {
            return await _context.Country.ToListAsync();
        }

        public async Task<Countries> GetSingleCountry(int id)
        {
            var country  = await  _context.Country.Where(x=>x.Id==id).FirstOrDefaultAsync();
            if (country != null)
            {
                return country;
            }
            return country;
        }

        public async Task<int> SaveCountry(Countries country)
        {
            await _context.Country.AddAsync(country);
            await _context.SaveChangesAsync();
            return 0;
        }

        public async Task<int> UpdateCountry(int id, Countries country)
        {
            _context.Entry(country).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountriesExists(id))
                {
                    throw;
                }
            }
            return 0;
        }

        public async Task<int> DeleteCountry(int id)
        {
            var country = await _context.Country.Where(x => x.Id == id).FirstOrDefaultAsync();
            _context.Country.Remove(country);
            await _context.SaveChangesAsync();
            return 0;
        }
        private bool CountriesExists(int id)
        {
            return _context.Country.Any(e => e.Id == id);
        }
    }
}
