using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMovies.Models;

namespace WpfMovies.ViewModels
{
    /*This class serves as
     DataContext for MovieList
    and helps to differentiate between 
    actor - specific movie list
    and a general movie list
     */
    public class MovieVM
    {

        public int SelectedActorId { get; set; }
        public bool IsActorSelected { get; set; }

        public List<Movie> Movies { get; set; }

    }
}
