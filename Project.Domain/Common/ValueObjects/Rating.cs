using Project.Domain.Common.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Common.ValueObjects;

public sealed class Rating : ValueObject
{
    private Rating()
    {
    }

    public double Value { get; private set; }

    private Rating(double value)
    {
        Value = value;
    }

    public static Rating CreateNew(double val)
    {
        return new Rating(val);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}