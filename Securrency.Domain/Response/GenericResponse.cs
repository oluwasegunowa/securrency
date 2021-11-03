using System;
using System.Collections.Generic;
using System.Text;

namespace Securrency.Domain.Response
{
    public class BaseResponse
    {
        public string Message { get; set; }
        /// <summary>
        /// This is used to return application defined codes to callers where necessary
        /// </summary>
        public string StatusCode { get; set; }
    }

  public  class GenericResponse<T>: BaseResponse
    {
      
        public bool IsSuccessful { get; set; }
        public T ResponseModel { get; set; }
      
    }
}
