using Microsoft.EntityFrameworkCore;
using Questionnaire.DAL.Interfaces;
using Questionnaire.Data.Domain;
using Questionnaire.Data.Entities;
using System.Linq;

namespace Questionnaire.DAL.Repositories
{
    public class QuestionsRepo : IQuestionsRepo
    {
        private readonly ApplicationContext _context;
        public QuestionsRepo(ApplicationContext dbContext)
        {
            _context = dbContext;
        }
        public IQueryable<Question> GetQuestions()
        {
            return _context.Questions.Include(x => x.AnswerType).AsQueryable();
        }
    }
}
