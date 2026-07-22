using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBorrowDal : EfEntityRepositoryBase<Borrow, NorthwindContext>, IBorrowDal
    {
        public List<BorrowDetailDto> GetBorrowDetails()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var result = from borrow in context.Borrows
                             join user in context.Users
                             on borrow.UserId equals user.Id
                             join book in context.Books
                             on borrow.BookId equals book.BookId
                             select new BorrowDetailDto
                             {
                                 BorrowId = borrow.BorrowId,
                                 UserId = user.Id,
                                 BookId = book.BookId,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 BookTitle = book.Title,
                                 BorrowDate = borrow.BorrowDate,
                                 ReturnDate = borrow.ReturnDate,
                                 IsReturned = borrow.IsReturned
                             };

                return result.ToList();
            }
        }
    }
}
