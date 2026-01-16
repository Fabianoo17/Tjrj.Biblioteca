using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tjrj.Biblioteca.Application.Commom
{
    public class ServiceResult<T>
    {
        public bool Success { get; private set; }
        public string? ErrorCode { get; private set; }
        public string? ErrorMessage { get; private set; }
        public T? Data { get; private set; }

        public static ServiceResult<T> Ok(T data) => new() { Success = true, Data = data };
        public static ServiceResult<T> Fail(string code, string message) => new()
        {
            Success = false,
            ErrorCode = code,
            ErrorMessage = message
        };
    }
}
