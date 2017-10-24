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
        private Model _model = new Model();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _model;
            DirectoryTree.Items.Add(_model.DirectoryItems);
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ForwardBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {

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
            _model.CurrentPath = selectedItem.GetFullPath();
            _model.History.Insert(0, selectedItem.GetFullPath());
            selectedItem.LoadChildren();
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
    }
}
