using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class MostReadBookDto : IDto
    {
        public int BookId { get; set; }
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
        public int BorrowCount { get; set; }
        public int PageCount { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string CategoryName { get; set; }
        public string AuthorName { get; set; }

    }
}