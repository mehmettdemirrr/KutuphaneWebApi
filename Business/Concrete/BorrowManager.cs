using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BorrowManager : IBorrowService
    {
        IBorrowDal _borrowDal;
        IBookService _bookService;
        ICategoryService _categoryService;
        IUserService _userService;
        public BorrowManager(IBorrowDal borrowDal, IBookService bookService, ICategoryService categoryService,
            IUserService userService)
        {
            _borrowDal = borrowDal;
            _bookService = bookService;
            _categoryService = categoryService;
            _userService = userService;
        }

        public IDataResult<List<Borrow>> GetAllBorrows()
        {
            return new SuccessDataResult<List<Borrow>>(_borrowDal.GetAll(), Messages.BorrowsListed);
        }

        public IDataResult<List<BorrowDetailDto>> GetBorrowDetails()
        {
            return new SuccessDataResult<List<BorrowDetailDto>>(_borrowDal.GetBorrowDetails(), Messages.BorrowsListed);
        }

        public IDataResult<Borrow> GetBorrowById(int borrowId)
        {
            return new SuccessDataResult<Borrow>(_borrowDal.Get(b => b.BorrowId == borrowId));
        }

        public IDataResult<List<Borrow>> GetActiveBorrowsByBookId(int bookId)
        {
            return new SuccessDataResult<List<Borrow>>(_borrowDal.GetAll(b => b.BookId == bookId && !b.IsReturned));
        }

        public IDataResult<List<Borrow>> GetActiveBorrowsByUserId(int userId)
        {
            return new SuccessDataResult<List<Borrow>>(_borrowDal.GetAll(b => b.UserId == userId && !b.IsReturned));
        }

        public IResult Loan(Borrow borrow)
        {
            IResult result = BusinessRules.Run(
                CheckIfUserIdCorrect(borrow.UserId),
                CheckIfBookIdCorrect(borrow.BookId),
                CheckIfUserBorrowLimitExceed(borrow.UserId),
                CheckIfBookHasStock(borrow.BookId)
            );

            Book book = _bookService.GetBookById(borrow.BookId).Data;
            book.UnitsInStock--;
            borrow.IsReturned = false;

            _borrowDal.Add(borrow);
            return new SuccessResult(Messages.BorrowSuccessful);
        }

        public IResult Return(Borrow borrow)
        {
            IResult result = BusinessRules.Run(
                CheckIfUserIdCorrect(borrow.UserId),
                CheckIfBookIdCorrect(borrow.BookId)
            );

            Book book = _bookService.GetBookById(borrow.BookId).Data;
            book.UnitsInStock++;
            borrow.IsReturned = true;

            _borrowDal.Update(borrow);
            return new SuccessResult(Messages.BorrowReturned);
        }

        private IResult CheckIfUserIdCorrect(int userId)
        {
            var result = _userService.GetById(userId).Success;
            if(!result)
            {
                return new ErrorResult(Messages.BorrowUserIdIncorrect);
            }
            return new SuccessResult();
        }

        private IResult CheckIfBookIdCorrect(int bookId)
        {
            var result = _bookService.GetBookById(bookId).Data;
            if (result == null)
            {
                return new ErrorResult(Messages.BorrowBookIdIncorrect);
            }
            return new SuccessResult();
        }

        private IResult CheckIfUserBorrowLimitExceed(int userId)
        {
            var result = _borrowDal.GetAll(b => b.UserId == userId && !b.IsReturned);
            if (result.Count > 1)
            {
                return new ErrorResult(Messages.BorrowUserLimitExceed);
            }
            return new SuccessResult();
        }

        private IResult CheckIfBookHasStock(int bookId)
        {
            var result = _bookService.GetBookById(bookId).Data.UnitsInStock;
            if (result == 0)
            {
                return new ErrorResult(Messages.BorrowBookHasNoStock);
            }
            return new SuccessResult();
        }
    }
}
