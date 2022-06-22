using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conint.server
{
    class DataSet
    {
        //https://stackoverflow.com/questions/26790477/read-csv-to-list-of-objects
        public string Date { get; set; }
        public float Open { get; set; }
        public float High { get; set; }
        public float Low { get; set; }
        public float Close { get; set; }
        public float AdjClose { get; set; }
        public long Volume { get; set; }

        public static DataSet FromCSV(string line)
        {
            string[] values = line.Split(',');
            DataSet dataset = new DataSet();
            dataset.Date = values[0];
            dataset.Open = float.Parse(values[1], CultureInfo.InvariantCulture);
            dataset.High = float.Parse(values[2], CultureInfo.InvariantCulture);
            dataset.Low = float.Parse(values[3], CultureInfo.InvariantCulture);
            dataset.Close = float.Parse(values[4], CultureInfo.InvariantCulture);
            dataset.AdjClose = float.Parse(values[5], CultureInfo.InvariantCulture);
            dataset.Volume = long.Parse(values[6], CultureInfo.InvariantCulture);
            return dataset;
        }
    }
}
