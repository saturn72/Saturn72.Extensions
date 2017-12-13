﻿using System;
using System.Xml;
using System.Xml.Linq;
using NUnit.Framework;
using Shouldly;

namespace Saturn72.Extensions.Data.Tests
{
    public class XmlExtensionsTests
    {
        [Test]
        public void XmlExtensions_XmlNode_GetAttributeValue()
        {
            //Throws when attribute does not exists
            var doc = new XmlDocument();
            doc.LoadXml("<book>" +
                        "  <title>Oberon's Legacy</title>" +
                        "  <price>5.95</price>" +
                        "</book>");

            // Create a new element node.
            var xmlNode = doc.CreateNode("element", "pages", "");
            xmlNode.InnerText = "290";
            var attName = "att";
            Should.Throw<NullReferenceException>(() => xmlNode.GetAttributeValue(attName));

            var attValue = "val";
            var xmlAtt = doc.CreateAttribute(attName);
            xmlAtt.Value = attValue;
            xmlNode.Attributes.Append(xmlAtt);
            xmlNode.GetAttributeValue(attName).ShouldBe(attValue);
        }

        [Test]
        public void XmlExtensions_XmlNode_GetAttributeValueOrDefault()
        {
            //Throws when attribute does not exists
            var doc = new XmlDocument();
            doc.LoadXml("<book>" +
                        "  <title>Oberon's Legacy</title>" +
                        "  <price>5.95</price>" +
                        "</book>");

            var attName = "att";

            // Create a new element node.
            var xmlNode = doc.CreateNode("element", "pages", "");
            xmlNode.InnerText = "290";
            xmlNode.GetAttributeValueOrDefault(attName).ShouldBe(null);

            var attValue = "val";
            var xmlAtt = doc.CreateAttribute(attName);
            xmlAtt.Value = attValue;
            xmlNode.Attributes.Append(xmlAtt);
            xmlNode.GetAttributeValueOrDefault(attName).ShouldBe(attValue);
        }

        [Test]
        public void XmlExtensions_XElement_GetAttributeValue()
        {
            var xElem1 = new XElement("Child2", "1");

            var attName = "att";
            Should.Throw<NullReferenceException>(() => xElem1.GetAttributeValue(attName));

            var attValue = "val";
            var xAtt = new XAttribute(attName, attValue);
            var xElem2 = new XElement("child2", xAtt);
            xElem2.GetAttributeValue(attName).ShouldBe(attValue);
        }

        [Test]
        public void XmlExtensions_XElement_GetAttributeValueDefault()
        {
            var attName = "att";

            var xElem1 = new XElement("Child2", "1");
            xElem1.GetAttributeValueOrDefault(attName).ShouldBe(null);

            var attValue = "val";
            var xAtt = new XAttribute(attName, attValue);
            var xElem2 = new XElement("child2", xAtt);
            xElem2.GetAttributeValueOrDefault(attName).ShouldBe(attValue);
        }

        [Test]
        public void XmlExtensions_XElement_GetInnerElementValue()
        {
            var innerElemName = "innerElemName";
            var innerElemValue = "innerElemValue";

            //throws On Null source element
            Should.Throw<NullReferenceException>(() => ((XElement)null).GetInnerElementValue(innerElemName));

            //throws On Null inner Element
            var rootElem1 = new XElement("Root", "innerValue");
            Should.Throw<NullReferenceException>(() => rootElem1.GetInnerElementValue(innerElemName));


            var innerElem = new XElement(innerElemName, innerElemValue);
            var rootElem = new XElement("Root", innerElem);

            rootElem.GetInnerElementValue(innerElemName).ShouldBe(innerElemValue);
        }

        [Test]
        public void XmlExtensions_XmlNode_GetInnerElementValue()
        {
            //Throws when attribute does not exists
            var doc = new XmlDocument();
            doc.LoadXml("<book>" +
                        "  <title>Oberon's Legacy</title>" +
                        "  <price>5.95</price>" +
                        "</book>");
            //throws On Null source element
            Should.Throw<NullReferenceException>(() => ((XmlNode)null).GetInnerElementValue("DDD"));

            //throws On Null inner Element
            var xmlNode = doc.SelectSingleNode("book");
            Should.Throw<NullReferenceException>(() => xmlNode.GetInnerElementValue("CCC"));

            xmlNode.GetInnerElementValue("price").ShouldBe("5.95");
        }
    }
}