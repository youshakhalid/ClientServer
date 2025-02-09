using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClientSide
{
    public interface ISerialize
    {
        public string ToXML();

        public T FromXML<T>(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringReader reader = new StringReader(xml);
            using (reader)
            {
                return (T)serializer.Deserialize(reader);

            }

        }
    }
}
