using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiChelaBordo.Models;
using MiChelaBordo.Models.Response;
using MiChelaBordo.Models.Request;
using MiChelaBordo.Services;
using Microsoft.AspNetCore.Authorization;

namespace MiChelaBordo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  


    public class CustomerController : ControllerBase
    {
        private ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("login")]
        public IActionResult Authenticate([FromBody] AuthRequest model)
        {
            ResponseTemplate res = new ResponseTemplate();
            var authResponse = _customerService.Auth(model);

            if (authResponse == null)
            {
                res.Success = 0;
                res.Message = "Wrong username or password";
                return BadRequest(res);
            }
            res.Success = 1;
            res.Data = authResponse;
            return Ok(res);
        }


        [HttpGet]
        public IActionResult Get()
        {
            ResponseTemplate res = new ResponseTemplate();
            using (MiChelaBordoContext db = new MiChelaBordoContext())
            {
                var lst = db.Customers.ToList();
                res.Success = 1;
                res.Message = "";
                res.Data = lst;
                return Ok(res);
            }
        }


        [HttpPost]
        public IActionResult Insert(CustomerRequest customerRequest)
        {
            ResponseTemplate res = new ResponseTemplate();
            try
            {
                using (MiChelaBordoContext db = new MiChelaBordoContext())
                {
                    Customer customer = new Customer()
                    {
                        CompleteName = customerRequest.CompleteName,
                        IdMail = customerRequest.IdMail,
                        Pass = Tools.Encrypt.GetSHA252(customerRequest.Pass),
                        Rfc = customerRequest.Rfc,
                        TelNumber = customerRequest.TelNumber
                    };
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    res.Success = 1;
                }
            }
            catch (Exception ex)
            {

                res.Message = ex.Message;
            }
            return Ok(res);
        }

        [HttpDelete("{mail}")]
        public IActionResult Delete(string mail)
        {
            ResponseTemplate res = new ResponseTemplate();
            try
            {
                using (MiChelaBordoContext db = new MiChelaBordoContext())
                {
                    Customer del = db.Customers.Where(r => r.IdMail == mail).First();
                    db.Customers.Remove(del);
                    db.SaveChanges();
                    res.Success = 1;
                }

            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
            }
            return Ok(res);
        }



        [HttpPut]
        public IActionResult Modify(CustomerRequest customer)
        {
            ResponseTemplate res = new ResponseTemplate();
            try
            {
                using (MiChelaBordoContext db = new MiChelaBordoContext())
                {
                    Customer foundCustomer = db.Customers.Find(customer.IdMail);

                    //Update values
                    ModifyItems(ref foundCustomer, customer);

                    //Performs modification
                    db.Entry(foundCustomer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    res.Success = 1;
                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                res.Success = 0;
                res.Message = ex.Message;
                return BadRequest(res);
            }
           






        }


        void ModifyItems(ref Customer customer, CustomerRequest custRequest)
        {
            //Gets what changed
            if (custRequest.CompleteName != customer.CompleteName)
                customer.CompleteName = custRequest.CompleteName;

            if (custRequest.Pass != customer.Pass)
                customer.Pass = custRequest.Pass;

            if (custRequest.Rfc != customer.Rfc)
                customer.Rfc = custRequest.Rfc;

            if (custRequest.TelNumber != customer.TelNumber)
                customer.TelNumber = custRequest.TelNumber;
        }


    }
}
