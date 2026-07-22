using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBookService
    {
        IDataResult<List<Book>> GetAllBooks();
        IDataResult<List<Book>> GetBooksByAuthorId(int authorId);
        IDataResult<List<Book>> GetBooksByCategoryId(int categoryId);
        IDataResult<List<BookDetailDto>> GetBookDetails();
        IDataResult<Book> GetBookById(int bookId);
        IResult Add(Book book);
        IResult Update(Book book);
        IResult Delete(Book book);
    }
}