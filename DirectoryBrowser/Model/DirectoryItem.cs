using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryBrowser
{
    public class DirectoryItem
    {
        public enum ItemType { Directory, File }
        public const string LOADING_STR = "...loading...";
        public const string HOME_NAME = "This PC";

        public DirectoryItem Parent { get; }
        public string Name { get; }
        public string NameInPath { get { return Name + (Type == ItemType.Directory ? "\\" : string.Empty); } }
        public ObservableCollection<DirectoryItem> Children { get; }
        public ItemType Type { get; }
        public string Size { get; }
        public DateTime DateCreated { get; }
        public DateTime DateModified { get; }

        public DirectoryItem(string name, DirectoryItem parent, ItemType type)
        {
            Parent = parent;
            Name = name;
            Type = type;
            FileInfo fileInfo = new FileInfo(GetFullPath());
            DateCreated = fileInfo.CreationTime;
            DateModified = fileInfo.LastWriteTime;
            switch (Type)
            {
                case ItemType.Directory:
                    Children = new ObservableCollection<DirectoryItem>();
                    if (name != LOADING_STR)
                    {
                        Children.Add(new DirectoryItem(LOADING_STR, null, ItemType.Directory));
                    }
                    Size = string.Empty;
                    break;
                case ItemType.File:
                default:
                    Size = FileSizeUtil.ConvertToString(fileInfo.Length);
                    break;
            }
            
        }

        public static DirectoryItem GetHome()
        {
            DirectoryItem homeItem = new DirectoryItem(HOME_NAME, null, ItemType.Directory);
            homeItem.Children.Clear();
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                homeItem.Children.Add(new DirectoryItem(drive.Name.Replace("\\", string.Empty), homeItem, ItemType.Directory));
            }
            return homeItem;
        }

        public string GetFullPath()
        {
            string fullPath = NameInPath;
            DirectoryItem tmp = this;
            while (!(tmp.Parent == null))
            {
                tmp = tmp.Parent;
                if (tmp.Name != HOME_NAME)
                {
                    fullPath = tmp.NameInPath + fullPath;
                }
            }
            return fullPath;
        }

        public void LoadChildren()
        {
            if (Name != HOME_NAME)
            {
                string[] dirs = Directory.GetDirectories(GetFullPath());
                string[] files = Directory.GetFiles(GetFullPath());
                Children.Clear();
                foreach (string dir in dirs)
                {
                    Children.Add(new DirectoryItem(dir.Replace(GetFullPath(), string.Empty), this, ItemType.Directory));
                }
                foreach (string file in files)
                {
                    Children.Add(new DirectoryItem(file.Replace(GetFullPath(), string.Empty), this, ItemType.File));
                }
            }
        }

        public DirectoryItem SearchRelativePath(string path)
        {
            DirectoryItem result = this;
            foreach (string dirName in path.Split('\\'))
            {
                if (dirName != string.Empty)
                {
                    if (dirName == HOME_NAME && Name == HOME_NAME)
                    {
                        return this;
                    }
                    else
                    {
                        var filteredChildren = result.Children.Where(child => {
                            return child.Name == dirName;
                        });
                        if (filteredChildren.Count() > 0)
                        {
                            result = filteredChildren.First();
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            return result;
        }
    }
}
