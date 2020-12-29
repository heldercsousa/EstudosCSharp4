using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace EstudosCSharp.LinqToXML
{
    /// <summary>
    /// LINQ to XML is implemented on top of XmlReader, and they're tightly integrated. If you're building a system that parses many smaller XML documents, and each one is different, you'd want to take advantage of the productivity improvements that LINQ to XML provides.
    /// </summary>
    public static class Samples
    {
        /// <summary>
        /// show how to get an attribute
        /// </summary>
        public static void LoadDescendantAtrribute()
        {
            XElement purchaseOrder = _LoadXML();

            //IEnumerable<string> partNos = from item in purchaseOrder.Descendants("Item")
            //                              select (string)item.Attribute("PartNumber");
            IEnumerable<string> partNos = purchaseOrder.Descendants("Item").Select(x => (string)x.Attribute("PartNumber"));

            _print(partNos);
        }

        /// <summary>
        /// show how to compute by an element value
        /// </summary>
        public static void LoadDescendantElementGreaterThan()
        {
            XElement purchaseOrder = _LoadXML();

            IEnumerable<XElement> pricesByPartNos = from item in purchaseOrder.Descendants("Item")
                                                    where (int)item.Element("Quantity") * (decimal)item.Element("USPrice") > 100
                                                    orderby (string)item.Element("PartNumber")
                                                    select item;

            
            _print(pricesByPartNos.Select(x => x.ToString()));
        }

        /// <summary>
        /// creates a xmlTree using functional construction
        /// </summary>
        public static void CreateXMLTree()
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

            Console.WriteLine(contacts.ToString());

        }

        #region Programming with Nodes (Linq To XML) -- https://docs.microsoft.com/en-us/dotnet/standard/linq/program-nodes

        /// <summary>
        /// The Parent property contains the parent XElement, not the parent node. Child nodes of XDocument have no parent XElement. Their parent is the document, so the Parent property for those nodes is set to null
        /// </summary>
        public static void ParentPropertyOfChildNodes()
        {
            // produces: 
            // True
            //True
            XDocument doc = XDocument.Parse(@"<!-- a comment --><Root/>");
            Console.WriteLine(doc.Nodes().OfType<XComment>().First().Parent == null);
            Console.WriteLine(doc.Root.Parent == null);
        }

        /// <summary>
        /// Adding text may or may not create a new text node. In a number of XML programming models, adjacent text nodes are always merged. This is sometimes called normalization of text nodes. 
        /// LINQ to XML doesn't normalize text nodes. If you add two text nodes to the same element, it will result in adjacent text nodes. However, if you add content specified as a string rather 
        /// than as an XText node, LINQ to XML might merge the string with an adjacent text node. The following example demonstrates this.
        /// </summary>
        public static void AddTextCreateOrNotANode()
        {
            /// produces 
            /// 1
            /// 1
            /// 2
            XElement xmlTree = new XElement("Root", "Content");

            Console.WriteLine(xmlTree.Nodes().OfType<XText>().Count());

            // this doesn't add a new text node
            xmlTree.Add("new content");
            Console.WriteLine(xmlTree.Nodes().OfType<XText>().Count());

            // this does add a new, adjacent text node
            xmlTree.Add(new XText("more text"));
            Console.WriteLine(xmlTree.Nodes().OfType<XText>().Count());
        }

        /// <summary>
        /// Setting a text node value to the empty string doesn't delete the node. In some XML programming models, text nodes are guaranteed to not contain the empty string. The reasoning is that such a 
        /// text node has no impact on serialization of the XML.However, for the same reason that adjacent text nodes are possible, if you remove the text from a text node by setting its value to the empty 
        /// string, the text node itself won't be deleted.
        /// </summary>
        public static void EmptyNodeValueDoesntDeleteIt()
        {
            /// produces >><<
            XElement xmlTree = new XElement("Root", "Content");
            XText textNode = xmlTree.Nodes().OfType<XText>().First();

            // the following line doesn't cause the removal of the text node.
            textNode.Value = "";

            XText textNode2 = xmlTree.Nodes().OfType<XText>().First();
            Console.WriteLine(">>{0}<<", textNode2);
        }

        /// <summary>
        /// An element with one empty text node is serialized differently from one with no text node.
        /// If an element contains only a child text node that's empty, it's serialized with the long tag syntax: <Child></Child>. If an element contains no child nodes whatsoever, it's serialized with the short tag syntax: <Child />.
        /// </summary>
        public static void NodeTextSerialization()
        {
            ///produces: <Child1></Child1>
            ///          < Child2 />
            XElement child1 = new XElement("Child1",
                new XText("")
            );
            XElement child2 = new XElement("Child2");
            Console.WriteLine(child1);
            Console.WriteLine(child2);
        }

        /// <summary>
        /// Namespaces are attributes in the LINQ to XML tree. Even though namespace declarations have identical syntax to attributes, in some programming interfaces, such as XSLT and XPath, namespace declarations 
        /// aren't considered to be attributes. However, in LINQ to XML, namespaces are stored as XAttribute objects in the XML tree. If you iterate through the attributes of an element that contains a namespace 
        /// declaration, the namespace declaration is one of the items in the returned collection. The IsNamespaceDeclaration property indicates whether an attribute is a namespace declaration.
        /// </summary>
        public static void NamespacesAreAttributes()
        {
            /* produces:
                xmlns="http://www.adventure-works.com"  IsNamespaceDeclaration:True
                xmlns:fc="www.fourthcoffee.com"  IsNamespaceDeclaration:True
                AnAttribute="abc"  IsNamespaceDeclaration:False
             */
            XElement root = XElement.Parse(
            @"<Root
                xmlns='http://www.adventure-works.com'
                xmlns:fc='www.fourthcoffee.com'
                AnAttribute='abc'/>");
                        foreach (XAttribute att in root.Attributes())
                            Console.WriteLine("{0}  IsNamespaceDeclaration:{1}", att, att.IsNamespaceDeclaration);
        }

        /// <summary>
        /// XPath axis methods don't return the child text nodes of XDocument. LINQ to XML allows for child text nodes of an XDocument, as long as the text nodes contain only white space. However, the XPath object
        /// model doesn't include white space as child nodes of a document, so when you iterate through the children of an XDocument using the Nodes axis, white space text nodes will be returned. However, when you 
        /// iterate through the children of an XDocument using the XPath axis methods, white space text nodes won't be returned.
        /// </summary>
        public static void XPathAxis()
        {
            /// produces:
            /// 3
            /// 0
            
            // Create a document with some white space child nodes of the document.
            XDocument root = XDocument.Parse(
            @"<?xml version='1.0' encoding='utf-8' standalone='yes'?>

            <Root/>

            <!--a comment-->
            ", LoadOptions.PreserveWhitespace);

            // count the white space child nodes using LINQ to XML
            Console.WriteLine(root.Nodes().OfType<XText>().Count());

            // count the white space child nodes using XPathEvaluate
            Console.WriteLine(((IEnumerable)root.XPathEvaluate("text()")).OfType<XText>().Count());
        }

        /// <summary>
        /// The XML declaration node of an XDocument is a property, not a child node. When you iterate through the child nodes of an XDocument, you won't see the XML declaration object. 
        /// It's a property of the document, not a child node of it.
        /// </summary>
        public static void DeclarationIsProperty()
        {
            ///prpoduces:
            ///<?xml version="1.0" encoding="utf-8" standalone="yes"?>
            ///< Root />
            ///1
            XDocument doc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Root")
            );

            doc.Save("Temp.xml");
            Console.WriteLine(File.ReadAllText("Temp.xml"));

            // this shows that there is only one child node of the document
            Console.WriteLine(doc.Nodes().Count());
        }
        #endregion

        private static void _print(IEnumerable<string> lst)
        {
            var str = string.Join(",", lst);
            Console.WriteLine(str);
        }

        private static XElement _LoadXML()
        {
            // Load the XML file from our project directory containing the purchase orders
            var filename = "PurchaseOrder.xml";
            var currentDirectory = Directory.GetCurrentDirectory();
            var purchaseOrderFilepath = Path.Combine(currentDirectory, "XMLs", filename);

            XElement purchaseOrder = XElement.Load(purchaseOrderFilepath);
            return purchaseOrder;
        }

    }
}
