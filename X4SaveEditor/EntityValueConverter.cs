using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Xml;
using System.Xml.Linq;

namespace X4SaveEditor
{
    public class EntityValueConverter : IValueConverter
    {

        private readonly Dictionary<string, string> _fromMacroToName = new Dictionary<string, string>();
        private readonly Dictionary<string, string> _fromNameToMacro = new Dictionary<string, string>();

        public EntityValueConverter()
        {
            var appPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            if (!File.Exists(appPath + "\\MacroTranslation.xml")) return;
            var xml = new XmlDocument();
            xml.Load(new Uri(appPath + "\\MacroTranslation.xml").ToString());

            foreach (XElement node in xml)
            {
                try
                {
                    _fromMacroToName.Add(node.Attribute("macro").Value, node.Attribute("name").Value);
                    _fromNameToMacro.Add(node.Attribute("name").Value, node.Attribute("macro").Value);
                }
                catch
                {
                    MessageBox.Show("Duplicate Key:" + node.Attribute("macro").Value);
                }
            }
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var key = "";
            if (value.GetType() == typeof(XmlElement))
            {
                key = ((XmlElement)value).InnerText;
            }
            else
            {
                var val = value as string;
                if (val != null)
                {
                    key = val;
                }
            }

            if (string.IsNullOrWhiteSpace(key))
                return "";

            return _fromMacroToName.ContainsKey(key) ? _fromMacroToName[key] : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return _fromNameToMacro.ContainsKey((string)value) ? _fromNameToMacro[(string)value] : value;
        }
    }
}