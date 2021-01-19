using System;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Text;
using System.Collections.Generic;

namespace Docx2txt
{
    class Program
    {
        static void Main(string[] args)
        {
			XmlNamespaceManager WpNs = new XmlNamespaceManager(new NameTable());
        	WpNs.AddNamespace("w",
				"http://schemas.openxmlformats.org/wordprocessingml/2006/main");
			FileStream DocFile = File.Open(args[0],FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			ZipArchive DocZip = new ZipArchive(DocFile, ZipArchiveMode.Read);
			Stream WpXml = DocZip.GetEntry(
			"word/document.xml").Open();
			IEnumerable<XElement> ParagraphElems = XDocument.Load(WpXml).XPathSelectElements("//w:p", WpNs);
			foreach(XElement pnode in ParagraphElems){
				Console.WriteLine(pnode.Value);
			}
			DocZip.Dispose();
			DocFile.Dispose();
        }
    }
}