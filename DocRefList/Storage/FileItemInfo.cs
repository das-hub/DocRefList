using System;
using System.Xml.Serialization;

namespace DocRefList.Storage
{
    public class FileItemInfo
    {
        [XmlAttribute("Id")]
        public Guid Id { get; set; }
        [XmlAttribute("Name")]
        public string FileName { get; set; }
        [XmlAttribute("MimeType")]
        public string MimeType { get; set; }
    }
}