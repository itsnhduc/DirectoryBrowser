using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryBrowser.Model
{
    class BrowserHistory
    {
        public ObservableCollection<DirectoryItem> History { get; private set; }
        public int HistoryPointer { get; private set; }

        public BrowserHistory()
        {
            History = new ObservableCollection<DirectoryItem>();
            HistoryPointer = -1;
        }

        private DirectoryItem _jump(int offset)
        {
            DirectoryItem target = null;
            if (HistoryPointer + offset < History.Count && HistoryPointer + offset > -1)
            {
                HistoryPointer += offset;
                target = History[HistoryPointer];
            }
            return target;
        }

        public DirectoryItem Forward() => _jump(1);
        public DirectoryItem Back() => _jump(-1);

        public void Add(DirectoryItem item)
        {
            History.Add(item);
            HistoryPointer++;
        }
    }
}
