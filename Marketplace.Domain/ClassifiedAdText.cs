using System.Text.RegularExpressions;
using Marketplace.Framework;

namespace Marketplace.Domain;

public class ClassifiedAdText : Value<ClassifiedAdText>
{
    public string Value { get; }
    
    public  ClassifiedAdText(string value)
    => Value = value;
    

    public override bool Equals(ClassifiedAdText? other)
    {
        if(other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value==other.Value;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value);
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
    
    public static implicit operator string(ClassifiedAdText text) => text.Value;
}