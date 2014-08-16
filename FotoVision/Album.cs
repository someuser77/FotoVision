using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Globalization;
using System.IO;
using System.Xml;
namespace FotoVision
{
	public class Album
	{
		private class Consts
		{
			public const string XmlFileName = "album.xml";
		}
		private string _name;
		private string _path;
		private string _description;
		private string _dateCreated;
		private bool _publish;
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
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
		public string DateCreated
		{
			get
			{
				return this._dateCreated;
			}
			set
			{
				this._dateCreated = value;
			}
		}
		public bool Publish
		{
			get
			{
				return this._publish;
			}
			set
			{
				this._publish = value;
			}
		}
		public string Path
		{
			get
			{
				return this._path;
			}
			set
			{
				this._path = value;
			}
		}
		public Album()
		{
			this.Clear();
		}
		public void Clear()
		{
			this.Name = "";
			this.Description = "";
			this.Publish = true;
			this.DateCreated = "";
			this.Path = "";
		}
		public void WriteXml()
		{
			string text = System.IO.Path.Combine(this.Path, "album.xml");
			try
			{
				XmlTextWriter xmlTextWriter = new XmlTextWriter(text, null);
				xmlTextWriter.Formatting = 1;
				xmlTextWriter.WriteStartElement("metadata");
				xmlTextWriter.WriteStartElement("album");
				xmlTextWriter.WriteStartAttribute("publish", null);
				xmlTextWriter.WriteString(this.Publish.ToString());
				xmlTextWriter.WriteEndAttribute();
				xmlTextWriter.WriteStartAttribute("date", null);
				xmlTextWriter.WriteString(this.DateCreated.Trim());
				xmlTextWriter.WriteEndAttribute();
				xmlTextWriter.WriteStartAttribute("description", null);
				xmlTextWriter.WriteString(this.Description);
				xmlTextWriter.WriteEndAttribute();
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.Flush();
				xmlTextWriter.Close();
			}
			catch (Exception expr_B7)
			{
				ProjectData.SetProjectError(expr_B7);
				Exception ex = expr_B7;
				Global.DisplayError("Error updating the album metadata file.", ex);
				ProjectData.ClearProjectError();
			}
		}
		public void ReadXml(string albumName)
		{
			string location = FileManager.GetLocation(albumName);
			if (!Directory.Exists(location))
			{
				return;
			}
			string text = System.IO.Path.Combine(location, "album.xml");
			this.Name = albumName;
			this.Path = location;
			this.DateCreated = Directory.GetCreationTime(location).ToShortDateString();
			this.Publish = true;
			if (File.Exists(text))
			{
				try
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.Load(text);
					XmlNode firstChild = xmlDocument.DocumentElement.FirstChild;
					this.Description = firstChild.Attributes["description"].Value;
					this.Publish = BooleanType.FromString(firstChild.Attributes["publish"].Value);
					if (Global.ValidateDate(firstChild.Attributes["date"].Value))
					{
						this.DateCreated = DateTime.Parse(firstChild.Attributes["date"].Value, CultureInfo.CurrentCulture).ToShortDateString();
					}
				}
				catch (Exception expr_F1)
				{
					ProjectData.SetProjectError(expr_F1);
					ProjectData.ClearProjectError();
				}
			}
		}
	}
}
