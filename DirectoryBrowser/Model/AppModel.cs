﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryBrowser.Model
{
    class AppModel : INotifyPropertyChanged
    {
        private ObservableCollection<string> _recent;
        private ObservableCollection<DirectoryItem> _curChildren;
        private string _currentPath;
        private int _dirCount;
        private int _fileCount;
        private string _fileSize;
        private DirectoryItem _dirItems;

        public ObservableCollection<string> Recent { get { return _recent; } set { _recent = value; NotifyPropertyChanged("History"); } }
        public ObservableCollection<DirectoryItem> CurrentChildren { get { return _curChildren; } set { _curChildren = value; NotifyPropertyChanged("CurrentChildren"); } }
        public string CurrentPath { get { return _currentPath; } set { _currentPath = value; NotifyPropertyChanged("CurrentPath"); } }
        public int DirectoryCount { get { return _dirCount; } set { _dirCount = value; NotifyPropertyChanged("DirectoryCount"); } }
        public int FileCount { get { return _fileCount; } set { _fileCount = value; NotifyPropertyChanged("FileCount"); } }
        public string FileSize { get { return _fileSize; } set { _fileSize = value; NotifyPropertyChanged("FileSize"); } }
        public DirectoryItem HomeDirectory { get { return _dirItems; } set { _dirItems = value; NotifyPropertyChanged("DirectoryItems"); } }
        
        public AppModel()
        {
            _recent = new ObservableCollection<string>();
            _dirItems = DirectoryItem.GetHome();
            _currentPath = _dirItems.NameInPath;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
