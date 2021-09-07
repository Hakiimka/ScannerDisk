using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskScanner.Classes
{
    public static class ScannerDataRepository
    {

        public static void InsertData(ScannerData Data, ScannerDBContext context)
        {
            context.data.Add(Data);
            context.SaveChanges();
        }
       
    }
}
