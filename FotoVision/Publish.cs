using FotoVision.PhotoAdmin;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
namespace FotoVision
{
	public class Publish
	{
		private class Consts
		{
			public const string PublishPhotoCache = "Publish Cache";
			public const string AlbumMetadataFile = "album.xml";
		}
		public delegate void UpdateMessageEventHandler(object sender, UploadMessageEventArgs e);
		public delegate void UpdateProgressEventHandler(object sender, UploadProgressEventArgs e);
		public delegate void CompleteEventHandler(object sender, UploadCompleteEventArgs e);
		private Publish.UpdateProgressEventHandler UpdateProgressEvent;
		private Publish.UpdateMessageEventHandler UpdateMessageEvent;
		private Publish.CompleteEventHandler CompleteEvent;
		private string[] _albumList;
		private int _photoSize;
		private int _quality;
		private int _progressCount;
		private int _progressPos;
		public event Publish.UpdateMessageEventHandler UpdateMessage
		{
			[MethodImpl(32)]
			add
			{
				this.UpdateMessageEvent = (Publish.UpdateMessageEventHandler)Delegate.Combine(this.UpdateMessageEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.UpdateMessageEvent = (Publish.UpdateMessageEventHandler)Delegate.Remove(this.UpdateMessageEvent, value);
			}
		}
		public event Publish.UpdateProgressEventHandler UpdateProgress
		{
			[MethodImpl(32)]
			add
			{
				this.UpdateProgressEvent = (Publish.UpdateProgressEventHandler)Delegate.Combine(this.UpdateProgressEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.UpdateProgressEvent = (Publish.UpdateProgressEventHandler)Delegate.Remove(this.UpdateProgressEvent, value);
			}
		}
		public event Publish.CompleteEventHandler Complete
		{
			[MethodImpl(32)]
			add
			{
				this.CompleteEvent = (Publish.CompleteEventHandler)Delegate.Combine(this.CompleteEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.CompleteEvent = (Publish.CompleteEventHandler)Delegate.Remove(this.CompleteEvent, value);
			}
		}
		private static string Location
		{
			get
			{
				return Path.Combine(Global.DataLocation, "Publish Cache");
			}
		}
		public Publish()
		{
			this._progressCount = 0;
			this._progressPos = 0;
		}
		public void SetAlbumList(string[] list)
		{
			this._albumList = null;
			if (list != null)
			{
				this._albumList = new string[checked(list.Length - 1 + 1)];
				Array.Copy(list, this._albumList, list.Length);
			}
		}
		public void CreatePublishFiles()
		{
			bool errorOccurred = false;
			this._progressCount = 0;
			this._progressPos = 0;
			this._photoSize = Global.Settings.GetInt(SettingKey.PublishPhotoSize);
			this._quality = Global.Settings.GetInt(SettingKey.PublishPhotoQuality);
			if (this.UpdateMessageEvent != null)
			{
				this.UpdateMessageEvent(this, new UploadMessageEventArgs("Preparing files for publishing", true, true));
			}
			checked
			{
				try
				{
					if (this._albumList == null)
					{
						Publish.Delete();
						this.Terminate(false);
						return;
					}
					this.DeleteUnusedAlbums();
					this._progressCount = 0;
					string[] albumList = this._albumList;
					for (int i = 0; i < albumList.Length; i++)
					{
						string album = albumList[i];
						this._progressCount += FileManager.GetPhotoCount(album);
					}
					string[] albumList2 = this._albumList;
					for (int j = 0; j < albumList2.Length; j++)
					{
						string albumName = albumList2[j];
						this.CreatePhotoFiles(albumName);
					}
				}
				catch (ThreadAbortException expr_D6)
				{
					ProjectData.SetProjectError(expr_D6);
					ThreadAbortException ex = (ThreadAbortException)expr_D6;
					ProjectData.ClearProjectError();
					return;
				}
				catch (Exception expr_E9)
				{
					ProjectData.SetProjectError(expr_E9);
					Exception ex2 = expr_E9;
					if (this.UpdateMessageEvent != null)
					{
						this.UpdateMessageEvent(this, new UploadMessageEventArgs("Publish error - " + ex2.Message, false, true));
					}
					errorOccurred = true;
					ProjectData.ClearProjectError();
				}
				this.Terminate(errorOccurred);
			}
		}
		public static void Delete()
		{
			FileManager.DeleteDirectory(Publish.Location);
		}
		public static string[] GetAlbums()
		{
			if (!Directory.Exists(Publish.Location))
			{
				return null;
			}
			string[] directories = Directory.GetDirectories(Publish.Location);
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
		public static string[] GetPhotos(string albumName)
		{
			string text = Path.Combine(Publish.Location, albumName);
			if (!Directory.Exists(text))
			{
				return null;
			}
			string[] files = Directory.GetFiles(text, "*.jpg");
			int arg_2D_0 = 0;
			checked
			{
				int num = files.Length - 1;
				for (int i = arg_2D_0; i <= num; i++)
				{
					files[i] = Path.GetFileName(files[i]);
				}
				return files;
			}
		}
		public static PhotoHashCode GetPhotoHashCode(string albumName, string photoName)
		{
			string text = Path.Combine(Publish.Location, Path.Combine(albumName, photoName));
			if (!File.Exists(text))
			{
				return null;
			}
			PhotoHashCode photoHashCode = new PhotoHashCode();
			photoHashCode.ImageFileName = text;
			photoHashCode.MetaDataFileName = text + ".xml";
			photoHashCode.ImageHash = Hash.ComputeFileHash(photoHashCode.ImageFileName);
			photoHashCode.MetaDataHash = Hash.ComputeFileHash(photoHashCode.MetaDataFileName);
			return photoHashCode;
		}
		public static AlbumHashCode GetAlbumHashCode(string albumName)
		{
			string text = Path.Combine(Publish.Location, albumName);
			if (!Directory.Exists(text))
			{
				return null;
			}
			AlbumHashCode albumHashCode = new AlbumHashCode();
			albumHashCode.AlbumName = text;
			albumHashCode.MetaDataFileName = Path.Combine(text, "album.xml");
			albumHashCode.MetaDataHash = Hash.ComputeFileHash(albumHashCode.MetaDataFileName);
			return albumHashCode;
		}
		private void Terminate(bool errorOccurred)
		{
			if (!errorOccurred && this.UpdateMessageEvent != null)
			{
				this.UpdateMessageEvent(this, new UploadMessageEventArgs("Preparing operation complete", true, true));
			}
			if (this.UpdateProgressEvent != null)
			{
				this.UpdateProgressEvent(this, new UploadProgressEventArgs(1, 1));
			}
			if (this.CompleteEvent != null)
			{
				this.CompleteEvent(this, new UploadCompleteEventArgs(errorOccurred));
			}
		}
		private void CreatePhotoFiles(string albumName)
		{
			string text = Path.Combine(Publish.Location, albumName);
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			this.CopyAlbumMetadata(albumName);
			Photo[] photos = FileManager.GetPhotos(albumName, false);
			this.DeleteUnusedPhotos(photos, albumName);
			if (photos == null)
			{
				return;
			}
			Photo[] array = photos;
			checked
			{
				for (int i = 0; i < array.Length; i++)
				{
					Photo photo = array[i];
					this.SavePhoto(text, photo, albumName);
				}
			}
		}
		private void CopyAlbumMetadata(string albumName)
		{
			string albumMetadataLocation = FileManager.GetAlbumMetadataLocation(albumName);
			string text = Path.Combine(Path.Combine(Publish.Location, albumName), Path.GetFileName(albumMetadataLocation));
			if (File.Exists(albumMetadataLocation))
			{
				FileManager.CopyFile(albumMetadataLocation, text);
			}
			else
			{
				FileManager.DeleteFile(text);
			}
		}
		private void SavePhoto(string location, Photo photo, string albumName)
		{
			if (this.UpdateMessageEvent != null)
			{
				this.UpdateMessageEvent(this, new UploadMessageEventArgs("Processing photo " + photo.PhotoName, true, false));
			}
			checked
			{
				try
				{
					string photoFileName = this.GetPhotoFileName(photo);
					string text = Path.Combine(location, photoFileName);
					string text2 = PublishData.ReadPhotoHashCode(text);
					PublishData publishData = new PublishData(photoFileName, photo);
					if (StringType.StrCmp(publishData.HashCode, text2, false) != 0 || !File.Exists(text))
					{
						Bitmap bitmap = PhotoHelper.Resize(photo.PhotoPath, this._photoSize);
						if (bitmap != null)
						{
							JpegQuality.Save(text, bitmap, this._quality);
							if (this.UpdateMessageEvent != null)
							{
								this.UpdateMessageEvent(this, new UploadMessageEventArgs("   Resized photo " + photo.PhotoName, true, true));
							}
							bitmap.Dispose();
							bitmap = null;
						}
					}
					if (this.UpdateProgressEvent != null)
					{
						this.UpdateProgressEvent(this, new UploadProgressEventArgs(this._progressPos, this._progressCount));
					}
					publishData.WriteXml(text);
					this._progressPos++;
				}
				catch (Exception expr_F8)
				{
					ProjectData.SetProjectError(expr_F8);
					Bitmap bitmap;
					if (bitmap != null)
					{
						bitmap.Dispose();
					}
					ProjectData.ClearProjectError();
				}
			}
		}
		private void DeleteUnusedAlbums()
		{
			if (this.UpdateMessageEvent != null)
			{
				this.UpdateMessageEvent(this, new UploadMessageEventArgs("Deleting unused albums", true, false));
			}
			if (!Directory.Exists(Publish.Location))
			{
				return;
			}
			string[] directories = Directory.GetDirectories(Publish.Location);
			Array.Sort(this._albumList);
			string[] array = directories;
			checked
			{
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i];
					if (Array.BinarySearch(this._albumList, Path.GetFileName(text)) < 0)
					{
						FileManager.DeleteDirectory(text);
					}
				}
			}
		}
		private void DeleteUnusedPhotos(Photo[] photos, string albumName)
		{
			if (this.UpdateMessageEvent != null)
			{
				this.UpdateMessageEvent(this, new UploadMessageEventArgs("Deleting unused photos in album " + albumName, true, false));
			}
			string text = Path.Combine(Publish.Location, albumName);
			if (!Directory.Exists(text))
			{
				return;
			}
			string[] files = Directory.GetFiles(text, "*.jpg");
			checked
			{
				if (photos == null)
				{
					string[] array = files;
					for (int i = 0; i < array.Length; i++)
					{
						string text2 = array[i];
						FileManager.DeleteFile(text2);
						FileManager.DeleteFile(text2 + ".xml");
					}
					return;
				}
				string[] array2 = new string[photos.Length - 1 + 1];
				int arg_9A_0 = 0;
				int num = photos.Length - 1;
				for (int j = arg_9A_0; j <= num; j++)
				{
					array2[j] = this.GetPhotoFileName(photos[j]);
				}
				Array.Sort(array2);
				string[] array3 = files;
				for (int k = 0; k < array3.Length; k++)
				{
					string text3 = array3[k];
					if (Array.BinarySearch(array2, Path.GetFileName(text3)) < 0)
					{
						FileManager.DeleteFile(text3);
						FileManager.DeleteFile(text3 + ".xml");
					}
				}
			}
		}
		private string GetPhotoFileName(Photo photo)
		{
			string fileName = Path.GetFileName(photo.PhotoPath);
			if (string.Compare(Path.GetExtension(fileName), ".jpg", true, CultureInfo.InvariantCulture) == 0)
			{
				return fileName;
			}
			return string.Format("{0}-{1}.jpg", photo.PhotoName, Path.GetExtension(fileName).ToLower(CultureInfo.InvariantCulture).Substring(1));
		}
	}
}
