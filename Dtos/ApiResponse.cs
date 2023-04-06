using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace admin.Dtos
{
    public class ApiResponse
    {
        public HttpStatusCode ResponseCode { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } = String.Empty;
        public object? Data { get; set; }
        public Exception? Exception { get; set; }
    }


    public class Exception
    {
        public string Message { get; set; } = String.Empty;
        public string ExceptionMessage { get; set; } = String.Empty;
        public string ExceptionType { get; set; } = String.Empty;
        public string StackTrace { get; set; } = String.Empty;
        public object? InnerException { get; set; }
    }

}