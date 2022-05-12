#nullable disable

namespace MovieProDemo.Models.Database
{
    public class MovieCollection
    {
        public int Id { get; set; } // primary key

        public int CollectionId { get; set; } // foreign key = primary key in Collection table
        public int MovieId { get; set; }      // foreign key = primary key of Movie record

        public int Order { get; set; }        // descriptive property

        public Collection Collection { get; set; } // stores record that matches id of foreign key
        public Movie Movie { get; set; }
    }
}
