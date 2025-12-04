using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingList.src.Domain
{
    internal class Book
    {
        public int Id { get; init; }
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public int Year { get; set; }
        public int Pages { get; set; }
        public string Genre { get; set; } = "";
        public bool Finished { get; private set; }
        public double Rating { get; private set; }

        public void MarkFinished() => Finished = true;

        public void SetRating(double rating)
        {
            if (rating < 0 || rating > 5)
                throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be between 0 and 5.");

            Rating = rating;
        }
    }
}
