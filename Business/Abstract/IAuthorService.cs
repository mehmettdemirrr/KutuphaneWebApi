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
    public interface IAuthorService
    {
        IDataResult<List<Author>> GetAll();
        IDataResult<List<Author>> GetAllByAuthorId(int id);
        IDataResult<List<AuthorDetailDto>> GetAuthorDetails();
        IDataResult<Author> GetById(int authorId);
        IResult Add(Author author);
        IResult Update(Author author);
        IResult Delete(Author author);
    }
}