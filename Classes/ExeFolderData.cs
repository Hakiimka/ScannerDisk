using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskScanner.Classes
{
    [Serializable]
    public class ExeFolderData
    {
        
        
        string path;
        public long summ { get; set; }
        public int filescount { get; set; }
       public  ObservableCollection<FileInfo> fileInfos { get; set; }
        public ObservableCollection<Exe> exe { get; set; }
        public DateTime timestart { get; set; }
        public ExeFolderData() { }
        public ExeFolderData(FileInfo[] infos,string path)
        {
            timestart = DateTime.Now;
            this.path = path;
           
            fileInfos = new ObservableCollection<FileInfo>(infos.ToList());
            foreach (var i in fileInfos)
                summ += i.Length;
            filescount = fileInfos.Count;

            var extensionCounts = fileInfos
                        .GroupBy(x => x.Extension)
                        .Select(g => new Exe(g.Key, g.Count()));

                       
            exe = new ObservableCollection<Exe>(extensionCounts);
        }
    }
}
