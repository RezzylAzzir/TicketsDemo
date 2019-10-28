using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;

namespace TicketsDemo.EF.Repositories
{

    public class AgenciesRepository : IAgenciesRepository
    {
        public List<Agency> GetAllAgencies()
        {
            using (var ctx = new TicketsContext())
            {
                return ctx.Agencies.ToList();
            }
        }
        public void CreateAgency(Data.Entities.Agency agency)
        {
            using (var ctx = new TicketsContext())
            {
                ctx.Agencies.Add(agency);

                ctx.SaveChanges();

            }
        }
        public void UpdateAgency(Data.Entities.Agency agency)
        {
            using (var ctx = new TicketsContext())
            {
                ctx.Agencies.Attach(agency);
                ctx.Entry(agency).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public void DeleteAgency(Data.Entities.Agency agency)
        {
            using (var ctx = new TicketsContext())
            {
                ctx.Agencies.Remove(agency);
                ctx.SaveChanges();
            }
        }

        public Data.Entities.Agency GetAgencyDetails(int id)
        {
            var agency = GetAllAgencies().Single(t => t.Id == id);
            return agency;
        }
    }
}
