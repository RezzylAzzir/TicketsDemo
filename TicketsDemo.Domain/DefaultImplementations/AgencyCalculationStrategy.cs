using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{
    public class AgencyCalculationStrategy : IPriceCalculationStrategy
    {
        private IRunRepository _runRepository;
        private ITrainRepository _trainRepository;
        private IAgenciesRepository _agenciesRepository;

        public AgencyCalculationStrategy(IRunRepository runRepository, ITrainRepository trainRepository, IAgenciesRepository agenciesRepository)
        {
            _runRepository = runRepository;
            _trainRepository = trainRepository;
            _agenciesRepository = agenciesRepository;
        }

        public List<PriceComponent> CalculatePrice(PlaceInRun placeInRun)
        {
            var components = new List<PriceComponent>();

            var run = _runRepository.GetRunDetails(placeInRun.RunId);
            var train = _trainRepository.GetTrainDetails(run.TrainId);
            var agency = _agenciesRepository.GetAgencyDetails(train.AgencyId);
            var place =
                train.Carriages
                    .Select(car => car.Places.SingleOrDefault(pl =>
                        pl.Number == placeInRun.Number &&
                        car.Number == placeInRun.CarriageNumber))
                    .SingleOrDefault(x => x != null);

            var placeComponent = new PriceComponent() { Name = "Main price: " + Convert.ToString(place.Carriage.DefaultPrice) + "$ AgencyTax: " + Convert.ToString(((agency.AgencyPay) - 1) * 100) + "%" + " Full Price:" };

            placeComponent.Value = place.Carriage.DefaultPrice * agency.AgencyPay;
            components.Add(placeComponent);
            var AgencyInfo = new PriceComponent()
            {
                Name = "Agency Tax provided by " + agency.Name,
                Value = place.Carriage.DefaultPrice * ((agency.AgencyPay) - 1)
            };
            components.Add(AgencyInfo);

            return components;
        }
    }
}
