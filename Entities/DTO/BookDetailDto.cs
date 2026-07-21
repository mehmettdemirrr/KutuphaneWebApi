using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class BookDetailDto : IDto
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public short UnitsInStock { get; set; }
    }
}