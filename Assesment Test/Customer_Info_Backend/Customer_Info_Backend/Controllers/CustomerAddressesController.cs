using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Customer_Info_Backend.Context;
using Customer_Info_Backend.Interface;
using Customer_Info_Backend.Models;

namespace Customer_Info_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAddressesController : ControllerBase
    {
        private readonly ICustomerAddressRepository _customerAddressRepository;

        public CustomerAddressesController(ICustomerAddressRepository customerAddressRepository)
        {
            _customerAddressRepository = customerAddressRepository;
        }

        // GET: api/CustomerAddresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerAddresses>>> GetCustomerAddress()
        {
            var customerAddress = await _customerAddressRepository.GetCustomerAddressList();
            return Ok(customerAddress);
        }

        // GET: api/CustomerAddresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerAddresses>> GetCustomerAddresses(int id)
        {
            var customerAddress = await _customerAddressRepository.GetSingleCustomerAddress(id);

            if (customerAddress == null)
            {
                return NotFound();
            }

            return Ok(customerAddress);
        }

        // PUT: api/CustomerAddresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerAddresses(int id, CustomerAddresses customerAddress)
        {
            if (customerAddress.Id == 0)
            {
                await _customerAddressRepository.SaveCustomerAddress(customerAddress);
            }
            else
            {
                await _customerAddressRepository.UpdateCustomerAddress(id, customerAddress);
            }
            
            return NoContent();
        }

        // POST: api/CustomerAddresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerAddresses>> PostCustomerAddresses(CustomerAddresses customerAddress)
        {
            await _customerAddressRepository.SaveCustomerAddress(customerAddress);
            return CreatedAtAction("GetCustomerAddresses", new { id = customerAddress.Id }, customerAddress);
        }

        // DELETE: api/CustomerAddresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerAddresses(int id)
        {
            //var customerAddress = _customerAddressRepository.GetSingleCustomerAddress(id);
            //if (customerAddress == null)
            //{
            //    return NotFound();
            //}

            await _customerAddressRepository.DeleteCustomerAddress(id);
            return NoContent();
        }


    }
}
