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

        public DirectoryItem Parent { get; set; }
        public string Name { get; set; }
        public ObservableCollection<DirectoryItem> Children { get; set; }

        public DirectoryItem(string name, DirectoryItem parent, ItemType type)
        {
            Parent = parent;
            switch (type)
            {
                case ItemType.Directory:
                    Name = name + "\\";
                    Children = new ObservableCollection<DirectoryItem>();
                    if (name != LOADING_STR)
                    {
                        Children.Add(new DirectoryItem(LOADING_STR, null, ItemType.Directory));
                    }
                    break;
                case ItemType.File:
                default:
                    Name = name;
                    break;
            }
            
        }

        public static DirectoryItem GetHome()
        {
            DirectoryItem homeItem = new DirectoryItem("Home", null, ItemType.Directory);
            homeItem.Children.Clear();
            homeItem.Children.Add(new DirectoryItem("C:", homeItem, ItemType.Directory));
            homeItem.Children.Add(new DirectoryItem("D:", homeItem, ItemType.Directory));
            return homeItem;
        }

        public string GetFullPath()
        {
            string fullPath = Name;
            DirectoryItem tmp = this;
            while (!(tmp.Parent == null))
            {
                tmp = tmp.Parent;
                if (tmp.Name != "Home\\")
                {
                    fullPath = tmp.Name + fullPath;
                }
            }
            return fullPath;
        }

        public void LoadChildren()
        {
            if (Name != "Home\\")
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
    }
}
