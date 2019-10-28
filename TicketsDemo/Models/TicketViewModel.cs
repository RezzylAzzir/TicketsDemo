using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.Models
{
    public class TicketViewModel
    {
        public Ticket Ticket { get; set; }

        public Train Train { get; set; }

        public DateTime Date { get; set; }

        public int PlaceNumber { get; set; }

        public Agency Agency { get; set; }
    }
}