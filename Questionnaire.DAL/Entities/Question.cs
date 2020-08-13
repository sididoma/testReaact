using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.Data.Entities
{
    public class Question : BaseEntity
    {
        public string QuestionDescription { get; set; }
        public int AnswerTypeId { get; set; }
        public DataType AnswerType { get; set; }
    }
}
