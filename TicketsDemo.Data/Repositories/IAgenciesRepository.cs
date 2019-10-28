using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.Data.Repositories
{
    public interface IAgenciesRepository
    {
        List<Agency> GetAllAgencies();
        Agency GetAgencyDetails(int agencyId);
        void CreateAgency(Agency agency);
        void UpdateAgency(Agency agency);
        void DeleteAgency(Agency agency);
    }
}
