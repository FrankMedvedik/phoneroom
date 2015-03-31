using System.Collections.Generic;

namespace PhoneLogic.Model
{
    public class RptSeries 
    {
        public string Name{ get; set; }
        public List<DataPoint> Elements = new List<DataPoint>();

    }

    public class DataPoint
    {
        public string IndependentValue { get; set; }
        public int DependentValue { get; set; }
    }


}
