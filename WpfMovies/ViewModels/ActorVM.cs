using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMovies.Models;

namespace WpfMovies.ViewModels
{
    /*This class serves as
     DataContext for ActorList
    and helps to differentiate between 
    movie - specific actors list
    and a general actors list
     */
    public class ActorVM
    {
        public int SelectedMovieId { get; set; }

        public bool IsMovieSelected  { get; set; }

        public List<Actor> Actors { get; set; }
    }
}
