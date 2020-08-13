using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.Data.Interfaces
{
    public interface IGenericRepo<T> where T : class
    {
        public BaseResult<T> CreateOrUpdate(T model);
        public bool AddRange(List<T> models);
        public BaseResult<string> Delete(T model);
        public BaseResult<T> GetValue(int id);
        public BaseResult<IQueryable<T>> ToList();
    }
}
