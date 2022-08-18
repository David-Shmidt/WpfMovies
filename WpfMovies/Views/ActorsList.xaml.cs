using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
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
    /// Interaction logic for ActorsList.xaml
    /// </summary>
    public partial class ActorsList : UserControl
    {
        private bool m_IsActorSelected = false;

        private ActorVM dataContext;
        public ActorsList()
        {
            InitializeComponent();
        }

        private  void onLoaded(object sender, RoutedEventArgs e)
        {
            dataContext = (ActorVM)DataContext;
            if(dataContext.IsMovieSelected)
            {
                showActorsList("http://localhost:24818/api/Actor/movie-actors/" + dataContext.SelectedMovieId);
                buttonAddActor.Visibility = Visibility.Collapsed;
                buttonShowMovies.Visibility = Visibility.Collapsed;
            }
            else
            {
                showActorsList("http://localhost:24818/api/Actor");
            }
            
        }


        private  void ButtonAddActor_OnClick(object i_Sender, RoutedEventArgs i_E)
        {
            ActorPage page = new ActorPage();
            page.ShowDialog();
            showActorsList("http://localhost:24818/api/Actor");
        }

        private void ButtonUpdateActor_OnClick(object i_Sender, RoutedEventArgs i_E)
        {
            Actor selectedActor = (Actor)gridActors.SelectedItem;
            ActorPage page = new ActorPage();
            page.Actor = selectedActor;
            page.ShowDialog();
            if(dataContext.IsMovieSelected)
            {
                showActorsList("http://localhost:24818/api/Actor/movie-actors/" + dataContext.SelectedMovieId);
            }
            else
            {
                showActorsList("http://localhost:24818/api/Actor");
            }
            
        }

        private async void ButtonDeleteActor_OnClick(object i_Sender, RoutedEventArgs i_E)
        {
            Actor selectedActor = (Actor)gridActors.SelectedItem;
            int actorId = selectedActor.Id;
            HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.DeleteAsync("http://localhost:24818/api/Actor/" + actorId);
            if(message.IsSuccessStatusCode)
            {
                MessageBox.Show("Actor Removed From List Successfully");
                if(dataContext.IsMovieSelected)
                {
                    showActorsList("http://localhost:24818/api/Actor/movie-actors/" + dataContext.SelectedMovieId);
                }
                else
                {
                    showActorsList("http://localhost:24818/api/Actor");
                }
                
            }
            else
            {
                MessageBox.Show("Server Error");
            }
        }

      

        private async void showActorsList(string i_UrlHttpRequest)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.GetAsync(i_UrlHttpRequest);
            if(message.IsSuccessStatusCode)
            {
                string responseString = await message.Content.ReadAsStringAsync();
                List<Actor> list = new List<Actor>();
                try
                {
                    list = JsonConvert.DeserializeObject<List<Actor>>(responseString);
                }
                catch(Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }

                gridActors.ItemsSource = list;
            }
            else
            {
                MessageBox.Show("Server Error-couldnt get actors");
            }
        }

        private async void ButtonShowMovies_OnClick(object i_Sender, RoutedEventArgs i_E)
        {
            if(!m_IsActorSelected)
            {
                MessageBox.Show("Please select an actor");
            }
            else
            {
                HttpClient client = new HttpClient();
                Actor selectedActor = (Actor)gridActors.SelectedItem;
                int actorId = selectedActor.Id;
                HttpResponseMessage message = await client.GetAsync("http://localhost:24818/api/Movie/actor-movies/" + actorId);
                if(message.IsSuccessStatusCode)
                {
                    var jsonResponse =await message.Content.ReadAsStringAsync();
                    List<Movie> moviesList = JsonConvert.DeserializeObject<List<Movie>>(jsonResponse);
                    MoviesOfActorPage page = new MoviesOfActorPage();
                    page.moviesList = moviesList;
                    page.m_ActorId = actorId;
                    page.ShowDialog();
                    m_IsActorSelected = false;
                }
                else
                {
                    MessageBox.Show("Server Error");
                }
            }
        }

        private void GridActors_OnPreviewMouseLeftButtonDown(object i_Sender, MouseButtonEventArgs i_E)
        {
            m_IsActorSelected = true;
        }
    }
}
