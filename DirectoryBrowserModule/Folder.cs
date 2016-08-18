using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryBrowserModule
{
    public class Folder
    {
        public string Name { get; private set; }
        public string FullPath { get; private set; }

        public IEnumerable<FileInfo> Files { get; private set; }

        public IEnumerable<Folder> SubFolders { get; private set; }

        public Folder(DirectoryInfo directoryInfo)
        {
            Name = directoryInfo.Name;
            FullPath = directoryInfo.FullName;

            Files = directoryInfo.EnumerateFiles();
            SubFolders = directoryInfo.EnumerateDirectories().Select(o=>new Folder(o)).AsEnumerable();
        }
    }
}
