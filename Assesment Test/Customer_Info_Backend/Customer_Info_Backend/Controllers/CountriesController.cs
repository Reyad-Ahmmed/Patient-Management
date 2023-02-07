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
    public class CountriesController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;

        public CountriesController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Countries>>> GetCountry()
        {
            var countries = await _countryRepository.GetCountryList();
            return Ok(countries);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Countries>> GetCountries(int id)
        {
            var country = await _countryRepository.GetSingleCountry(id);

            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountries(int id, Countries countries)
        {
            if (id != countries.Id)
            {
                return BadRequest();
            }

            await _countryRepository.UpdateCountry(id, countries);
            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Countries>> PostCountries(Countries countries)
        {
            await _countryRepository.SaveCountry(countries);
            return CreatedAtAction("GetCountries", new { id = countries.Id }, countries);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountries(int id)
        {
            //var countries = _countryRepository.GetSingleCountry(id);
            //if (countries == null)
            //{
            //    return NotFound();
            //}

            await _countryRepository.DeleteCountry(id);
            return NoContent();
        }

       
    }
}
