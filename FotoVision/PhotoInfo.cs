using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
namespace FotoVision
{
	public struct PhotoInfo
	{
		private string _fileName;
		private string _path;
		private string _fileLengthString;
		private Size _imageSize;
		private string _imageType;
		private DateTime _dateCreated;
		private DateTime _dateModified;
		private bool _readOnly;
		public string FileName
		{
			get
			{
				return this._fileName;
			}
		}
		public string Path
		{
			get
			{
				return this._path;
			}
		}
		public string FileLengthString
		{
			get
			{
				return this._fileLengthString;
			}
		}
		public Size ImageSize
		{
			get
			{
				return this._imageSize;
			}
		}
		public string ImageType
		{
			get
			{
				return this._imageType;
			}
		}
		public DateTime DateCreated
		{
			get
			{
				return this._dateCreated;
			}
		}
		public DateTime DateModified
		{
			get
			{
				return this._dateModified;
			}
		}
		public bool ReadOnly
		{
			get
			{
				return this._readOnly;
			}
		}
		public void Read(string path, Bitmap image, ImageFormat format)
		{
			FileInfo fileInfo = new FileInfo(path);
			this._path = path;
			this._fileName = System.IO.Path.GetFileName(path);
			this._fileLengthString = this.GetLengthString(fileInfo.Length);
			this._dateCreated = fileInfo.CreationTime;
			this._dateModified = fileInfo.LastWriteTime;
			this._readOnly = FileManager.IsFileReadOnly(path);
			this._imageSize = image.Size;
			this._imageType = this.GetFileType(format);
		}
		private string GetFileType(ImageFormat format)
		{
			if (format.Equals(ImageFormat.Jpeg))
			{
				return "Jpeg";
			}
			if (format.Equals(ImageFormat.Tiff))
			{
				return "Tiff";
			}
			if (format.Equals(ImageFormat.Png))
			{
				return "Png";
			}
			if (format.Equals(ImageFormat.Gif))
			{
				return "Gif";
			}
			if (format.Equals(ImageFormat.Bmp))
			{
				return "Bmp";
			}
			return "Unknown";
		}
		private string GetLengthString(long length)
		{
			if (length < 1024L)
			{
				return StringType.FromLong(length) + " Bytes";
			}
			if (length < 1048576L)
			{
				return ((float)((double)length / 1024.0)).ToString("f1", CultureInfo.CurrentCulture) + " KB";
			}
			return ((float)((double)length / 1048576.0)).ToString("f1", CultureInfo.CurrentCulture) + " MB";
		}
	}
}
