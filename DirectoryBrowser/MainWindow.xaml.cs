using DirectoryBrowser.Model;
using DirectoryBrowser.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DirectoryBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AppModel _model = new AppModel();
        private BrowserHistory _history = new BrowserHistory();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _model;
            DirectoryTree.Items.Add(_model.HomeDirectory);
        }

        private void _navigateTo(DirectoryItem item, bool addToHistory = true)
        {
            // set current path
            _model.CurrentPath = item.GetFullPath();
            if (item.Type == DirectoryItem.ItemType.Directory)
            {
                _model.CurrentChildren = item.Children;

                if (_model.Recent.Where(path => path == item.GetFullPath()).Count() == 0)
                {
                    _model.Recent.Insert(0, item.GetFullPath());
                }

                item.LoadChildren();

                _model.DirectoryCount = item.Children.Where(child => child.Type == DirectoryItem.ItemType.Directory).Count();
                _model.FileCount = item.Children.Where(child => child.Type == DirectoryItem.ItemType.File).Count();

                _model.FileSize = string.Empty;

                if (addToHistory) _history.Add(item);
            }
            else
            {
                long size = new FileInfo(item.GetFullPath()).Length;
                _model.FileSize = FileUtil.StringifyShort(size);
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            DirectoryItem target = _history.Back();
            if (target != null) _navigateTo(target, false);
        }

        private void ForwardBtn_Click(object sender, RoutedEventArgs e)
        {
            DirectoryItem target = _history.Forward();
            if (target != null) _navigateTo(target, false);
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            _navigateTo(_model.HomeDirectory);
        }

        private void DirectoryTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            DirectoryItem selectedItem = e.NewValue as DirectoryItem;
            _navigateTo(selectedItem);
        }

        private void DirectoryTree_Expanded(object sender, RoutedEventArgs e)
        {
            DirectoryItem expandedItem = (e.OriginalSource as TreeViewItem).Header as DirectoryItem;
            expandedItem.LoadChildren();
        }

        private void RecentPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ListView selectedItem = sender as ListView;
            _model.CurrentPath = selectedItem.SelectedItem as string;
            _navigateTo(_model.HomeDirectory.SearchRelativePath(_model.CurrentPath));
        }

        private void Explorer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListView selectedItem = sender as ListView;
            DirectoryItem item = selectedItem.SelectedValue as DirectoryItem;

            if (item != null)
            {
                switch (item.Type)
                {
                    case DirectoryItem.ItemType.Directory:
                        _navigateTo(item);
                        break;
                    case DirectoryItem.ItemType.File:
                    default:
                        Process.Start(item.GetFullPath());
                        break;
                }
            }
        }

        private void Explorer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView selectedItem = sender as ListView;
            DirectoryItem dirItem = selectedItem.SelectedValue as DirectoryItem;

            if (dirItem != null && dirItem.Type == DirectoryItem.ItemType.File)
            {
                long size = new FileInfo(dirItem.GetFullPath()).Length;
                _model.FileSize = FileUtil.StringifyShort(size);
            }
        }

        private void AddressBar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DirectoryItem targetItem = _model.HomeDirectory.SearchRelativePath(AddressBar.Text);
                if (targetItem != null)
                {
                    _navigateTo(targetItem);
                }
            }
        }
    }
}
