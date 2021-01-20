using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace EstudosCSharp.XMLDOM
{
    /// <summary>
    /// samples to the former Microsoft implementation of DOM
    /// </summary>
    public static class Samples
    {
        /// <summary>
        ///  the following example uses a typical way to create an XML tree using the Microsoft implementation of DOM, XmlDocument. 
        ///  This style of coding hides the structure of the XML tree
        /// </summary>
        public static void FormerXmlDocument()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement name = doc.CreateElement("Name");
            name.InnerText = "Patrick Hines";
            XmlElement phone1 = doc.CreateElement("Phone");
            phone1.SetAttribute("Type", "Home");
            phone1.InnerText = "206-555-0144";
            XmlElement phone2 = doc.CreateElement("Phone");
            phone2.SetAttribute("Type", "Work");
            phone2.InnerText = "425-555-0145";
            XmlElement street1 = doc.CreateElement("Street1");
            street1.InnerText = "123 Main St";
            XmlElement city = doc.CreateElement("City");
            city.InnerText = "Mercer Island";
            XmlElement state = doc.CreateElement("State");
            state.InnerText = "WA";
            XmlElement postal = doc.CreateElement("Postal");
            postal.InnerText = "68042";
            XmlElement address = doc.CreateElement("Address");
            address.AppendChild(street1);
            address.AppendChild(city);
            address.AppendChild(state);
            address.AppendChild(postal);
            XmlElement contact = doc.CreateElement("Contact");
            contact.AppendChild(name);
            contact.AppendChild(phone1);
            contact.AppendChild(phone2);
            contact.AppendChild(address);
            XmlElement contacts = doc.CreateElement("Contacts");
            contacts.AppendChild(contact);
            doc.AppendChild(contacts);
            _PrintXmlDOM(doc);
        }

        /// <summary>
        /// alternate approach to former XmlDocument DOM, using functional construction with Linq to XML
        /// </summary>
        public static void AlternateToFormer()
        {
            XElement contacts =
                new XElement("Contacts",
                    new XElement("Contact",
                        new XElement("Name", "Patrick Hines"),
                        new XElement("Phone", "206-555-0144",
                            new XAttribute("Type", "Home")),
                        new XElement("phone", "425-555-0145",
                            new XAttribute("Type", "Work")),
                        new XElement("Address",
                            new XElement("Street1", "123 Main St"),
                            new XElement("City", "Mercer Island"),
                            new XElement("State", "WA"),
                            new XElement("Postal", "68042")
                        )
                    )
                );

            Console.WriteLine("Using Linq to XML:");
            Console.WriteLine(contacts.ToString());
        }

        /// <summary>
        /// Example of how to transform an XML Element to an XML Attribute
        /// </summary>
        public static void XmlAttributeOverriding()
        {
            var room = new Classroom();
            room.RoomNumber = "100A";
            XmlAttributeOverrides overrides = new XmlAttributeOverrides();
            XmlAttributes roomNumberAttributes = new XmlAttributes();
            roomNumberAttributes.XmlAttribute = new XmlAttributeAttribute("number");
            overrides.Add(typeof(Classroom), "RoomNumber", roomNumberAttributes);
            XmlSerializer serializar = new XmlSerializer(typeof(Classroom), overrides);

            using (var textWriter = new StringWriter())
            {
                serializar.Serialize(textWriter, room);
                Console.WriteLine(textWriter.ToString());
            }
        }

        private static void _PrintXmlDOM(XmlDocument doc)
        {
            using (var stringWriter = new StringWriter())
            using (var xmlTextWriter = XmlWriter.Create(stringWriter))
            {
                doc.WriteTo(xmlTextWriter);
                xmlTextWriter.Flush();
                var t = stringWriter.GetStringBuilder().ToString();
                Console.WriteLine("Using XmlDocument (DOM):");
                Console.WriteLine(t);
            }
        }

        public class Classroom
        {
            public string RoomNumber { get; set; }
        }
    }
}
