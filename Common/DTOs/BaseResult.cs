using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs
{
    public class BaseResult<T> where T : class
    {
        public bool IsOkay { get; set; }
        public string ErrorMessage { get; set; }
        public T Result { get; set; }
    }
}
