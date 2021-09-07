using System;

namespace DiskScanner.Classes
{
    [Serializable]
    public class Exe
    {
        public string Extension { get; set; }
        public int Count { get; set; }
        public Exe() { }
        public Exe(string ext, int count)
        {
            Extension = ext; Count = count;
        }
    }
}
