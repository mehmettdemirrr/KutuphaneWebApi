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
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BookManager : IBookService
    {
        IBookDal _bookDal;
        ICategoryService _categoryService;
        IBorrowService _borrowService;

        public BookManager(IBookDal bookDal, ICategoryService categoryService, IBorrowService borrowService)
        {
            _bookDal = bookDal;
            _categoryService = categoryService;
            _borrowService = borrowService;
        }

        [CacheAspect]
        public IDataResult<List<Book>> GetAll()
        {
            return new SuccessDataResult<List<Book>>(_bookDal.GetAll(), Messages.BooksListed);
        }

        public IDataResult<List<Book>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Book>>(_bookDal.GetAll(p => p.CategoryId == id));
        }

        public IDataResult<List<BookDetailDto>> GetBookDetails()
        {
            return new SuccessDataResult<List<BookDetailDto>>(_bookDal.GetBookDetails());
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<Book> GetById(int bookId)
        {
            return new SuccessDataResult<Book>(_bookDal.Get(p => p.BookId == bookId));
        }

        [SecuredOperation("book.add,admin")]
        [ValidationAspect(typeof(BookValidator))]
        [CacheRemoveAspect("IBookService.Get")]
        [PerformanceAspect(5)]
        public IResult Add(Book book)
        {
            IResult result = BusinessRules.Run(CheckIfTitleExist(book.Title));

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
            IResult result = BusinessRules.Run(CheckIfTitleExist(book.Title));

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
            IResult result = BusinessRules.Run(CheckIfBookIsBorrowed(book.BookId));

            if (result != null)
            {
                return result;
            }

            _bookDal.Delete(book);
            return new SuccessResult(Messages.BookDeleted);
        }

        private IResult CheckIfTitleExist(string title)
        {
            var result = _bookDal.GetAll(p => p.Title == title).Any();
            if (result)
            {
                return new ErrorResult(Messages.TitleAlreadyExistError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfBookIsBorrowed(int bookId)
        {
            var result = _bookDal.Get(b => b.BookId & b.Status).Any();
            if (result)
            {
                return new ErrorResult(Messages.TitleAlreadyExistError);
            }
            return new SuccessResult();
        }
    }
}