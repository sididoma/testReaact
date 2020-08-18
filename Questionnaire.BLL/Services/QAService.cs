using Questionnaire.BLL.DTOs;
using Questionnaire.BLL.Interfaces;
using Questionnaire.DAL.Interfaces;
using Questionnaire.Data.Entities;
using Questionnaire.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Questionnaire.BLL.Services
{
    public class QAService : IQAService
    {
        private const string _separator = "***";
        private readonly IGenericRepo<Question> _qaRepo;
        private readonly IGenericRepo<User> _userRepo;
        private readonly IQuestionsRepo _questionRepo;
        private readonly IGenericRepo<Answer> _aRepo;

        public QAService(IGenericRepo<Question> qaRepo, IGenericRepo<User> userRepo, IQuestionsRepo questionRepo, IGenericRepo<Answer> aRepo)
        {
            _qaRepo = qaRepo;
            _userRepo = userRepo;
            _questionRepo = questionRepo;
            _aRepo = aRepo;
        }

        public List<QuestionDTO> GetQuestions()
        {
            var dbSet = _questionRepo.GetQuestions();

            var result = dbSet.Select(select => new QuestionDTO
            {
                Type = select.AnswerType.Type,
                TypeId = select.AnswerTypeId,
                QuestionId = select.Id,
                Question = select.QuestionDescription,
                EnumDescriptions = select.AnswerType.Type == "enum" ? select.AnswerType.EnumDescription.Split(_separator, StringSplitOptions.None) : null,
                Answer = string.Empty
            }).ToList();

            return result;
        }

        public bool SaveAnswers(List<QuestionDTO> request)
        {
            var user = new User() { CreateDateTime = DateTime.Now };
            var saveUserRes = _userRepo.CreateOrUpdate(user);

            var answers = request.Select(s => new Answer()
            {
                QuestionId = s.QuestionId,
                UserId = saveUserRes.Id,
                AnswerDesc = s.Answer
            }).ToList();

            var res = _aRepo.AddRange(answers);

            return res;
        }
    }
}
