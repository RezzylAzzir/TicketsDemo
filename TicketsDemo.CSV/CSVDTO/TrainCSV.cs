﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDemo.CSV.CSVDTO
{
    public class TrainCSV
    {
        public int AgencyId { get; set; }
        public string Id { get; set; }
        public string Number { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
    }
}
