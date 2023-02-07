using Customer_Info_Backend.Context;
using Customer_Info_Backend.Interface;
using Customer_Info_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Customer_Info_Backend.Repository
{
    public class CustomerAddressRepository:ICustomerAddressRepository
    {
        private readonly CustomerDbContext _context;
        public CustomerAddressRepository(CustomerDbContext context)
        {
            _context = context;
        }
        public async Task<List<CustomerAddresses>> GetCustomerAddressList()
        {
            return await _context.CustomerAddress.ToListAsync();
        }

        public async Task<CustomerAddresses> GetSingleCustomerAddress(int id)
        {
            var customerAddress = await _context.CustomerAddress.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (customerAddress != null)
            {
                return customerAddress;
            }
            return customerAddress;
        }

        public async Task<int> SaveCustomerAddress(CustomerAddresses customerAddress)
        {
            await _context.CustomerAddress.AddAsync(customerAddress);
            await _context.SaveChangesAsync();
            return 0;
        }

        public async Task<int> UpdateCustomerAddress(int id, CustomerAddresses customerAddress)
        {
            _context.Entry(customerAddress).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerAddressesExists(id))
                {
                    throw;
                }
            }
            return 0;
        }

        public async Task<int> DeleteCustomerAddress(int id)
        {
            var customerAddress = await _context.CustomerAddress.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (customerAddress != null)
            {
                _context.CustomerAddress.Remove(customerAddress);
                await _context.SaveChangesAsync();
            }
            return 0;
        }

        private bool CustomerAddressesExists(int id)
        {
            return _context.CustomerAddress.Any(e => e.Id == id);
        }
    }
}
