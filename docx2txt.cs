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
		static void PrintText(ZipArchive DocZip, string entry_name){
			XmlNamespaceManager WpNs = new XmlNamespaceManager(new NameTable());
        	WpNs.AddNamespace("w",
				"http://schemas.openxmlformats.org/wordprocessingml/2006/main");
			ZipArchiveEntry wpxml_part = DocZip.GetEntry(entry_name);
			if(wpxml_part==null)return;
			Stream xml_stream = wpxml_part.Open();
			IEnumerable<XElement> ParagraphElems = XDocument.Load(xml_stream).XPathSelectElements("//w:p", WpNs);
			foreach(XElement pnode in ParagraphElems){
				Console.WriteLine(pnode.Value);
			}
			xml_stream.Dispose();
		}
        static void Main(string[] args)
        {
			
			FileStream DocFile = File.Open(args[0],FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			ZipArchive DocZip = new ZipArchive(DocFile, ZipArchiveMode.Read);
			PrintText(DocZip, "word/comments.xml");
			PrintText(DocZip, "word/document.xml");
			PrintText(DocZip, "word/footnotes.xml");
			PrintText(DocZip, "word/endnotes.xml");
			DocZip.Dispose();
			DocFile.Dispose();
        }
    }
}