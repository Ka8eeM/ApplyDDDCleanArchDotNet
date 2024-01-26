using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Common.Errors;

public class Result<TValue> : Result
{
    private readonly TValue _value;
    protected internal Result(TValue? value, bool isSuccess, Error error)
        : base(isSuccess, error) => _value = value!;

    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failure result can not be accessed.");

    public static implicit operator Result<TValue>(TValue? value) => Create(value);


    public new static Result<TValue> Failure(Error error) => new(value: default, isSuccess: false, error: error);

    //private static Result<TValue> Create(TValue? value)
    //{
    //    if (value == null)
    //    {
    //        return new Result<TValue>(default, false, Error.NullValue);
    //    }

    //    return new Result<TValue>(value, true, Error.None);
    //}


    //public Result<TResult> Map<TResult>(Func<TValue, TResult> func)
    //{
    //    if (IsFailure)
    //    {
    //        return new Result<TResult>(default, false, Error);
    //    }
    //    return new Result<TResult>(func(Value), true, Error.None);
    //}
}