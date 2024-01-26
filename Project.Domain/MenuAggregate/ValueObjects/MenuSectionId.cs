using Project.Domain.Common.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.MenuAggregate.ValueObjects;

public sealed class MenuSectionId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private MenuSectionId(Guid value)
    {
        Value = value;
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static MenuSectionId CreateUnique(Guid value)
    {
        return new MenuSectionId(value);
    }
}
