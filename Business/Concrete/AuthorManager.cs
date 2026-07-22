using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.DataAccess;
using Core.Utilities.Business;
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
    public class AuthorManager : IAuthorService
    {
        IAuthorDal _authorDal;
        IBookDal _bookDal;
        public AuthorManager(IAuthorDal authorDal, IBookDal bookDal)
        {
            _authorDal = authorDal;
            _bookDal = bookDal;
        }

        [CacheAspect]
        public IDataResult<List<Author>> GetAllAuthors()
        {
            return new SuccessDataResult<List<Author>>(_authorDal.GetAll(), Messages.AuthorsListed);
        }

        [CacheAspect]
        public IDataResult<Author> GetAuthorById(int authorId)
        {
            return new SuccessDataResult<Author>(_authorDal.Get(a => a.AuthorId == authorId));
        }

        [CacheAspect]
        public IDataResult<List<Book>> GetBooksByAuthorId(int authorId)
        {
            return new SuccessDataResult<List<Book>>(_bookDal.GetAll(b => b.AuthorId == authorId));
        }

        [ValidationAspect(typeof(AuthorValidator))]
        [CacheRemoveAspect("IAuthorService.Get")]
        public IResult Add(Author author)
        {
            IResult result = BusinessRules.Run(
                CheckIfAuthorNameExist(author.AuthorName,author.AuthorId)
            );

            if (result != null)
            {
                return result;
            }

            _authorDal.Add(author);
            return new SuccessResult(Messages.AuthorAdded);
        }

        [ValidationAspect(typeof(AuthorValidator))]
        [CacheRemoveAspect("IAuthorService.Get")]
        public IResult Update(Author author)
        {
            IResult result = BusinessRules.Run(
                CheckIfAuthorNameExist(author.AuthorName,author.AuthorId)
            );

            if (result != null)
            {
                return result;
            }

            _authorDal.Update(author);
            return new SuccessResult(Messages.AuthorUpdated);
        }

        [ValidationAspect(typeof(AuthorValidator))]
        [CacheRemoveAspect("IAuthorService.Get")]
        public IResult Delete(Author author)
        {
            IResult result = BusinessRules.Run(
                CheckIfAuthorHasBooks(author.AuthorId)
            );

            if (result != null)
            {
                return result;
            }

            _authorDal.Delete(author);
            return new SuccessResult(Messages.AuthorDeleted);
        }

        private IResult CheckIfAuthorNameExist(string authorName, int authorId)
        {
            var result = _authorDal.GetAll(a => a.AuthorName == authorName && a.AuthorId != authorId).Any();
            if (result)
            {
                return new ErrorResult(Messages.AuthorNameAlreadyExist);
            }
            return new SuccessResult();
        }

        private IResult CheckIfAuthorHasBooks(int authorId)
        {
            var result = _bookDal.GetAll(b => b.AuthorId == authorId).Any();
            if (result)
            {
                return new ErrorResult(Messages.DeletedAuthorHasBooks);
            }
            return new SuccessResult();
        }
    }
}
