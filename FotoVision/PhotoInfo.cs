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
			this._fileLengthString = this.GetLengthString(fileInfo.get_Length());
			this._dateCreated = fileInfo.get_CreationTime();
			this._dateModified = fileInfo.get_LastWriteTime();
			this._readOnly = FileManager.IsFileReadOnly(path);
			this._imageSize = image.get_Size();
			this._imageType = this.GetFileType(format);
		}
		private string GetFileType(ImageFormat format)
		{
			if (format.Equals(ImageFormat.get_Jpeg()))
			{
				return "Jpeg";
			}
			if (format.Equals(ImageFormat.get_Tiff()))
			{
				return "Tiff";
			}
			if (format.Equals(ImageFormat.get_Png()))
			{
				return "Png";
			}
			if (format.Equals(ImageFormat.get_Gif()))
			{
				return "Gif";
			}
			if (format.Equals(ImageFormat.get_Bmp()))
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
				return ((float)((double)length / 1024.0)).ToString("f1", CultureInfo.get_CurrentCulture()) + " KB";
			}
			return ((float)((double)length / 1048576.0)).ToString("f1", CultureInfo.get_CurrentCulture()) + " MB";
		}
	}
}
