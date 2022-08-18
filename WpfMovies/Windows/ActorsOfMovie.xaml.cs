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
using System.Windows.Shapes;
using WpfMovies.Models;
using WpfMovies.ViewModels;

namespace WpfMovies.Windows
{
    /// <summary>
    /// Interaction logic for ActorsOfMovie.xaml
    /// </summary>
    public partial class ActorsOfMovie : Window
    {
        public ActorsOfMovie()
        {
            InitializeComponent();
        }

        public int m_MovieId;


        private void ActorsOfMovie_OnLoaded(object i_Sender, RoutedEventArgs i_E)
        {
            DataContext = new ActorVM() { SelectedMovieId = m_MovieId, IsMovieSelected = true};
        }
    }
}
