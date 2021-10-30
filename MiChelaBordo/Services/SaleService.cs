using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiChelaBordo.Models;
using MiChelaBordo.Models.Request;
using MiChelaBordo.Models.Response;

namespace MiChelaBordo.Services
{
    public class SaleService : ISaleService
    {
        public void Add(SalesRequest model)
        {
            using (MiChelaBordoContext db = new MiChelaBordoContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        Purchase purchase = GeneratePurchase(model, db);
                        InsertConcepts(model, purchase.Id, db);
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();

                        throw new Exception("An error ocurred in the insertion");
                    }
                }
            }
        }


        void InsertConcepts(SalesRequest model, int purchaseId, MiChelaBordoContext db)
        {
            foreach (var mConcept in model.Concepts)
            {
                var c = new Models.Concept
                {
                    Amount = mConcept.Amount,
                    ProductId = mConcept.IdProduct,
                    UnitPrice = mConcept.UnitPrice,
                    PurchaseId = purchaseId,
                };
                db.Concepts.Add(c);

            }
            db.SaveChanges();
        }

        Purchase GeneratePurchase(SalesRequest model, MiChelaBordoContext db)
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

            return purchase;
        }

    }
}
