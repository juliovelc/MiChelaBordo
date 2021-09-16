using MiChelaBordo.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiChelaBordo.Models.Response;
using MiChelaBordo.Models;

namespace MiChelaBordo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class SalesController : ControllerBase
    {
        [HttpPost]
        public IActionResult Add(SalesRequest model)
        {
            ResponseTemplate res = new ResponseTemplate();
            using (MiChelaBordoContext db = new MiChelaBordoContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        Purchase purchase = new Purchase()
                        {
                            Subtotal = model.Concepts.Sum(x => x.Quantity * x.UnitPrice),
                            PurchaseTime = DateTime.Now,
                            UserMail = model.IdMail,
                        };
                        purchase.Total = Decimal.Multiply(purchase.Subtotal, 1.16m);

                        db.Purchases.Add(purchase);
                        db.SaveChanges(); //after this, purchase now have an id

                        //adding concepts
                        foreach (var mConcept in model.Concepts)
                        {
                            var c = new Models.Concept
                            {
                                Amount = mConcept.Amount,
                                ProductId = mConcept.IdProduct,
                                UnitPrice = mConcept.UnitPrice,
                                PurchaseId = purchase.Id,
                            };
                            db.Concepts.Add(c);

                        }
                        db.SaveChanges();
                        transaction.Commit();
                        res.Success = 1;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        res.Message = ex.Message;
                    }
                    
                }

            }
            return Ok(res);
        }




    }
}
