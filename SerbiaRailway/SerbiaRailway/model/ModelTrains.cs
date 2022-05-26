using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbiaRailway.model
{
    public class ModelTrains
    {
        public List<Train> TrainModels { get; set; }

        public ModelTrains()
        {
        }

        public ModelTrains(List<Train> trainModels)
        {
            TrainModels = trainModels;
        }
    }
}
