using Questionnaire.BLL.DTOs;
using System.Collections.Generic;

namespace Questionnaire.BLL.Interfaces
{
    public interface IQAService
    {
        public List<QuestionDTO> GetQuestions();
        public bool SaveAnswers(List<QuestionDTO> request);
    }
}
