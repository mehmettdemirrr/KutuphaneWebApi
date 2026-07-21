using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
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
        public BorrowManager(IBorrowDal borrowDal, IBookService bookService, ICategoryService categoryService)
        {
            _borrowDal = borrowDal;
            _bookService = bookService;
            _categoryService = categoryService;
        }

        public IDataResult<List<Borrow>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<BorrowDetailDto>> GetBorrowDetails()
        {
            throw new NotImplementedException();
        }

        public IDataResult<Borrow> GetById(int borrowId)
        {
            throw new NotImplementedException();
        }

        public IResult Loan(Borrow borrow)
        {
            throw new NotImplementedException();
        }

        public IResult Return(Borrow borrow)
        {
            throw new NotImplementedException();
        }
    }
}
