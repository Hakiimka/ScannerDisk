using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;


namespace DiskScanner.Classes
{
    [Serializable]
    public class FolderData
    {
        [JsonIgnore]
        public ObservableCollection<FileInfo> fileInfos { get; set; }
        public ObservableCollection<MyDirectory> directories { get; set; }

        public string _path { get; set; }
        public long summ { get; set; }
        public int filescount { get; set; }
        public DateTime timestart { get; set; }
        public FolderData() { }
        public FolderData(FileInfo[] infos, string path)
        {
            timestart = DateTime.Now;
            fileInfos = new ObservableCollection<FileInfo>(infos);
            foreach (var i in fileInfos)
                summ += i.Length;
            filescount = fileInfos.Count;
            _path = path;

            string[] dirs = Directory.GetDirectories(path);
            directories = new ObservableCollection<MyDirectory>();

            foreach (var i in dirs)
            {
                directories.Add(new MyDirectory(new DirectoryInfo(i).Name, new DirectoryInfo(i).GetFiles("*", SearchOption.AllDirectories).Length));
            }
        }

    }
}
