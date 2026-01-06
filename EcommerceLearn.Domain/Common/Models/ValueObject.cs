namespace EcommerceLearn.Domain.Common.Models;
//You cannot create it directly
public abstract class ValueObject
{
    //why is two object equal
    protected abstract IEnumerable<object?> GetEqualityComponents();
    //compares
    public override bool Equals(object? obj)
    {
        if(obj is not ValueObject other) return false;

        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }
    //searching
    public override int GetHashCode() =>
        GetEqualityComponents()
            .Aggregate(1, (hash, obj) =>
            {
                unchecked { return hash * 23 + (obj?.GetHashCode() ?? 0); }
            });


    public static bool operator ==(ValueObject? a, ValueObject? b) => Equals(a, b);
    public static bool operator !=(ValueObject? a, ValueObject? b) { return !Equals(a, b); }
}