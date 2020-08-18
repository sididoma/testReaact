using System.Collections.Generic;
using System.Linq;

namespace Questionnaire.Data.Interfaces
{
    public interface IGenericRepo<T> where T : class
    {
        public T CreateOrUpdate(T model);
        public bool AddRange(List<T> models);
        public bool Delete(T model);
        public IQueryable<T> ToList();
    }
}
