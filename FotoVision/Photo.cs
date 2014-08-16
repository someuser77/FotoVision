using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;
namespace FotoVision
{
	public class Photo : IComparable
	{
		private string _photoName;
		private string _photoPath;
		private string _thumbnailPath;
		private string _thumbnailCode;
		private string _metadataPath;
		private string _title;
		private string _description;
		private string _dateTaken;
		public string Title
		{
			get
			{
				return StringType.FromObject(Interaction.IIf(this._title == null || StringType.StrCmp(this._title, "", false) == 0, this._photoName, this._title));
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
		public string PhotoName
		{
			get
			{
				return this._photoName;
			}
			set
			{
				this._photoName = value;
			}
		}
		public string PhotoPath
		{
			get
			{
				return this._photoPath;
			}
			set
			{
				this._photoPath = value;
			}
		}
		public string ThumbnailPath
		{
			get
			{
				return this._thumbnailPath;
			}
			set
			{
				this._thumbnailPath = value;
			}
		}
		public string ThumbnailCode
		{
			get
			{
				return this._thumbnailCode;
			}
			set
			{
				this._thumbnailCode = value;
			}
		}
		public string MetadataPath
		{
			get
			{
				return this._metadataPath;
			}
			set
			{
				this._metadataPath = value;
			}
		}
		public Photo()
		{
			this._photoName = "";
			this._photoPath = "";
			this._thumbnailPath = "";
			this._thumbnailCode = "";
			this._metadataPath = "";
			this._title = "";
			this._description = "";
			this._dateTaken = "";
		}
		public static Photo[] GetPhotos(string path, string thumbnailFolder)
		{
			string[] photoFileList = FileManager.GetPhotoFileList(path);
			if (photoFileList == null)
			{
				return null;
			}
			checked
			{
				Photo[] array = new Photo[photoFileList.Length - 1 + 1];
				int arg_27_0 = 0;
				int num = photoFileList.Length - 1;
				for (int i = arg_27_0; i <= num; i++)
				{
					string thumbnailPath = Path.Combine(Path.Combine(Path.GetDirectoryName(photoFileList[i]), thumbnailFolder), Photo.GetThumbnailName(photoFileList[i]));
					array[i] = Photo.ReadXml(photoFileList[i]);
					array[i].PhotoName = Path.GetFileNameWithoutExtension(photoFileList[i]);
					array[i].PhotoPath = photoFileList[i];
					array[i].ThumbnailCode = Photo.GetThumbnailCode(photoFileList[i]);
					array[i].ThumbnailPath = thumbnailPath;
				}
				return array;
			}
		}
		public static bool CreateThumbnail(string photoPath, string thumbnailFolder)
		{
			bool result = false;
			try
			{
				string text = Path.Combine(thumbnailFolder, Photo.GetThumbnailName(photoPath));
				if (!File.Exists(text))
				{
					Image thumbnail = PhotoHelper.GetThumbnail(photoPath, 120);
					thumbnail.Save(text, ImageFormat.Jpeg);
					result = true;
				}
			}
			catch (Exception expr_30)
			{
				ProjectData.SetProjectError(expr_30);
				ProjectData.ClearProjectError();
			}
			finally
			{
				Image thumbnail;
				if (thumbnail != null)
				{
					thumbnail.Dispose();
				}
			}
			return result;
		}
		public void WriteXml()
		{
			string text = this.PhotoPath + ".xml";
			try
			{
				XmlTextWriter xmlTextWriter = new XmlTextWriter(text, null);
				xmlTextWriter.Formatting = 1;
				xmlTextWriter.WriteStartElement("metadata");
				xmlTextWriter.WriteStartElement("photo");
				xmlTextWriter.WriteStartAttribute("title", null);
				xmlTextWriter.WriteString(this.Title);
				xmlTextWriter.WriteEndAttribute();
				xmlTextWriter.WriteStartAttribute("date", null);
				xmlTextWriter.WriteString(this.DateTaken.Trim());
				xmlTextWriter.WriteEndAttribute();
				xmlTextWriter.WriteStartAttribute("description", null);
				xmlTextWriter.WriteString(this.Description);
				xmlTextWriter.WriteEndAttribute();
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.Flush();
				xmlTextWriter.Close();
			}
			catch (Exception expr_AF)
			{
				ProjectData.SetProjectError(expr_AF);
				Exception ex = expr_AF;
				Global.DisplayError("Error updating the photo metadata file.", ex);
				ProjectData.ClearProjectError();
			}
		}
		public void UpdateLocation(string location)
		{
			string directoryName = Path.GetDirectoryName(this.PhotoPath);
			string text = Path.Combine(Path.GetDirectoryName(directoryName), location);
			this.PhotoPath = this.PhotoPath.Replace(directoryName, text);
			this.MetadataPath = this.MetadataPath.Replace(directoryName, text);
			this.ThumbnailPath = this.ThumbnailPath.Replace(directoryName, text);
		}
		public int CompareTo(object obj)
		{
			if (obj is string)
			{
				return this.ThumbnailPath.CompareTo(RuntimeHelpers.GetObjectValue(obj));
			}
			return this.ThumbnailPath.CompareTo(((Photo)obj).ThumbnailPath);
		}
		public bool Equals(object obj)
		{
			return BooleanType.FromObject(Interaction.IIf(this.CompareTo(RuntimeHelpers.GetObjectValue(obj)) == 0, true, false));
		}
		public static bool Equals(Photo objA, Photo objB)
		{
			return objA.Equals(objB);
		}
		public override int GetHashCode()
		{
			return this.ThumbnailPath.GetHashCode();
		}
		private static string GetThumbnailCode(string photoPath)
		{
			string text = Path.GetExtension(photoPath) + StringType.FromLong(File.GetLastWriteTime(photoPath).Ticks);
			return text.Substring(1);
		}
		private static string GetThumbnailName(string photoPath)
		{
			return string.Format("{0}-{1}.jpg", Path.GetFileNameWithoutExtension(photoPath), Photo.GetThumbnailCode(photoPath));
		}
		private static Photo ReadXml(string photoPath)
		{
			Photo photo = new Photo();
			string text = photoPath + ".xml";
			photo.MetadataPath = text;
			if (File.Exists(text))
			{
				try
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.Load(text);
					XmlNode firstChild = xmlDocument.DocumentElement.FirstChild;
					photo.Title = firstChild.Attributes["title"].Value;
					photo.Description = firstChild.Attributes["description"].Value;
					if (Global.ValidateDate(firstChild.Attributes["date"].Value))
					{
						photo.DateTaken = DateTime.Parse(firstChild.Attributes["date"].Value, CultureInfo.CurrentCulture).ToShortDateString();
						return photo;
					}
					photo.DateTaken = File.GetLastWriteTime(photoPath).ToShortDateString();
					return photo;
				}
				catch (Exception expr_DB)
				{
					ProjectData.SetProjectError(expr_DB);
					ProjectData.ClearProjectError();
					return photo;
				}
			}
			photo.DateTaken = File.GetLastWriteTime(photoPath).ToShortDateString();
			return photo;
		}
	}
}
