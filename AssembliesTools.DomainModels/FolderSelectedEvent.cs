using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;

namespace AssembliesTools.DomainModels
{
    public class FolderSelectedEvent : PubSubEvent<Folder>
    {
    }


    public class Folder
    {
        public string Name { get; private set; }
        public string FullPath { get; private set; }

        public Folder(string fullPath)
        {
            FullPath = fullPath;
            if(!string.IsNullOrEmpty(fullPath))
                Name = System.IO.Path.GetDirectoryName(fullPath);
        }
    }
}
