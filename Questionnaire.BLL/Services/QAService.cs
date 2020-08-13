using Common.DTOs;
using Questionnaire.BLL.DTOs;
using Questionnaire.BLL.Interfaces;
using Questionnaire.DAL.Interfaces;
using Questionnaire.Data.Entities;
using Questionnaire.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            var result = dbSet.Select(s => new QuestionDTO
            {
                Type = s.AnswerType.Type,
                TypeId = s.AnswerTypeId,
                QuestionId = s.Id,
                Question = s.QuestionDescription,
                EnumDescriptions = s.AnswerType.Type == "string" ? s.AnswerType.EnumDescription.Split(_separator, StringSplitOptions.None) : null
            }).ToList();

            return result;
        }

        public bool SaveAnswers(List<QuestionDTO> request)
        {
            var user = new User() { CreateDateTime = DateTime.Now };
            var saveUserRes = _userRepo.CreateOrUpdate(user);

            if (!saveUserRes.IsOkay)
                return false;



            var answers = request.Select(s => new Answer()
            {
                QuestionId = s.QuestionId,
                UserId = saveUserRes.Result.Id,
                AnswerDesc = s.Answer
            }).ToList();

            var res = _aRepo.AddRange(answers);

            return res;
        }
    }
}
