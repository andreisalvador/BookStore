using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities
{ 
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
        public bool Active { get; set; }     
    }
}