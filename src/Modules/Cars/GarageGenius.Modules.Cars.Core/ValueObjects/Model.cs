using GarageGenius.Modules.Cars.Core.Exceptions;

namespace GarageGenius.Modules.Cars.Core.ValueObjects;
internal sealed class Model : IEquatable<Model>
{
    public string Value { get; }

    public Model(string model)
    {
        if (string.IsNullOrWhiteSpace(model) || model.Length is > 100 or < 3)
        {
            throw new InvalidModelException(model);
        }

        Value = model;
    }

    public static implicit operator string(Model model)
    {
        return model.Value;
    }

    public static implicit operator Model(string model)
    {
        if (model is null) return null;
        return new Model(model);
    }

    public bool Equals(Model? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override string ToString()
    {
        return Value;
    }

    public override int GetHashCode()
    {
        return Value is not null ? Value.GetHashCode() : 0;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == this.GetType() && Equals((Model?)obj);
    }
}
