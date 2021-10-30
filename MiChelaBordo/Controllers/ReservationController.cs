using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiChelaBordo.Models.Response;
using MiChelaBordo.Models;
using MiChelaBordo.Models.Request;



namespace MiChelaBordo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {


        [HttpGet]
        public IActionResult Get()
        {
            ResponseTemplate res = new ResponseTemplate();
            using (MiChelaBordoContext db = new MiChelaBordoContext())
            {
                var lst = db.Reservations.ToList();
                res.Success = 1;
                res.Message = "";
                res.Data = lst;
                return Ok(res);
            }
        }


        [HttpPost]
        public IActionResult Insert(ReservationRequest reservRequest)
        {
            ResponseTemplate res = new ResponseTemplate();
            try
            {
                using (MiChelaBordoContext db = new MiChelaBordoContext())
                {
                    Reservation reservation = new Reservation()
                    {
                        CompleteName = reservRequest.CompleteName,
                        Telephone = reservRequest.Telephone,
                        Email = reservRequest.Email,
                        RouteName = reservRequest.RouteName,
                        TimeSchedule = reservRequest.TimeSchedule
                    };
                    db.Reservations.Add(reservation);
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
                    Reservation del = db.Reservations.Where(e => e.Email == mail).First();
                    db.Reservations.Remove(del);
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
        public IActionResult Modify(ReservationRequest reservationReq)
        {
            ResponseTemplate res = new ResponseTemplate();
            try
            {
                using (MiChelaBordoContext db = new MiChelaBordoContext())
                {
                    Reservation foundReservation = db.Reservations.Where( e => e.Email == reservationReq.Email).First();

                    // Update values
                    ModifyItems(ref foundReservation, reservationReq);

                    // Performs modification
                    db.Entry(foundReservation).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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


        void ModifyItems(ref Reservation reservation, ReservationRequest request)
        {
            //Gets what changed
            if (request.CompleteName != reservation.CompleteName)
                reservation.CompleteName = request.CompleteName;

            if (request.Telephone != reservation.Telephone)
                reservation.Telephone = request.Telephone;

            if (request.Email != reservation.Email)
                reservation.Email = request.Email;

            if (request.RouteName != reservation.RouteName)
                reservation.RouteName = request.RouteName;

            if (request.TimeSchedule != reservation.TimeSchedule)
                reservation.TimeSchedule = request.TimeSchedule;
        }
    }
}
