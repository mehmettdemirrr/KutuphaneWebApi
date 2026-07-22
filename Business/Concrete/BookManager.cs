using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BookManager : IBookService
    {
        IBookDal _bookDal;
        ICategoryService _categoryService;
        IBorrowDal _borrowDal;
        IAuthorService _authorService;

        public BookManager(IBookDal bookDal, ICategoryService categoryService, IBorrowDal borrowDal, IAuthorService authorService)
        {
            _bookDal = bookDal;
            _categoryService = categoryService;
            _borrowDal = borrowDal;
            _authorService = authorService;
        }

        [CacheAspect]
        public IDataResult<List<Book>> GetAllBooks()
        {
            return new SuccessDataResult<List<Book>>(_bookDal.GetAll(), Messages.BooksListed);
        }

        [CacheAspect]
        public IDataResult<List<Book>> GetBooksByAuthorId(int authorId)
        {
            return new SuccessDataResult<List<Book>>(_bookDal.GetAll(b => b.AuthorId == authorId));
        }

        [CacheAspect]
        public IDataResult<List<Book>> GetBooksByCategoryId(int categoryId)
        {
            return new SuccessDataResult<List<Book>>(_bookDal.GetAll(b => b.CategoryId == categoryId));
        }

        [CacheAspect]
        public IDataResult<List<BookDetailDto>> GetBookDetails()
        {
            return new SuccessDataResult<List<BookDetailDto>>(_bookDal.GetBookDetails());
        }

        [CacheAspect]
        public IDataResult<Book> GetBookById(int bookId)
        {
            return new SuccessDataResult<Book>(_bookDal.Get(p => p.BookId == bookId));
        }

        //[SecuredOperation("book.add,admin")]
        [ValidationAspect(typeof(BookValidator))]
        [CacheRemoveAspect("IBookService.Get")]
        //[PerformanceAspect(5)]
        public IResult Add(Book book)
        {
            IResult result = BusinessRules.Run(
                CheckIfTitleExist(book.Title,book.BookId),
                CheckIfISBNExist(book.ISBN,book.BookId),
                CheckIfAuthorIdCorrect(book.AuthorId),
                CheckIfCategoryIdCorrect(book.CategoryId)
            );

            if (result != null)
            {
                return result;
            }

            _bookDal.Add(book);
            return new SuccessResult(Messages.BookAdded);
        }

        [ValidationAspect(typeof(BookValidator))]
        [CacheRemoveAspect("IBookService.Get")]
        public IResult Update(Book book)
        {
            IResult result = BusinessRules.Run(
                CheckIfTitleExist(book.Title,book.BookId),
                CheckIfISBNExist(book.ISBN,book.BookId),
                CheckIfAuthorIdCorrect(book.AuthorId),
                CheckIfCategoryIdCorrect(book.CategoryId)
            );

            if (result != null)
            {
                return result;
            }

            _bookDal.Update(book);
            return new SuccessResult(Messages.BookUpdated);
        }

        [ValidationAspect(typeof(BookValidator))]
        [CacheRemoveAspect("IBookService.Get")]
        public IResult Delete(Book book)
        {
            IResult result = BusinessRules.Run(CheckIfDeletedBookIsBorrowed(book.BookId));

            if (result != null)
            {
                return result;
            }

            _bookDal.Delete(book);
            return new SuccessResult(Messages.BookDeleted);
        }

        private IResult CheckIfTitleExist(string title, int bookId)
        {
            var result = _bookDal.GetAll(b => b.Title == title && b.BookId != bookId).Any();
            if (result)
            {
                return new ErrorResult(Messages.BookNameAlreadyExist);
            }
            return new SuccessResult();
        }

        private IResult CheckIfDeletedBookIsBorrowed(int bookId)
        {
            var result = _borrowDal.GetAll(b => b.BookId == bookId && !b.IsReturned).Any();
            if (result)
            {
                return new ErrorResult(Messages.BookCategoryIdIncorrect);
            }
            return new SuccessResult();
        }

        private IResult CheckIfISBNExist(string ISBN, int bookId)
        {
            var result = _bookDal.GetAll(b => b.Title == ISBN && b.BookId != bookId).Any();
            if(result)
            {
                return new ErrorResult(Messages.ISBNAlreadyExist);
            }
            return new SuccessResult();
        }

        private IResult CheckIfAuthorIdCorrect(int authorId)
        {
            var result = _authorService.GetAuthorById(authorId).Data;
            if (result == null)
            {
                return new ErrorResult(Messages.BookAuthorIdIncorrect);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryIdCorrect(int categoryId)
        {
            var result = _categoryService.GetCategoryById(categoryId).Data;
            if (result == null)
            {
                return new ErrorResult(Messages.BookCategoryIdIncorrect);
            }
            return new SuccessResult();
        }
    }
}