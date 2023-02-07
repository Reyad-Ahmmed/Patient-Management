using Customer_Info_Backend.Context;
using Customer_Info_Backend.Interface;
using Customer_Info_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Customer_Info_Backend.Repository
{
    public class CustomerRepository:ICustomerRepository
    {
        private readonly CustomerDbContext _context;
        public CustomerRepository(CustomerDbContext context)
        {
            _context = context;
        }
        public async Task<List<Customers>> GetCustomerList()
        {
            return await _context.Customer.ToListAsync();
        }

        public async Task<Customers> GetSingleCustomer(int id)
        {
            var customer = await _context.Customer.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (customer != null)
            {
                return customer;
            }
            return customer;
        }

        public async Task<int> SaveCustomer(Customers customer)
        {
            await _context.Customer.AddAsync(customer);
            await _context.SaveChangesAsync();
            return 0;
        }

        public async Task<int> UpdateCustomer(int id, Customers customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomersExists(id))
                {
                    throw;
                }
            }
            return 0;
        }

        public async Task<int> DeleteCustomer(int id)
        {
            var customer = await _context.Customer.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (customer != null)
            {
                _context.Customer.Remove(customer);
                await _context.SaveChangesAsync();
            }
            return 0;
        }
        private bool CustomersExists(int id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }
    }
}
