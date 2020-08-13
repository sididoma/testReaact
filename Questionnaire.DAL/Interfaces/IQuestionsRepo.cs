using Questionnaire.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Questionnaire.DAL.Interfaces
{
    public interface IQuestionsRepo
    {
        public IQueryable<Question> GetQuestions();
    }
}
