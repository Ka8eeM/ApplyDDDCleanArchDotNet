using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Common.Errors;

public class Error : IEquatable<Error>
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null.");

    public string Code { get; }
    public string Message { get; }

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }
    public static implicit operator string(Error error) => error.Code;

    public static bool operator ==(Error? a, Error? b)
    {
        //return Equals(a, b);
        if (a is null && b is null) return true;
        if (a is null || b is null) return false;
        if (a is not null && b is not null && a.Equals(b)) return true;
        return false;
    }

    public static bool operator !=(Error? a, Error? b)
    {
        return !(a == b);
    }

    public bool Equals(Error? other)
    {
        if (other == null) return false;
        return Code == other.Code && Message == other.Message;
    }

    public override string ToString()
    {
        return $"{Code}: {Message}";
    }

    public override bool Equals(object obj)
    {
        return obj is Error error && Equals(error);
    }
    public override int GetHashCode()
    {
        return Code.GetHashCode() ^ Message.GetHashCode();
    }
}