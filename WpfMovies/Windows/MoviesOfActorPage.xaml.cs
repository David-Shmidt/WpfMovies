using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using WpfMovies.Models;
using WpfMovies.ViewModels;
using WpfMovies.Views;

namespace WpfMovies.Windows
{
    /// <summary>
    /// Interaction logic for MoviesOfActorPage.xaml
    /// </summary>
    public partial class MoviesOfActorPage : Window
    {
        public MoviesOfActorPage()
        {
            InitializeComponent();
            
        }

        internal List<Movie> moviesList;

        public int m_ActorId;
        private void onLoaded(object i_Sender, RoutedEventArgs args)
        {
            DataContext = new MovieVM(){SelectedActorId = m_ActorId ,IsActorSelected = true , Movies = moviesList};
        }

    }
}
