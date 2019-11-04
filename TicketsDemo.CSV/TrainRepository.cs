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
        private IConfiguration _config;

        public CSVTrainRepository(ICSVReader csvReader, IAgenciesRepository agenciesRepository, IConfiguration configuration)
        {
            _reader = csvReader;
            _agenciesRepo = agenciesRepository;
            _config = configuration;

        }
        #region ITrainRepository Members

        public List<Train> GetAllTrains()
        {

            //var trainsDTO = _reader.ReadFile<TrainCSV>(Path.Combine(_config.CsvDataPath, "TrainsDB.csv"));
            //var carriagesDTO = _reader.ReadFile<TrainCSV>(Path.Combine(_config.CsvDataPath, "carriages.csv"));
            //var placesDTO = _reader.ReadFile<TrainCSV>(Path.Combine(_config.CsvDataPath, "places.csv"));
            //var i = 0;
            //trainsDTO.Select(td => new Train()
            //{

            //});
            var Trains = new List<Train>();
            foreach (var trainDTO in _reader.ReadFile<TrainCSV>(Path.Combine(_config.CsvDataPath, "TrainsDB.csv")))
            {
                var train = new Train()
                {
                    AgencyId = int.Parse(trainDTO.Id),
                    StartLocation = trainDTO.StartLocation,
                    EndLocation = trainDTO.EndLocation,
                    Number = int.Parse(trainDTO.Number),
                    Id = int.Parse(trainDTO.Number),
                    Carriages = new List<Carriage>(),
                };
                foreach (var carriagesDTO in _reader.ReadFile<CarriageCSV>(Path.Combine(_config.CsvDataPath, "carriages.csv")))
                {
                    if (train.Number == int.Parse(carriagesDTO.TrainId))
                    {


                        var carr = new Carriage()
                        {
                            Number = int.Parse(carriagesDTO.CarriageId),
                            TrainId = int.Parse(carriagesDTO.TrainId),
                            Train = train,
                            DefaultPrice = decimal.Parse(carriagesDTO.Price),
                            Id = int.Parse(carriagesDTO.CarriageId),
                            Type = (CarriageType)Enum.Parse(typeof(CarriageType), carriagesDTO.Type),
                            Places = new List<Place>()
                        };
                        var places = int.Parse(carriagesDTO.Places);
                        for (int pl = 1; pl <= places; pl++)
                        {
                            var newPlace = new Place()
                            {
                                Carriage = carr,
                                CarriageId = carr.Id,
                                Number = pl,
                                Id = carr.Id * pl, 
                                PriceMultiplier = 1
                            };
                            carr.Places.Add(newPlace);
                        }
                        train.Carriages.Add(carr);
                    }
                    else
                    {
                        continue;
                    }
                }


                Trains.Add(train);
            }
            return Trains;
            //return trainsDTO.Select(td => new Train()
            //{
            //    AgencyId = int.Parse(td.Id),
            //    Carriages = new List<Carriage>()
            //    {
                    
            //         new Carriage() {
                          
            //              Places = new List<Place>(),
            //              Type = CarriageType.SecondClassSleeping,
            //              DefaultPrice = 100m,
            //              Number = 1,
            //              places = 20,
            //        },new Carriage() {
            //              Places = new List<Place>(),
            //              Type = CarriageType.Sedentary,
            //              DefaultPrice = 40m,
            //              Number = 2,
            //              places = 100,
            //        },new Carriage() {
            //              Places = new List<Place>(),
            //              Type = CarriageType.FirstClassSleeping,
            //              DefaultPrice = 120m,
            //              Number = 3,
            //              places = 10,
            //        }
            //    },
                
            //    StartLocation = td.StartLocation,
            //    EndLocation = td.EndLocation,
            //    Number = int.Parse(td.Number),
            //    Id = int.Parse(td.Number),



            //}).ToList();
        }

        public Data.Entities.Train GetTrainDetails(int id)
        {

            var train = GetAllTrains().Single(t => t.Id == id);
            //train.Carriages = new List<Carriage>();

            var i = 0;
        //    foreach (var carriageDTO in _reader.ReadFile<CarriageCSV>(Path.Combine(_config.CsvDataPath, String.Format("carriage{0}.csv", id))))
        //    {
        //        var carr = new Carriage()
        //        {
        //            Number = int.Parse(carriageDTO.Number),
        //            TrainId = id,
        //            Train = train,
        //            DefaultPrice = decimal.Parse(carriageDTO.Price),
        //            Id = i,
        //            Type = (CarriageType)Enum.Parse(typeof(CarriageType), carriageDTO.Type),
        //            Places = new List<Place>()
        //        };

        //        var places = int.Parse(carriageDTO.Places);
        //        for (int pl = 1; pl <= places; pl++)
        //        {
        //            var newPlace = new Place()
        //            {
        //                Carriage = carr,
        //                CarriageId = carr.Id,
        //                Number = pl,
        //                Id = carr.Id * 1000000 + pl, //magic
        //                PriceMultiplier = 1
        //            };
        //            carr.Places.Add(newPlace);
        //        }

        //        i++;
        //        train.Carriages.Add(carr);
        //    }

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
