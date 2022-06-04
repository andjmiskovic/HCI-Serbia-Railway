using System.Collections.Generic;

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
