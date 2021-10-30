using MiChelaBordo.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiChelaBordo.Models.Response;
using MiChelaBordo.Services;
using MiChelaBordo.Models;

namespace MiChelaBordo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class SalesController : ControllerBase
    {
        private ISaleService _sale;

        public SalesController(ISaleService sale)
        {
            this._sale = sale;
        }


        [HttpPost]
        public IActionResult Add(SalesRequest model)
        {
            ResponseTemplate res = new ResponseTemplate();
            try
            {
                _sale.Add(model);
                res.Success = 1;
                return Ok(res);
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                return BadRequest(res);
            }          
        }




    }
}
