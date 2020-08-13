using Questionnaire.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Questionnaire.BLL.Interfaces
{
    public interface IQAService
    {
        public List<QuestionDTO> GetQuestions();
        public bool SaveAnswers(List<QuestionDTO> request);
    }
}
