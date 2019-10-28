using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.Interfaces;
using TicketsDemo.Models;
using Ninject;

namespace TicketsDemo.Controllers
{
    public class RunController : Controller
    {
        private ITicketRepository _tickRepo;
        private IRunRepository _runRepo;
        private IReservationRepository _reservationRepo;
        private IReservationService _resServ;
        private ITicketService _tickServ;
        private IPriceCalculationStrategy _priceCalc;
        private ITrainRepository _trainRepo;
        private IAgenciesRepository _agenciesRepo;

        public RunController(ITicketRepository tick, IRunRepository run,
            IReservationService resServ,
            ITicketService tickServ,
            IPriceCalculationStrategy priceCalcStrategy,
            IReservationRepository reservationRepo,
            ITrainRepository trainRepo,
            IAgenciesRepository agenciesRepo)
        {
            _tickRepo = tick;
            _runRepo = run;
            _resServ = resServ;
            _tickServ = tickServ;
            _priceCalc = priceCalcStrategy;
            _reservationRepo = reservationRepo;
            _trainRepo = trainRepo;
            _agenciesRepo = agenciesRepo;
        }

        public ActionResult Index(int id)
        {
            var run = _runRepo.GetRunDetails(id);
            var train = _trainRepo.GetTrainDetails(run.TrainId);
            var agency = _agenciesRepo.GetAgencyDetails(train.AgencyId);
            var model = new RunViewModel()
            {
                RunDate = run.Date,
                Carriages = train.Carriages.ToDictionary(x => x.Number),
                PlacesByCarriage = run.Places.GroupBy(x => x.CarriageNumber).ToDictionary(x => x.Key, x => x.ToList()),
                ReservedPlaces = run.Places.Where(p => _resServ.PlaceIsOccupied(p)).Select(p => p.Id).ToList(),
                Train = train,
                Agency = agency,
            };

            return View(model);
        }

        public ActionResult ReservePlace(int placeId)
        {
            var place = _runRepo.GetPlaceInRun(placeId);

            var reservation = _resServ.Reserve(place);

            var model = new ReservationViewModel()
            {
                Reservation = reservation,
                PlaceInRun = place,
                PriceComponents = _priceCalc.CalculatePrice(place),
                Date = place.Run.Date,
                Train = _trainRepo.GetTrainDetails(place.Run.TrainId),
                //Agency = _agenciesRepo.GetAgencyDetails(place.Run.AgencyId),
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateTicket(CreateTicketModel model)
        {
            var tick = _tickServ.CreateTicket(model.ReservationId, model.FirstName, model.LastName);
            return RedirectToAction("Ticket", new { id = tick.Id });
        }

        public ActionResult Ticket(int id)
        {
            var ticket = _tickRepo.Get(id);
            var reservation = _reservationRepo.Get(ticket.Id);
            var placeInRun = _runRepo.GetPlaceInRun(reservation.PlaceInRunId);

            var ticketWM = new TicketViewModel();
            ticketWM.Ticket = ticket;
            ticketWM.PlaceNumber = placeInRun.Number;
            ticketWM.Date = placeInRun.Run.Date;
            ticketWM.Train = _trainRepo.GetTrainDetails(placeInRun.Run.TrainId);
            //ticketWM.Agency = _agenciesRepo.GetAgencyDetails(placeInRun.Run.AgencyId);
            return View(ticketWM);
        }

    }
}