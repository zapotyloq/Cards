using Cards.Models;
using Client.ViewModels;
using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace Client.Views
{
    /// <summary>
    /// Логика взаимодействия для CardsView.xaml
    /// </summary>
    public partial class CardsView : Window
    {
        public CardsView()
        {
            try
            {
                DataContext = new CardsViewModel();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
            InitializeComponent();
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            var item = ((CardsViewModel)DataContext).SelectedItem;
            try
            {
                var response = await ((CardsViewModel)DataContext).UpdateAsync(item);
                if (response != null)
                    MessageBox.Show("Сard updated successfully");
                else
                    MessageBox.Show("Update error");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);   
            }

        }

        private async void Del_Click(object sender, RoutedEventArgs e)
        {
            int id = ((CardsViewModel)DataContext).SelectedItem.Id;

            try
            {
                var response = await ((CardsViewModel)DataContext).DeleteAsync(id); ;
                if (response != null)
                    MessageBox.Show("Сard deleted successfully");
                else
                    MessageBox.Show("Delete error");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);   
            }
        }

        private async void Insert_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                var item = ((CardsViewModel)DataContext).SelectedItem;

                var response = await ((CardsViewModel)DataContext).AddAsync(item);
                if (response != null)
                {
                    MessageBox.Show("Сard added successfully");
                }
                else
                    MessageBox.Show("Insert error");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);   
            }
        }

        private void LoadImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPG images | *.jpg";
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName != string.Empty)
            {
                FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read);

                byte[] imgByteArr = new byte[fs.Length];

                fs.Read(imgByteArr, 0, Convert.ToInt32(fs.Length));
                fs.Close();

                ((CardsViewModel)DataContext).SelectedItem.File = imgByteArr;
                ((CardsViewModel)DataContext).RefreshSelectedData();
            }
        }
    }
}
