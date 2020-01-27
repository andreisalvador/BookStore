using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities
{
    public class Book : EntityBase
    {
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public double Price { get; set; }
        public DateTime ReleaseDate { get; set; }

        public bool IsValid()
        {
            return this.Id > 0 && !string.IsNullOrEmpty(this.Title);
        }
    }
}