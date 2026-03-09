using System.Text.RegularExpressions;
using Marketplace.Framework;

namespace Marketplace.Domain;

public class ClassifiedAdTitle : Value<ClassifiedAdTitle>
{
    private readonly string _value;
    
    private  ClassifiedAdTitle(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Title cannot be empty", nameof(value));
        
        if(_value.Length > 100)
            throw new ArgumentOutOfRangeException(nameof(value), "Title cannot be longer than 100 characters");

        _value = value;
    }

    public override bool Equals(ClassifiedAdTitle? other)
    {
        if(other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return _value==other._value;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_value);
    }
    
    public static ClassifiedAdTitle FromString(string value) => new ClassifiedAdTitle(value);

    public static ClassifiedAdTitle FromHtml(string htmlTitle)
    {
        var supportedTags= htmlTitle
            .Replace("<i>","*")
            .Replace("</i>","*")
            .Replace("<b>","**")
            .Replace("</b>","**");

        return new ClassifiedAdTitle(Regex.Replace(supportedTags, "<.*?>", string.Empty));
    }
}