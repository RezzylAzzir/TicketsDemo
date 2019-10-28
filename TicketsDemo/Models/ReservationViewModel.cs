using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.Models
{
    public class ReservationViewModel
    {
        public DateTime Date { get; set; }

        public Agency Agency { get; set; }
        public Train Train { get; set; }
        public Reservation Reservation { get; set; }
        public PlaceInRun PlaceInRun { get; set; }
        public List<PriceComponent> PriceComponents { get; set; }
    }
}