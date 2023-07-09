using iText.Commons.Utils;
using iText.Forms.Form.Element;
using iText.StyledXmlParser.Jsoup.Nodes;
using Microsoft.Extensions.DependencyModel;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IText7Extractor.Models
{
    [XmlRoot(ElementName = "Document", Namespace = "")]
    public class Document
    {
        [XmlElement("Page")]
        public List<Page>? Pages { get; set; }

        [XmlArray("InteractionFields")]
        [XmlArrayItem("FormField")]
        public List<InteractionField>? InteractionFields { get; set; }
    }

    public class Page
    {
        [XmlElement("TextType")]
        public List<TextType>? TextTypes { get; set; }
    }

    public class TextType
    {
        [XmlAttribute("fontSize")]
        public int FontSize { get; set; }

        [XmlElement("text")]
        public List<TextNode>? TextNodes { get; set; }
    }

    public class TextNode
    {
        [XmlText]
        public string? Text { get; set; }
    }

    public class InteractionField
    {
        [XmlAttribute("FieldType")]
        public string? FieldType { get; set; }

        [XmlText]
        public string? Value { get; set; }
        public string? lable { get; set; }
    }
}
