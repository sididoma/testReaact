using Questionnaire.Data.Domain;
using Questionnaire.Data.Entities;
using Questionnaire.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;

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
            _context.Set<T>().AddRange(models);
            _context.SaveChanges();
            return true;
        }

        public T CreateOrUpdate(T model)
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

            return model;
        }

        public bool Delete(T model)
        {
            _context.Set<T>().Remove(model);

            _context.SaveChanges();

            return true;
        }

        public IQueryable<T> ToList()
        {
            var result = _context.Set<T>().AsQueryable();

            return result;
        }
    }
}
