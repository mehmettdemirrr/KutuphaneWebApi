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
        public List<BookDetailDto> GetBookDetails()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var result = from b in context.Books
                             join c in context.Categories
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
}
