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
    public interface IBorrowService
    {
        IDataResult<List<Borrow>> GetAllBorrows();
        IDataResult<List<BorrowDetailDto>> GetBorrowDetails();
        IDataResult<Borrow> GetBorrowById(int borrowId); 
        IDataResult<List<Borrow>> GetActiveBorrowsByBookId(int bookId);
        IDataResult<List<Borrow>> GetActiveBorrowsByUserId(int userId);
        IResult Loan(Borrow borrow);
        IResult Return(Borrow borrow);
    }
}