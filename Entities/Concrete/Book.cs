using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Book : IEntity
    {
        public int BookId { get; set; }
        public int PageCount { get; set; }
        public int UnitsInStock { get; set; }
        public int Status { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
    }
}