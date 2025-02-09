using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClientSide
{
    public class Users : ISerialize
    {
        public Users()
        {
            
        }
        public string? name { get; set; }
        public string? designation{ get; set; }

        public string ToXML()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Users));
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, this);
                return writer.ToString();
            }
        }
    }
}
