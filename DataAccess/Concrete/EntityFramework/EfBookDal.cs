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
                         join a in _context.Authors
                         on b.AuthorId equals a.AuthorId
                         select new BookDetailDto {
                             BookId = b.BookId,
                             UnitsInStock = b.UnitsInStock,
                             PageCount= b.PageCount,
                             Title = b.Title,
                             AuthorName = a.AuthorName,
                             CategoryName = c.CategoryName
                         };

            return result.ToList();
        }

        public List<MostReadBookDto> GetMostReadBooks(int limit)
        {
            if (limit <= 0) return new List<MostReadBookDto>();

            var query = from b in _context.Books
                        join a in _context.Authors on b.AuthorId equals a.AuthorId
                        join c in _context.Categories on b.CategoryId equals c.CategoryId
                        join br in _context.Borrows on b.BookId equals br.BookId into brs
                        select new MostReadBookDto
                        {
                            BookId = b.BookId,
                            CategoryId = b.CategoryId,
                            AuthorId = b.AuthorId,
                            BorrowCount = brs.Count(br => br.IsReturned && !br.IsDeleted),
                            PageCount = b.PageCount,
                            Title = b.Title,
                            ISBN = b.ISBN,
                            CategoryName = c.CategoryName,
                            AuthorName = a.AuthorName
                        };

            return query
                .OrderByDescending(x => x.BorrowCount)
                .ThenBy(x => x.Title)
                .Take(limit)
                .ToList();
        }
    }
}
