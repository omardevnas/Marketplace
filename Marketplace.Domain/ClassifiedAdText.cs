using System.Text.RegularExpressions;
using Marketplace.Framework;

namespace Marketplace.Domain;

public class ClassifiedAdText : Value<ClassifiedAdText>
{
    private readonly string _value;
    
    private  ClassifiedAdText(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Title cannot be empty", nameof(value));
        
        if(value.Length > 100)
            throw new ArgumentOutOfRangeException(nameof(value), "Title cannot be longer than 100 characters");

        _value = value;
    }

    public override bool Equals(ClassifiedAdText? other)
    {
        if(other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return _value==other._value;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_value);
    }
    
    public static ClassifiedAdText FromString(string value) => new ClassifiedAdText(value);

    public static ClassifiedAdText FromHtml(string htmlTitle)
    {
        var supportedTags= htmlTitle
            .Replace("<i>","*")
            .Replace("</i>","*")
            .Replace("<b>","**")
            .Replace("</b>","**");

        return new ClassifiedAdText(Regex.Replace(supportedTags, "<.*?>", string.Empty));
    }
    
    public static implicit operator string(ClassifiedAdText text) => text._value;
}