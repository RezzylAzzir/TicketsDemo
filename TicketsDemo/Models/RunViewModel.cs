using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.Models
{
    public class RunViewModel
    {
        public Train Train { get; set; }

        public Agency Agency { get; set; }

        public DateTime RunDate { get; set; }

        public Dictionary<int, Carriage> Carriages { get; set; }

        public Dictionary<int, List<PlaceInRun>> PlacesByCarriage { get; set; }

        public List<int> ReservedPlaces { get; set; }
    }
}