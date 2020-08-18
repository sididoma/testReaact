using Microsoft.AspNetCore.Mvc;
using Moq;
using Questionnaire.BLL.DTOs;
using Questionnaire.BLL.Interfaces;
using Questionnaire.Controllers;
using Questionnaire.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Questionnaire.Test.Controllers
{
    public class QuestionnaireControllerTests
    {
        private readonly IQAService _service;
        private List<QuestionDTO> _data;
        public QuestionnaireControllerTests()
        {
            _data = new List<QuestionDTO>();
            _data.Add(new QuestionDTO()
            {
                TypeId = 1,
                Answer = "Test",
                Question = "Test"
            });
            var serviceMock = new Mock<IQAService>();
            serviceMock.Setup(srv => srv.GetQuestions()).Returns(_data);

            serviceMock.Setup(srv => srv.SaveAnswers(_data)).Returns(true);

            _service = serviceMock.Object;
        }
        [Fact]
        public void GetQuestions()
        {
            // Arrange
            var controller = new QuestionnaireController(_service);

            // Act
            var result = controller.Get();

            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<BaseResponse<IEnumerable<QuestionDTO>>>(viewResult.Value);
            Assert.Equal(model.Data, _data);
        }

        [Fact]
        public void SaveAnswers()
        {
            // Arrange
            var controller = new QuestionnaireController(_service);

            // Act
            var result = controller.SaveAnswers(_data);

            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            _ = Assert.IsAssignableFrom<BaseResponse<string>>(viewResult.Value);
        }
    }
}
