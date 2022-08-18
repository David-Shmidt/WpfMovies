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
using WpfMovies.ViewModels;
using WpfMovies.Windows;

namespace WpfMovies.Views
{
    /// <summary>
    /// Interaction logic for MovieList.xaml
    /// </summary>
    public partial class MovieList : UserControl
    {
        public MovieList()
        {
            InitializeComponent();
        }

        public MovieVM? movieVM;


        private void onLoaded(object sender, RoutedEventArgs e)
        {
            movieVM = (MovieVM)DataContext;
            if(movieVM.IsActorSelected)
            {
                gridMovies.ItemsSource = movieVM.Movies;
                buttonAddMovie.Visibility = Visibility.Collapsed;
                buttonShowActorsOfMovie.Visibility = Visibility.Collapsed;
            }
            else
            {
                showMoviesList("http://localhost:24818/api/Movie");
            }
            
        }
        private async void ButtonAddMovie_OnClick(object i_Sender, RoutedEventArgs i_E)
        {
            List<Genre> genresList =await getGenreList();
            List<AgeRate> ageRatesList =await getAgeRateList();
            MoviePage moviePage = new MoviePage();
            moviePage.ageRateList = ageRatesList;
            moviePage.genreList = genresList;
            moviePage.ShowDialog();
            showMoviesList("http://localhost:24818/api/Movie");
        }


        private async void ButtonUpdateMovie_OnClick(object i_Sender, RoutedEventArgs i_E)
        {
            Movie movie = (Movie)gridMovies.SelectedValue;
            List<AgeRate> ageRateList = await getAgeRateList();
            List<Genre> genreList = await getGenreList();
            MoviePage page = new MoviePage();
            page.selectedMovie = movie;
            page.ageRateList = ageRateList;
            page.genreList = genreList;
            page.ShowDialog();
            /*If the current Movie list is not
             specific to an actor
            refresh the whole list*/
            if(!movieVM.IsActorSelected) 
            {
                showMoviesList("http://localhost:24818/api/Movie");
            }
            else
            {
                showMoviesList("http://localhost:24818/api/Movie/actor-movies/" + movieVM.SelectedActorId);
            }
        }

        private async Task<List<Genre>> getGenreList()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage responseMessage = await client.GetAsync("http://localhost:24818/api/Genre");
            List<Genre> list = new List<Genre>();
            if(responseMessage.IsSuccessStatusCode)
            {
                string jsonString = await responseMessage.Content.ReadAsStringAsync();
                list = JsonConvert.DeserializeObject<List<Genre>>(jsonString);
            }
            else
            {
                MessageBox.Show("Server Error - Could not retrive genres");
            }
            return list;
        }

        private async Task<List<AgeRate>> getAgeRateList()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage responseMessage = await client.GetAsync("http://localhost:24818/api/AgeRate");
            List<AgeRate> list = new List<AgeRate>();
            if(responseMessage.IsSuccessStatusCode)
            {
                string jsonString = await responseMessage.Content.ReadAsStringAsync();
                list = JsonConvert.DeserializeObject<List<AgeRate>>(jsonString);
            }
            else
            {
                MessageBox.Show("Server Error - Could not retrive Age Rates");
            }
            return list;
        }

       

        private async void ButtonDeleteMovie_OnClick(object i_Sender, RoutedEventArgs i_E)
        {
           MessageBoxResult result =  MessageBox.Show(
                "Are you sure yo want to delete?",
                "Delete Movie",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning,
                MessageBoxResult.Yes);

           switch(result)
           {
               case MessageBoxResult.Yes:
                   HttpClient client = new HttpClient();
                   Movie selectedMovieToDelete = (Movie)gridMovies.SelectedItem;
                   int movieId = selectedMovieToDelete.Id;
                   HttpResponseMessage response = await client.DeleteAsync("http://localhost:24818/api/Movie/" + movieId);
                   if(response.IsSuccessStatusCode)
                   {
                       MessageBox.Show("Movie was deleted");
                       showMoviesList("http://localhost:24818/api/Movie");
                   }
                   break;
           }
        }


        private void ButtonShowMovieActors_OnClick(object i_Sender, RoutedEventArgs i_E)
        {
            ActorsOfMovie actorsOfMoviePage = new ActorsOfMovie();
            Movie selectedMovie = (Movie)gridMovies.SelectedItem;
            int selectedMovieId = selectedMovie.Id;
            actorsOfMoviePage.m_MovieId = selectedMovieId;
            actorsOfMoviePage.ShowDialog();
        }

        private async void showMoviesList(string i_UrlHttpRequest)
        {
            HttpClient client = new HttpClient();
            
            HttpResponseMessage message = await client.GetAsync(i_UrlHttpRequest);
            if(message.IsSuccessStatusCode)
            {
                string jsonString =await message.Content.ReadAsStringAsync();
                List<Movie> movieList = JsonConvert.DeserializeObject<List<Movie>>(jsonString);
                gridMovies.ItemsSource = movieList;
            }
            else
            {
                MessageBox.Show("Server Error");
            }
        }

    }
}
