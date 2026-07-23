using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBookDal : EfEntityRepositoryBase<Book, NorthwindContext>, IBookDal
    {
        public EfBookDal(NorthwindContext context) : base(context)
        {
            
        }

        public List<BookDetailDto> GetBookDetails()
        {
            var result = from b in _context.Books
                         join c in _context.Categories
                         on b.CategoryId equals c.CategoryId
                         select new BookDetailDto { 
                            BookId = b.BookId,
                            Title = b.Title,
                            CategoryName = c.CategoryName,
                            UnitsInStock = b.UnitsInStock
                         };
                
            return result.ToList();
        }
    }
}
