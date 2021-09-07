using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskScanner.Classes
{
    [Serializable]
  public  class MyDirectory
    {
        public string Folder { get; set; }
        public int Count { get; set; }
        public MyDirectory() { }
        public MyDirectory(string folder, int count)
        {
            Folder = folder; Count = count;
        }
    }
}

