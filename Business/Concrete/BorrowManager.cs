using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IResult Loan(Borrow borrow)
        {
            IResult result = BusinessRules.Run(
                CheckIfUserCorrect(borrow.UserId),
                CheckIfBookCorrect(borrow.BookId),
                CheckIfUserBorrowLimitExceed(borrow.UserId)
            );

            throw new NotImplementedException();
        }

        public IResult Return(Borrow borrow)
        {
            throw new NotImplementedException();
        }

        private IResult CheckIfUserCorrect(int userId)
        {
            var result = _userService.GetById(userId).Success;
            if(!result)
            {
                throw new NotImplementedException();
            }
            return new SuccessResult();
        }

        private IResult CheckIfBookCorrect(int userId)
        {
            throw new NotImplementedException();
        }

        private IResult CheckIfUserBorrowLimitExceed(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
