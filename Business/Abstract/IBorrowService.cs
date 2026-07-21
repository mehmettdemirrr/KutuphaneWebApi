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
        IDataResult<List<Borrow>> GetAll();
        IDataResult<List<BorrowDetailDto>> GetBorrowDetails();
        IDataResult<Borrow> GetById(int borrowId);
        IResult Loan(Borrow borrow);
        IResult Return(Borrow borrow);
    }
}