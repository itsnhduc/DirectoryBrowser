using System;
using System.Collections.Generic;
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
        private Model _dataCtx = new Model
        {
            CurrentPath = "Home"
        };

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _dataCtx;
            DirectoryTree.Items.Add(DirectoryItem.GetHome());
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ForwardBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            _dataCtx.CurrentPath = "D:/";
        }

        private void GridViewBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListViewBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DirectoryTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            DirectoryItem selectedItem = e.NewValue as DirectoryItem;
            _dataCtx.CurrentPath = selectedItem.GetFullPath();
        }

        private void DirectoryTree_Expanded(object sender, RoutedEventArgs e)
        {
            DirectoryItem expandedItem = (e.OriginalSource as TreeViewItem).Header as DirectoryItem;
            expandedItem.LoadChildren();
        }
    }
}
