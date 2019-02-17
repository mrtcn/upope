using System;
using System.Xml.Serialization;

namespace Upope.ServiceBase.ServiceBase.Models
{
    public enum Culture
    {
        [Culture(Name = "Türkçe", CultureName = "tr-TR", ShortName = "tr", IsDefault = true, HomePageTitle = "Ana Sayfa", Url = "/"), XmlEnum("1")]
        Turkish = 1,
        [Culture(Name = "İngilizce", CultureName = "en-US", ShortName = "en", IsDefault = false, HomePageTitle = "Home Page", Url = "/en"), XmlEnum("2")]
        English = 2
    }

    public class CultureAttribute : Attribute
    {
        public string Url { get; set; }
        public string CultureName { get; set; }
        public bool IsDefault { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string HomePageTitle { get; set; }
    }
}
