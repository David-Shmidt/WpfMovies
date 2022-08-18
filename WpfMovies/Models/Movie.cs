using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMovies.Models
{
    /*This class is used
     to populate the dataGrid*/
    public class Movie
    {
        public int Id { get; set; }

        public int GenreId { get; set; }

        public int AgeRateId { get; set; }
        public string Name { get; set; }

        public string GenreName { get; set; }

        public string AgeRate { get; set; }

        public string Budget { get; set; }

        public string BoxOffice { get; set; }
    }

    /*this class is used to
     add entities to the db*/
    public class MovieDbModel
    {
        public string Name { get; set; }

        public int DirectorId { get; set; }
        public int GenreId { get; set; }
        public int AgeRateId { get; set; }
        public string Budget { get; set; }

        public string BoxOffice { get; set; }
    }
}
