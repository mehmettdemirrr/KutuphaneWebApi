using Core.DataAccess;
using Entities.Concrete;
using Entities.DTO;

namespace DataAccess.Abstract
{
    public interface IBorrowDal : IEntityRepository<Borrow>
    {
        List<BorrowDetailDto> GetBorrowDetails();
    }
}
