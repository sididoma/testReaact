using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Questionnaire.BLL.DTOs
{
    public class QuestionDTO
    {
        public int QuestionId { get; set; }
        public string Question { get; set; }
        public int TypeId { get; set; }
        public string Type { get; set; }
        public string[] EnumDescriptions { get; set; }
        public string Answer { get; set; }
    }
}
