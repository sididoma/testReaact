using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Questionnaire.BLL.DTOs;
using Questionnaire.BLL.Interfaces;
using Questionnaire.Models;

namespace Questionnaire.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class QuestionnaireController : ControllerBase
    {
        private readonly IQAService _service;

        public QuestionnaireController(IQAService service)
        {
            _service = service;
        }

        /// <summary>
        /// Метод для получения списка вопрос для заполнения анкеты.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(BaseResponse<IEnumerable<QuestionDTO>>), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BaseResponse<string>))]
        public IActionResult Get()
        {
            try
            {
                var result = _service.GetQuestions();

                return Ok(new BaseResponse<IEnumerable<QuestionDTO>>()
                {
                    Code = 0,
                    Message = "Ok",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse<string>()
                {
                    Code = -1,
                    Message = $"Ошибка при получении списка вопросов",
                    Data = ex.Message
                });
            }
        }


        /// <summary>
        /// Сохранение заполненной анкеты.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("SaveAnswers")]
        [ProducesErrorResponseType(typeof(BaseResponse<string>))]
        [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status200OK)]
        public IActionResult SaveAnswers([FromBody]List<QuestionDTO> model)
        {
            try
            {
                var res = _service.SaveAnswers(model);

                return Ok(new BaseResponse<string>()
                {
                    Data = res.ToString(),
                    Code = 0,
                    Message = "Ok"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse<string>
                {
                    Code = -2,
                    Message = "Ошибка при сохранении ответов",
                    Data = ex.Message
                });
            }
        }
    }
}