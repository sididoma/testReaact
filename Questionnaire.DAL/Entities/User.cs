using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.Data.Entities
{
    public class User : BaseEntity
    {
        public DateTime CreateDateTime { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
