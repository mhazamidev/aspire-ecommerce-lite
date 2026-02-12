using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnlineShop.Shared.Response
{
    public class Result<T>
    {
        public bool ForceLogout { get; set; }
        public bool Succeeded { get; set; }
        public T? Data { get; set; }
        public string? ErrorMessage { get; set; }

        public static Result<T> Success(T data)
        {
            return new Result<T> { Data = data, Succeeded = true };
        }
        public static Result<T> Fail(string errorMessage = "")
        {
            return new Result<T> { ErrorMessage = errorMessage };
        }

        public static Result<T> Deserialize(string json)
        {
            try
            {
                return JsonSerializer.Deserialize<Result<T>>(json);
            }
            catch (Exception)
            {
                var result = JsonSerializer.Deserialize<BaseResult>(json);
                return new Result<T>
                {
                    ErrorMessage = result?.ErrorMessage
                };
            }
        }
    }

    public class BaseResult : Result<int?>
    {
        public static BaseResult Success()
        {
            return new BaseResult { Succeeded = true };
        }
        public new static BaseResult Fail(string errorMessage = "")
        {
            return new BaseResult { ErrorMessage = errorMessage };
        }
        public new static BaseResult Deserialize(string json)
        {
            return JsonSerializer.Deserialize<BaseResult>(json);
        }
    }
}
