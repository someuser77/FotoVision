using Microsoft.VisualBasic.CompilerServices;
using System;
using System.IO;
using System.Xml;
namespace FotoVision
{
	public class PublishData
	{
		private string _fileName;
		private string _title;
		private string _description;
		private string _dateTaken;
		private string _hashCode;
		public string Title
		{
			get
			{
				return this._title;
			}
			set
			{
				this._title = value;
			}
		}
		public string Description
		{
			get
			{
				return this._description;
			}
			set
			{
				this._description = value;
			}
		}
		public string DateTaken
		{
			get
			{
				return this._dateTaken;
			}
			set
			{
				this._dateTaken = value;
			}
		}
		public string FileName
		{
			get
			{
				return this._fileName;
			}
			set
			{
				this._fileName = value;
			}
		}
		public string HashCode
		{
			get
			{
				return this._hashCode;
			}
			set
			{
				this._hashCode = value;
			}
		}
		public PublishData(string fileName, Photo photo)
		{
			this._title = photo.Title;
			this._description = photo.Description;
			this._dateTaken = photo.DateTaken;
			this._fileName = fileName;
			this._hashCode = Hash.ComputeFileHash(photo.PhotoPath);
		}
		public void WriteXml(string photoPath)
		{
			string text = photoPath + ".xml";
			XmlTextWriter xmlTextWriter = new XmlTextWriter(text, null);
			xmlTextWriter.Formatting = 1;
			xmlTextWriter.WriteStartElement("metadata");
			xmlTextWriter.WriteStartElement("photo");
			xmlTextWriter.WriteStartAttribute("filename", null);
			xmlTextWriter.WriteString(this.FileName.Trim());
			xmlTextWriter.WriteEndAttribute();
			xmlTextWriter.WriteStartAttribute("title", null);
			xmlTextWriter.WriteString(this.Title);
			xmlTextWriter.WriteEndAttribute();
			xmlTextWriter.WriteStartAttribute("date", null);
			xmlTextWriter.WriteString(this.DateTaken.Trim());
			xmlTextWriter.WriteEndAttribute();
			xmlTextWriter.WriteStartAttribute("description", null);
			xmlTextWriter.WriteString(this.Description);
			xmlTextWriter.WriteEndAttribute();
			xmlTextWriter.WriteStartAttribute("hashcode", null);
			xmlTextWriter.WriteString(this.HashCode);
			xmlTextWriter.WriteEndAttribute();
			xmlTextWriter.WriteEndElement();
			xmlTextWriter.WriteEndElement();
			xmlTextWriter.Flush();
			xmlTextWriter.Close();
		}
		public static string ReadPhotoHashCode(string photoPath)
		{
			string text = photoPath + ".xml";
			if (File.Exists(text))
			{
				try
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.Load(text);
					XmlNode firstChild = xmlDocument.DocumentElement.FirstChild;
					return firstChild.Attributes.ItemOf("hashcode").get_Value;
				}
				catch (Exception expr_45)
				{
					ProjectData.SetProjectError(expr_45);
					ProjectData.ClearProjectError();
				}
			}
			return "";
		}
	}
}
