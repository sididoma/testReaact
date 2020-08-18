using Questionnaire.Data.Entities;
using System.Linq;

namespace Questionnaire.DAL.Interfaces
{
    public interface IQuestionsRepo
    {
        public IQueryable<Question> GetQuestions();
    }
}
