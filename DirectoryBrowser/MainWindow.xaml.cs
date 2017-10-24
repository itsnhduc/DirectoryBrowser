using System;
using System.Collections.Generic;
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
        private Model _model = new Model();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _model;
            DirectoryTree.Items.Add(_model.HomeDirectory);
        }

        private void _navigateTo(DirectoryItem item)
        {
            // set current path
            _model.CurrentPath = item.GetFullPath();
            if (item.Type == DirectoryItem.ItemType.Directory)
            {
                _model.CurrentChildren = item.Children;

                _model.History.Insert(0, item.GetFullPath());

                item.LoadChildren();

                _model.DirectoryCount = item.Children.Where(child => child.Type == DirectoryItem.ItemType.Directory).Count();
                _model.FileCount = item.Children.Where(child => child.Type == DirectoryItem.ItemType.File).Count();

                _model.FileSize = string.Empty;
            }
            else
            {
                long size = new FileInfo(item.GetFullPath()).Length;
                _model.FileSize = FileSizeUtil.ConvertToString(size);
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            // navigate to directory (from history)
        }

        private void ForwardBtn_Click(object sender, RoutedEventArgs e)
        {
            // navigate to directory (from history)
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

        private void HistoryPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ListView selectedItem = sender as ListView;
            _model.CurrentPath = selectedItem.SelectedItem as string;
        }

        private void Explorer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListView selectedItem = sender as ListView;
            DirectoryItem dirItem = selectedItem.SelectedValue as DirectoryItem;

            if (dirItem != null)
            {
                _navigateTo(dirItem);
            }
        }

        private void Explorer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView selectedItem = sender as ListView;
            DirectoryItem dirItem = selectedItem.SelectedValue as DirectoryItem;

            if (dirItem != null && dirItem.Type == DirectoryItem.ItemType.File)
            {
                long size = new FileInfo(dirItem.GetFullPath()).Length;
                _model.FileSize = FileSizeUtil.ConvertToString(size);
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
