using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using Newtonsoft.Json;
using WpfMovies.Models;

namespace WpfMovies.Views
{
    /// <summary>
    /// Interaction logic for DirectorsList.xaml
    /// </summary>
    public partial class DirectorsList : UserControl
    {
        public DirectorsList()
        {
            InitializeComponent();
        }

        private void DirectorsList_OnLoaded(object i_Sender, RoutedEventArgs i_E)
        {
            showDirectorsList("http://localhost:24818/api/Director");
        }

        private async void showDirectorsList(string i_HttpRequestURL)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(i_HttpRequestURL);
            if(response.IsSuccessStatusCode)
            {
                string jsonDirectors =await response.Content.ReadAsStringAsync();
                List<Director> directors = JsonConvert.DeserializeObject<List<Director>>(jsonDirectors);
                gridDirectors.ItemsSource = directors;
            }
            else
            {
                MessageBox.Show("Server Error");
            }
        }
    }
}
