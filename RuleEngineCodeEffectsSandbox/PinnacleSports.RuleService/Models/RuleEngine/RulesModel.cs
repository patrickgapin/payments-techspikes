using System;
using System.Linq;
using System.Xml.Linq;

namespace PinnacleSports.RuleService.Models.RuleEngine
{
    public class RulesModel
    {
        public RulesModel(XContainer xml, Type modelType)
        {
            XmlRule = xml.Descendants(XNamespace + "rule")
                .FirstOrDefault(element => element.Attribute("type").Value == modelType.AssemblyQualifiedName);
        }

        public XNamespace XNamespace => Properties.Settings.Default.CodeEffectsNamespace;

        public XElement XmlRule { get; }

        public XElement XmlRuleFull => XmlRule?.Parent;

        public string RuleId => XmlRule.Attributes("id").FirstOrDefault()?.Value;

        public string Name => XmlRule.Descendants(XNamespace + "name").FirstOrDefault()?.Value;

        public string Description => XmlRule.Descendants(XNamespace + "description").FirstOrDefault()?.Value;
    }
}