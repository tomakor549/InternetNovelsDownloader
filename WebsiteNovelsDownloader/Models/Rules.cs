using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteNovelsDownloader.Models
{
    public class Rules
    {
        public string Name { get; private set; }
        public string Attribute { get; private set; }
        public string? AttributeValue { get; private set; }

        public const string IdRules = "id";
        public const string ClassRules = "class";
        public const string HrefRules = "href";
        public const string ItemRules = "item";

        protected Rules(Attribute attribute, string attributeValue, string name)
        {
            var attributeString = CheckAttribute(attribute);
            if (string.IsNullOrEmpty(attributeString))
            {
                throw new ArgumentException("Rules.Rules(): Unfinished implementation");
            }
            Attribute = attributeString;
            Name = name;
            AttributeValue = attributeValue;
        }

        protected Rules(Attribute attribute, string name)
        {
            var attributeString = CheckAttribute(attribute);
            if (string.IsNullOrEmpty(attributeString))
            {
                throw new ArgumentException("Rules.Rules(): Unfinished implementation");
            }
            Attribute = attributeString;
            Name = name;
        }

        private string? CheckAttribute(Attribute attribute)
        {
            switch (attribute)
            {
                case Models.Attribute.Class:
                    return ClassRules;
                case Models.Attribute.Id:
                    return IdRules;
                case Models.Attribute.Href:
                    return HrefRules;
                case Models.Attribute.Item:
                    return ItemRules;
                default:
                    return null;
            }
        }

        public string ContainsRule() => $"//{Name}[contains(@{Attribute}, \"{AttributeValue}\")]";
        public string AttributeRule() => $"//{Name}[@{Attribute}]";
        public string Manifest() => $"//{Name}/item[@{Attribute}='{AttributeValue}']";
    }

    public class DivRules : Rules
    {
        public DivRules(Attribute attribute, string attributeValue) : base(attribute, attributeValue, "div") { }
    }

    public class HyperlinkRules : Rules
    {
        public string TextValue { get; private set; }
        public HyperlinkRules(Attribute attribute, string textValue) : base(attribute, "a")
        {
            TextValue = textValue;
        }
    }

    public class ManifestRules : Rules
    {
        public ManifestRules(Attribute attribute, string attributeValue) : base(attribute, "manifest") { }
    }

    public enum Attribute
    {
        Class = 1,
        Id = 2,
        Href = 3,
        Item = 4,
    }
}
