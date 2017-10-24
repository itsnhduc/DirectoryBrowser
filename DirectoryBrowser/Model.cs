using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryBrowser
{
    class Model : INotifyPropertyChanged
    {
        private ObservableCollection<string> _history;
        private string _currentPath;
        private DirectoryItem _dirItems;

        public ObservableCollection<string> History { get { return _history; } set { _history = value; NotifyPropertyChanged("History"); } }
        public string CurrentPath { get { return _currentPath; } set { _currentPath = value; NotifyPropertyChanged("CurrentPath"); } }
        public DirectoryItem DirectoryItems { get { return _dirItems; } set { _dirItems = value; NotifyPropertyChanged("DirectoryItems"); } }


        public Model()
        {
            _history = new ObservableCollection<string>();
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
