using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiChelaBordo.Models;
using MiChelaBordo.Models.Response;
using MiChelaBordo.Models.Request;

namespace MiChelaBordo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
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
                        Pass = customerRequest.Pass,
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


    }
}
