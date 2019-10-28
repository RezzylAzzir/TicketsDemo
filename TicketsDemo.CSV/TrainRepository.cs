using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.CSV.CSVDTO;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
using TicketsDemo.EF.Repositories;

namespace TicketsDemo.CSV.Repositories
{
    public class CSVTrainRepository : ITrainRepository
    {

        private ICSVReader _reader;
        public IAgenciesRepository _agenciesRepo;

        public CSVTrainRepository(ICSVReader csvReader, IAgenciesRepository agenciesRepository)
        {
            _reader = csvReader;
            _agenciesRepo = agenciesRepository;

        }
        #region ITrainRepository Members

        public List<Train> GetAllTrains()
        {

            var trainsDTO = _reader.ReadFile<TrainCSV>(Path.Combine("C:/vitagames/", "TrainsDB.csv"));

            return trainsDTO.Select(td => new Train()
            {
                AgencyId = int.Parse(td.Id),
                Carriages = new List<Carriage>(),
                StartLocation = td.StartLocation,
                EndLocation = td.EndLocation,
                Number = int.Parse(td.Number),
                Id = int.Parse(td.Number),



            }).ToList();
        }

        public Data.Entities.Train GetTrainDetails(int id)
        {

            var train = GetAllTrains().Single(t => t.Id == id);
            train.Carriages = new List<Carriage>();

            var i = 0;
            foreach (var carriageDTO in _reader.ReadFile<CarriageCSV>(Path.Combine("C:/vitagames", String.Format("carriage{0}.csv", id))))
            {
                var carr = new Carriage()
                {
                    Number = int.Parse(carriageDTO.Number),
                    TrainId = id,
                    Train = train,
                    DefaultPrice = decimal.Parse(carriageDTO.Price),
                    Id = i,
                    Type = (CarriageType)Enum.Parse(typeof(CarriageType), carriageDTO.Type),
                    Places = new List<Place>()
                };

                var places = int.Parse(carriageDTO.Places);
                for (int pl = 1; pl <= places; pl++)
                {
                    var newPlace = new Place()
                    {
                        Carriage = carr,
                        CarriageId = carr.Id,
                        Number = pl,
                        Id = carr.Id * 1000000 + pl, //magic
                        PriceMultiplier = 1
                    };
                    carr.Places.Add(newPlace);
                }

                i++;
                train.Carriages.Add(carr);
            }

            return train;
        }

        public void CreateTrain(Data.Entities.Train train)
        {
            throw new NotImplementedException();
        }

        public void UpdateTrain(Data.Entities.Train train)
        {
            throw new NotImplementedException();
        }

        public void DeleteTrain(Data.Entities.Train train)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
