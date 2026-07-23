using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Book : IEntity, ISoftDelete
    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public int PageCount { get; set; }
        public int UnitsInStock { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public bool IsDeleted { get; set; }
    }
}