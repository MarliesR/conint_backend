using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace conint.server
{
    public class StockFile
    {
        public string path;
        public string filename;

        public StockFile(string filename)
        {
            this.filename = filename;
        }
        public void createFilePath()
        {
            string projectDirectory = Directory.GetCurrentDirectory();
            string folderpath = projectDirectory + "\\" + filename;
            Console.WriteLine("folderpath: {0}", folderpath);
            path = folderpath;
        }

        public string ConvertToJson()
        {
            List<DataSet> csv = new();
            //liest CSV File aus, skippt erste Zeile (benennung der spalten), mach aus jeder Zeile ein DataSet Objekt und fügt es zur Liste hinzu
            csv = System.IO.File.ReadAllLines(path).Skip(1).Select(v => DataSet.FromCSV(v)).ToList();
            return JsonConvert.SerializeObject(csv);
        }

        public bool CheckIfFileExists(string pathname)
        {
            if (File.Exists(pathname))
            {
                return true;
            }
            return false;
        }
    }
}


