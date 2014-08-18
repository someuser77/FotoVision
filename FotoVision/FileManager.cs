using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
namespace FotoVision
{
	public sealed class FileManager
	{
		private class Consts
		{
			public const string LocalPhotoFolder = "My Albums";
			public const string ThumbnailFolder = "thumbnails";
			public const string NewAlbumName = "New album";
			public const string NewPhotoName = "New photo";
			public const string AlbumMetadata = "album.xml";
			public static string[] PhotoFormats = new string[]
			{
				".jpg",
				".jpeg",
				".tif",
				"tiff",
				".png",
				".bmp",
				".gif"
			};
			public const string OpenFilter = "All Formats|*.jpg;*.jpeg;*.tif;*.tiff;*.png;*.bmp;*.gif|JPEG Compliant (*.jpg, *.jpeg)|*.jpg;*.jpeg|Tagged Image File Format (*.tif, *.tiff)|*.tif;*.tiff|Portable Network Graphics (*.png)|*.png|Windows Bitmap (*.bmp)|*.bmp|CompuServe Graphics Interchange (*.gif)|*.gif";
			public const int MaxAlbumLength = 100;
			public const string InvalidAlbumPattern = "[\\\\/:*?\"<>|]";
		}
		public static string OpenFilter
		{
			get
			{
				return "All Formats|*.jpg;*.jpeg;*.tif;*.tiff;*.png;*.bmp;*.gif|JPEG Compliant (*.jpg, *.jpeg)|*.jpg;*.jpeg|Tagged Image File Format (*.tif, *.tiff)|*.tif;*.tiff|Portable Network Graphics (*.png)|*.png|Windows Bitmap (*.bmp)|*.bmp|CompuServe Graphics Interchange (*.gif)|*.gif";
			}
		}
		private static string NewAlbumName
		{
			get
			{
				int num = 2;
				string text = "New album";
				checked
				{
					while (Directory.Exists(Path.Combine(FileManager.Location, text)))
					{
						text = string.Format("{0} {1}", "New album", num);
						num++;
					}
					return text;
				}
			}
		}
		private static string Location
		{
			get
			{
				return Path.Combine(Global.DataLocation, "My Albums");
			}
		}
		private FileManager()
		{
		}
		public static string GetLocation(string album)
		{
			return Path.Combine(FileManager.Location, album);
		}
		public static string GetAlbumMetadataLocation(string album)
		{
			return Path.Combine(FileManager.GetLocation(album), "album.xml");
		}
		public static string GetNewPhotoName(string album)
		{
			string text = Path.Combine(FileManager.Location, album);
			string text2 = ".jpg";
			int num = 2;
			string text3 = "New photo" + text2;
			checked
			{
				while (File.Exists(Path.Combine(text, text3)))
				{
					text3 = string.Format("{0} {1}{2}", "New photo", num, text2);
					num++;
				}
				return text3;
			}
		}
		public static string AddNewAlbum()
		{
			string newAlbumName = FileManager.NewAlbumName;
			FileManager.AddAlbum(newAlbumName);
			return newAlbumName;
		}
		public static void AddAlbum(string name)
		{
			Directory.CreateDirectory(Path.Combine(FileManager.Location, name));
		}
		public static void RenameAlbum(string oldName, string newName)
		{
			string text = Path.Combine(FileManager.Location, oldName);
			string text2 = Path.Combine(FileManager.Location, newName);
			if (newName.IndexOf(Path.DirectorySeparatorChar) != -1 || newName.IndexOf(Path.AltDirectorySeparatorChar) != -1)
			{
				throw new ArgumentException(string.Format("The directory name '{0}' contains invalid characters.", newName));
			}
			string text3 = Path.Combine(FileManager.Location, Path.GetFileName(Path.GetTempFileName()));
			Directory.Move(text, text3);
			Directory.Move(text3, text2);
		}
		public static bool AlbumExists(string name)
		{
			return Directory.Exists(Path.Combine(FileManager.Location, name));
		}
		public static string[] GetAlbums()
		{
			if (!Directory.Exists(FileManager.Location))
			{
				return null;
			}
			string[] directories = Directory.GetDirectories(FileManager.Location);
			int arg_23_0 = 0;
			checked
			{
				int num = directories.Length - 1;
				for (int i = arg_23_0; i <= num; i++)
				{
					directories[i] = Path.GetFileName(directories[i]);
				}
				return directories;
			}
		}
		public static Photo[] GetPhotos(string album, bool update)
		{
			string text = Path.Combine(FileManager.Location, album);
			if (!Directory.Exists(text))
			{
				return null;
			}
			if (update)
			{
				FileManager.UpdateThumbnails(text);
			}
			return Photo.GetPhotos(text, "thumbnails");
		}
		public static bool IsSupportedFile(string file)
		{
			string text = Path.GetExtension(file).ToLower(CultureInfo.InvariantCulture);
			string[] photoFormats = FileManager.Consts.PhotoFormats;
			checked
			{
				for (int i = 0; i < photoFormats.Length; i++)
				{
					string text2 = photoFormats[i];
					if (StringType.StrCmp(text, text2, false) == 0)
					{
						return true;
					}
				}
				return false;
			}
		}
		public static int GetPhotoCount(string album)
		{
			string text = Path.Combine(FileManager.Location, album);
			if (!Directory.Exists(text))
			{
				return 0;
			}
			string[] photoFileList = FileManager.GetPhotoFileList(text);
			if (photoFileList == null)
			{
				return 0;
			}
			return photoFileList.Length;
		}
		public static string[] GetPhotoFileList(string path)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string[] photoFormats = FileManager.Consts.PhotoFormats;
			checked
			{
				for (int i = 0; i < photoFormats.Length; i++)
				{
					string text = photoFormats[i];
					string[] files = Directory.GetFiles(path, "*" + text);
					if (files.Length > 0)
					{
						if (stringBuilder.Length > 0)
						{
							stringBuilder.Append(";");
						}
						stringBuilder.Append(string.Join(";", files));
					}
				}
				if (stringBuilder.ToString().Length > 0)
				{
					return stringBuilder.ToString().Split(new char[]
					{
						';'
					});
				}
				return null;
			}
		}
		public static void CopyFile(string sourceFileName, string destFileName)
		{
			File.Copy(sourceFileName, destFileName, true);
			if (FileManager.IsFileReadOnly(destFileName))
			{
				FileAttributes fileAttributes = File.GetAttributes(destFileName);
                fileAttributes &= ~FileAttributes.ReadOnly;
				File.SetAttributes(destFileName, fileAttributes);
			}
		}
		public static void CopyMetaFile(string source, string target)
		{
			string text = source + ".xml";
			string text2 = target + ".xml";
			if (File.Exists(text))
			{
				string directoryName = Path.GetDirectoryName(text2);
				if (!Directory.Exists(directoryName))
				{
					Directory.CreateDirectory(directoryName);
				}
				FileManager.CopyFile(text, text2);
			}
		}
		public static void DeleteMetaFile(string photoPath)
		{
			string text = photoPath + ".xml";
			if (File.Exists(text))
			{
				FileManager.DeleteFile(text);
			}
		}
		public static void DeletePhoto(string photoPath)
		{
			if (File.Exists(photoPath))
			{
				FileManager.DeleteFile(photoPath);
				FileManager.DeleteMetaFile(photoPath);
			}
		}
		public static bool DeleteAlbum(string albumName)
		{
			bool result = false;
			string text = Path.Combine(FileManager.Location, albumName);
			if (Directory.Exists(text))
			{
				result = FileManager.DeleteDirectory(text);
			}
			return result;
		}
		public static bool IsFileReadOnly(string path)
		{
			bool result;
			try
			{
                result = BooleanType.FromObject(Interaction.IIf((File.GetAttributes(path) & FileAttributes.ReadOnly) > 0, true, false));
			}
			catch (Exception expr_24)
			{
				ProjectData.SetProjectError(expr_24);
				result = false;
				ProjectData.ClearProjectError();
			}
			return result;
		}
		public static bool IsValidAlbumName(string name)
		{
			return !Regex.IsMatch(name, "[\\\\/:*?\"<>|]") && name.Length != 0 && name.Length <= 100 && !name.StartsWith(".") && !name.EndsWith(".");
		}
		public static bool DeleteFile(string path)
		{
			bool result = false;
			try
			{
				if (File.Exists(path))
				{
					if (FileManager.IsFileReadOnly(path))
					{
						throw new ApplicationException(string.Format("The file '{0}' is read-only.", path));
					}
					File.Delete(path);
					result = true;
				}
			}
			catch (Exception expr_2D)
			{
				ProjectData.SetProjectError(expr_2D);
				Exception ex = expr_2D;
				Global.DisplayError(string.Format("The file '{0}' could not be deleted.", Path.GetFileName(path)), ex);
				ProjectData.ClearProjectError();
			}
			return result;
		}
		public static bool DeleteDirectory(string path)
		{
			bool result = false;
			try
			{
				if (Directory.Exists(path))
				{
					Directory.Delete(path, true);
					result = true;
				}
			}
			catch (Exception expr_15)
			{
				ProjectData.SetProjectError(expr_15);
				Exception ex = expr_15;
				Global.DisplayError(string.Format("The folder '{0}' could not be deleted.", Path.GetFileName(path)), ex);
				ProjectData.ClearProjectError();
			}
			return result;
		}
		private static void UpdateThumbnails(string albumPath)
		{
			FileManager.DeleteUnusedThumbnails(albumPath);
			string[] photoFileList = FileManager.GetPhotoFileList(albumPath);
			if (photoFileList == null)
			{
				return;
			}
			string text = Path.Combine(albumPath, "thumbnails");
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			int arg_37_0 = 0;
			checked
			{
				int num = photoFileList.Length - 1;
				for (int i = arg_37_0; i <= num; i++)
				{
					string photoPath = photoFileList[i];
					bool flag = Photo.CreateThumbnail(photoPath, text);
					if (flag)
					{
						Global.Progress.Update(null, "Generating thumbnails", i + 1, photoFileList.Length);
					}
				}
				Global.Progress.Complete(null);
			}
		}
		private static void DeleteUnusedThumbnails(string albumPath)
		{
			string text = Path.Combine(albumPath, "thumbnails");
			if (!Directory.Exists(text))
			{
				return;
			}
			string[] files = Directory.GetFiles(text, "*.jpg");
			Photo[] photos = Photo.GetPhotos(albumPath, "thumbnails");
			if (photos == null)
			{
				return;
			}
			Array.Sort(photos);
			string[] array = files;
			checked
			{
				for (int i = 0; i < array.Length; i++)
				{
					string text2 = array[i];
					if (Array.BinarySearch(photos, text2) < 0)
					{
						FileManager.DeleteFile(text2);
					}
				}
			}
		}
	}
}
