using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Customer_Info_Frontend.ViewModels
{
    public class CountryVM:BaseModel
    {
        public int Id { get; set; }
        public string CountryName { get; set; }

        //----------------------------------------------------
        public IList<CountryVM> Countries { get; set; }
        public CountryVM Country { get; set; }

        //----------------------------------------------------

        public IList<CountryVM> GetCountryList()
        {
            Countries = new List<CountryVM>();
            try
            {
                HttpResponseMessage response = GetResponse(Api_Address, ApiController_Country);

                if (response.IsSuccessStatusCode)
                {
                    var dataObjects = response.Content.ReadFromJsonAsync<IEnumerable<CountryVM>>().Result;
                    Countries = (IList<CountryVM>)dataObjects.ToList();
                }

                response.EnsureSuccessStatusCode();
                return Countries;
            }
            catch (Exception)
            {
                return Countries;
                throw;
            }
        }
        public CountryVM GetSingleCountry(int country_id)
        {
            Country = new CountryVM();
            try
            {
                HttpResponseMessage response = GetResponse(Api_Address, ApiController_Country + "/" + country_id.ToString());

                if (response.IsSuccessStatusCode)
                {
                    var dataObjects = response.Content.ReadFromJsonAsync<CountryVM>().Result;
                    Country = dataObjects;
                }

                response.EnsureSuccessStatusCode();
                return Country;
            }
            catch (Exception)
            {
                return Country;
                throw;
            }
        }
        public void AddSingleCountry(CountryVM country)
        {
            CountryVM obj = new CountryVM();
            obj = country;

            try
            {
                Task<HttpResponseMessage> response = PostResponse(Api_Address, ApiController_Country, obj);

                if (response.IsCompleted)
                {

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void EditSingleCountry(CountryVM country)
        {
            try
            {
                Task<HttpResponseMessage> response = PutResponse(Api_Address, ApiController_Country, country.Id.ToString(), country);

                if (response.IsCompleted)
                {

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void DeleteSingleCountry(int id)
        {
            try
            {
                HttpResponseMessage response = DeleteResponse(Api_Address, ApiController_Country, id.ToString());

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
