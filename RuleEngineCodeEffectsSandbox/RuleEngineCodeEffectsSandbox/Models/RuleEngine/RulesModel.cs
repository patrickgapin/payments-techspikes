using System.Xml;

namespace RuleEngineCodeEffectsSandbox.Models.RuleEngine
{
    public class RulesModel
    {
        public RulesModel(XmlDocument xml)
        {
            XmlNamespaceManager = new XmlNamespaceManager(xml.NameTable);
            if (xml.DocumentElement != null) XmlNamespaceManager.AddNamespace("x", xml.DocumentElement.NamespaceURI);
        }

        public XmlNamespaceManager XmlNamespaceManager { get; }
        public XmlNode XmlRule { get; set; }
    }
}