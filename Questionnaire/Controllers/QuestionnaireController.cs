using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Questionnaire.BLL.DTOs;
using Questionnaire.BLL.Interfaces;

namespace Questionnaire.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionnaireController : ControllerBase
    {
        private readonly IQAService _service;

        public QuestionnaireController(IQAService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<QuestionDTO> Get()
        {
            var result = _service.GetQuestions();

            return result;
        }


        [HttpPost("SaveAnswers")]
        public IActionResult SaveAnswers([FromBody]List<QuestionDTO> model)
        {
            var res = _service.SaveAnswers(model);

            return Ok(res);
        }
    }
}