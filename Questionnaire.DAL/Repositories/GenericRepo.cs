using Common.DTOs;
using Microsoft.EntityFrameworkCore;
using Questionnaire.Data.Domain;
using Questionnaire.Data.Entities;
using Questionnaire.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.Data.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseEntity
    {
        private readonly ApplicationContext _context;
        public GenericRepo(ApplicationContext dbContext)
        {
            _context = dbContext;
        }

        public bool AddRange(List<T> models)
        {
            try
            {
                _context.Set<T>().AddRange(models);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public BaseResult<T> CreateOrUpdate(T model)
        {
            try
            {
                if (model.Id != 0)
                {
                    _context.Set<T>().Update(model);
                }
                else
                {
                    _context.Set<T>().Add(model);
                }

                _context.SaveChanges();

                return new BaseResult<T>() { IsOkay = true, Result = model };
            }
            catch (Exception ex)
            {
                return new BaseResult<T>() { ErrorMessage = ex.Message, IsOkay = false, Result = null };
            }
        }

        public BaseResult<string> Delete(T model)
        {
            try
            {
                _context.Set<T>().Remove(model);

                _context.SaveChanges();

                return new BaseResult<string>() { ErrorMessage = "ok", IsOkay = true };
            }
            catch (Exception ex)
            {
                return new BaseResult<string> { ErrorMessage = ex.Message, IsOkay = false, Result = "Ошибка при удалении записи" };
            }
        }

        public BaseResult<T> GetValue(int id)
        {
            throw new NotImplementedException();
        }

        public BaseResult<IQueryable<T>> ToList()
        {
            try
            {
                var result = _context.Set<T>().AsQueryable();

                return new BaseResult<IQueryable<T>>() { IsOkay = true, Result = result };
            }
            catch (Exception ex)
            {
                return new BaseResult<IQueryable<T>>() { IsOkay = false, ErrorMessage = ex.Message };
            }
        }
    }
}
