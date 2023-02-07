using Autofac;
using Customer_Info_Frontend.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Customer_Info_Frontend.Controllers
{
    public class CustomersController : Controller
    {
        public IActionResult Index()
        {
            var model = new CustomerVM();
            model.GetCustomerList();
            var country = new CountryVM();
            ViewBag.countryList = country.GetCountryList();
            return View(model);
        }

        [HttpPost]
        public IActionResult AddCustomer(string customer)
        {
            var model = new CustomerVM();
            model.AddSingleCustomer(customer);
            return RedirectToAction("Index");
        }
       
        public JsonResult UpdateCustomer(string customer)
        {
            var model = new CustomerVM();
            model.EditSingleCustomer(customer);
            return Json(1);
        }

        public JsonResult GetCustomerData(int id)
        {
            var model = new CustomerVM();
            model.GetSingleCustomer(id);
            return Json(model);
        }
        public JsonResult DeleteGetCustomerData(int id)
        {
            var model = new CustomerVM();
            model.DeleteSingleCustomer(id);
            return Json(1);
        }

    }
}
