using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.Data.Entities
{
    public class DataType : BaseEntity
    {
        public string Type { get; set; }
        public string EnumDescription { get; set; }
    }
}
