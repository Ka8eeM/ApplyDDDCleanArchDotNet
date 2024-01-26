

using Project.Domain.Common.Primitives;

namespace Project.Domain.MenuAggregate.ValueObjects;

public sealed class MenuId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private MenuId(Guid value)
    {
        Value = value;
    }



    public static MenuId CreateUnique(Guid guid)
    {
        return new(guid);
    } //  factory method pattern



    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
