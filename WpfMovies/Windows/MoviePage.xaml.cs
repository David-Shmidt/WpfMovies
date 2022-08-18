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
using System.Windows.Shapes;
using Newtonsoft.Json;
using WpfMovies.Models;

namespace WpfMovies.Windows
{
    /// <summary>
    /// Interaction logic for MoviePage.xaml
    /// </summary>
    public partial class MoviePage : Window
    {
        public MoviePage()
        {
            InitializeComponent();
        }

        public List<Genre> genreList = new List<Genre>();
        
        public List<AgeRate> ageRateList = new List<AgeRate>();
        public Movie? selectedMovie;



        private void ButtonCloseWindow_OnClick(object i_Sender, RoutedEventArgs i_E)
        {
            this.Close();
        }

        private void ButtonSaveChanges_OnClick(object i_Sender, RoutedEventArgs i_E)
        {
            if(selectedMovie == null)
            {
                addMovie();
            }
            else
            {
                updateMovie();
            }
            
        }

        private async void addMovie()
        {
            MovieDbModel movie = new MovieDbModel();
            movie.Name = textBoxMovieName.Text;
            movie.DirectorId = 1;
            movie.AgeRateId = (int)comboBoxAgeRate.SelectedValue;
            movie.GenreId = (int)comboBoxGenreName.SelectedValue;
            movie.Budget = textBoxBudget.Text;
            movie.BoxOffice = textBoxBoxOffice.Text;
            
            HttpClient client = new HttpClient();
            var jsonMovie = JsonConvert.SerializeObject(movie);
            HttpContent content = new StringContent(jsonMovie, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PostAsync("http://localhost:24818/api/Movie", content);
            if(responseMessage.IsSuccessStatusCode)
            {
                MessageBox.Show("Movie Added");
                comboBoxAgeRate.SelectedIndex = -1;
                comboBoxGenreName.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Server Error");
            }
        }

        private async void updateMovie()
        {
            MovieDbModel movie = new MovieDbModel();
            int movieId = selectedMovie.Id;
            movie.Name = textBoxMovieName.Text;
            movie.DirectorId = 1;
            movie.AgeRateId = (int)comboBoxAgeRate.SelectedValue;
            movie.GenreId = (int)comboBoxGenreName.SelectedValue;
            movie.Budget = textBoxBudget.Text;
            movie.BoxOffice = textBoxBoxOffice.Text;

            HttpClient client = new HttpClient();
            string jsonMovie =  JsonConvert.SerializeObject(movie);
            HttpContent content = new StringContent(jsonMovie, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PutAsync("http://localhost:24818/api/Movie/" + movieId, content);
            if(responseMessage.IsSuccessStatusCode)
            {
                MessageBox.Show("Movie Updated");
            }
            else
            {
                MessageBox.Show("Server Error");
            }

        }

        private void onLoaded(object sender, RoutedEventArgs e)
        {
            comboBoxAgeRate.ItemsSource = ageRateList;
            comboBoxGenreName.ItemsSource = genreList;
            comboBoxAgeRate.DisplayMemberPath = "Rate";
            comboBoxAgeRate.SelectedValuePath = "Id";
            comboBoxGenreName.DisplayMemberPath = "GenreName";
            comboBoxGenreName.SelectedValuePath = "Id";
            if(selectedMovie != null)
            {
                textBoxMovieName.Text = selectedMovie.Name;
                comboBoxGenreName.SelectedValue = selectedMovie.GenreId;
                comboBoxAgeRate.SelectedValue = selectedMovie.AgeRateId;
                textBoxBudget.Text = selectedMovie.Budget;
                textBoxBoxOffice.Text = selectedMovie.BoxOffice;
            }
            else
            {
                comboBoxAgeRate.SelectedIndex = -1;
                comboBoxGenreName.SelectedIndex = -1;
            }
           

        }
    }
}
