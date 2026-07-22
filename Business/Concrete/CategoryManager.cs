using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;
        IBookDal _bookDal;

        public CategoryManager(ICategoryDal categoryDal, IBookDal bookDal)
        {
            _categoryDal = categoryDal;
            _bookDal = bookDal;
        }

        [CacheAspect]
        public IDataResult<List<Category>> GetAllCategories()
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<Category> GetCategoryById(int categoryId)
        {
            return new SuccessDataResult<Category>(_categoryDal.Get(c => c.CategoryId == categoryId));
        }

        [ValidationAspect(typeof(CategoryValidator))]
        [CacheRemoveAspect("ICategoryService.Get")]
        public IResult Add(Category category)
        {
            var result = BusinessRules.Run(
                CheckIfCategoryNameExist(category.CategoryName,category.CategoryId)    
            );

            if (result != null)
            {
                return result;
            }

            _categoryDal.Add(category);
            return new SuccessResult(Messages.CategoryAdded);
        }

        [ValidationAspect(typeof(CategoryValidator))]
        [CacheRemoveAspect("ICategoryService.Get")]
        public IResult Update(Category category)
        {
            var result = BusinessRules.Run(
                CheckIfCategoryNameExist(category.CategoryName,category.CategoryId)
            );

            if (result != null)
            {
                return result;
            }

            _categoryDal.Update(category);
            return new SuccessResult(Messages.CategoryUpdated);
        }

        [CacheRemoveAspect("ICategoryService.Get")]
        public IResult Delete(Category category)
        {
            var result = BusinessRules.Run(
                CheckIfCategoryHasBook(category.CategoryId)
            );

            if (result != null)
            {
                return result;
            }

            _categoryDal.Delete(category);
            return new SuccessResult(Messages.CategoryDeleted);
        }

        private IResult CheckIfCategoryNameExist(string categoryName, int categoryId)
        {
            var result = _categoryDal.GetAll(c => c.CategoryName == categoryName && c.CategoryId != categoryId).Any();
            if (result)
            {
                return new ErrorResult(Messages.CategoryNameAlreadyExist);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryHasBook(int  categoryId)
        {
            var result = _bookDal.GetAll(b => b.CategoryId == categoryId).Any();
            if (result)
            {
                return new ErrorResult(Messages.DeletedCategoryHasBooks);
            }
            return new SuccessResult();
        }
    }
}