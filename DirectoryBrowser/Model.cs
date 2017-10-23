using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryBrowser
{
    class Model : INotifyPropertyChanged
    {
        private List<string> _history;
        private string _currentPath;

        public List<string> History { get { return _history; } set { _history = value; NotifyPropertyChanged("History"); } }
        public string CurrentPath { get { return _currentPath; } set { _currentPath = value; NotifyPropertyChanged("CurrentPath"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
