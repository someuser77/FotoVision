using FotoVision.PhotoAdmin;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
namespace FotoVision
{
	public class Upload
	{
		private class Consts
		{
			public const string AdminServiceUrl = "PhotoAdmin.asmx";
		}
		public delegate void UpdateMessageEventHandler(object sender, UploadMessageEventArgs e);
		public delegate void UpdateProgressEventHandler(object sender, UploadProgressEventArgs e);
		public delegate void CompleteEventHandler(object sender, UploadCompleteEventArgs e);
		private Upload.UpdateMessageEventHandler UpdateMessageEvent;
		private Upload.UpdateProgressEventHandler UpdateProgressEvent;
		private Upload.CompleteEventHandler CompleteEvent;
        private FotoVision.PhotoAdmin.PhotoAdmin _photoAdmin;
		private AdminHeader _adminHeader;
		private int _progressCount;
		private int _progressPos;
		private Form _parent;
		public event Upload.UpdateMessageEventHandler UpdateMessage
		{
			[MethodImpl(32)]
			add
			{
				this.UpdateMessageEvent = (Upload.UpdateMessageEventHandler)Delegate.Combine(this.UpdateMessageEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.UpdateMessageEvent = (Upload.UpdateMessageEventHandler)Delegate.Remove(this.UpdateMessageEvent, value);
			}
		}
		public event Upload.UpdateProgressEventHandler UpdateProgress
		{
			[MethodImpl(32)]
			add
			{
				this.UpdateProgressEvent = (Upload.UpdateProgressEventHandler)Delegate.Combine(this.UpdateProgressEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.UpdateProgressEvent = (Upload.UpdateProgressEventHandler)Delegate.Remove(this.UpdateProgressEvent, value);
			}
		}
		public event Upload.CompleteEventHandler Complete
		{
			[MethodImpl(32)]
			add
			{
				this.CompleteEvent = (Upload.CompleteEventHandler)Delegate.Combine(this.CompleteEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.CompleteEvent = (Upload.CompleteEventHandler)Delegate.Remove(this.CompleteEvent, value);
			}
		}
		public Form Parent
		{
			get
			{
				return this._parent;
			}
			set
			{
				this._parent = value;
			}
		}
        private FotoVision.PhotoAdmin.PhotoAdmin PhotoAdmin
		{
			get
			{
				if (this._photoAdmin == null)
				{
                    this._photoAdmin = new FotoVision.PhotoAdmin.PhotoAdmin();
					this._photoAdmin.Url = Global.CombineUrl(Global.Settings.GetString(SettingKey.ServiceLocation), "PhotoAdmin.asmx");
					this._photoAdmin.Timeout = checked(Global.Settings.GetInt(SettingKey.ServiceTimeout) * 1000);
					this.AttachCredentials();
				}
				return this._photoAdmin;
			}
		}
		public Upload()
		{
			this._progressCount = 0;
			this._progressPos = 0;
		}
		public void Synchronize()
		{
			bool errorOccurred = false;
			this._progressCount = 0;
			this._progressPos = 0;
			if (this.UpdateMessageEvent != null)
			{
				this.UpdateMessageEvent(this, new UploadMessageEventArgs("Logging into the website", true, true));
			}
			checked
			{
				try
				{
					if (!this.Login())
					{
						if (this.UpdateMessageEvent != null)
						{
							this.UpdateMessageEvent(this, new UploadMessageEventArgs("Upload error - could not log into the web service", false, true));
						}
						errorOccurred = true;
					}
					else
					{
						if (this.UpdateMessageEvent != null)
						{
							this.UpdateMessageEvent(this, new UploadMessageEventArgs("Uploading files to the website", true, true));
						}
						string[] albums = Publish.GetAlbums();
						if (albums == null)
						{
							this.SyncAlbums(albums);
						}
						else
						{
							Array.Sort(albums);
							this.CalculateProgressCount(albums);
							this.SyncAlbums(albums);
							string[] array = albums;
							for (int i = 0; i < array.Length; i++)
							{
								string albumName = array[i];
								this.SyncPhotos(albumName);
							}
						}
					}
				}
				catch (ThreadAbortException expr_CD)
				{
					ProjectData.SetProjectError(expr_CD);
					ThreadAbortException ex = (ThreadAbortException)expr_CD;
					ProjectData.ClearProjectError();
					return;
				}
				catch (WebException expr_E3)
				{
					ProjectData.SetProjectError(expr_E3);
					WebException ex2 = (WebException)expr_E3;
					string message = string.Format("An error occurred when accessing the site '{0}'. Please make sure the FotoVision Web package is installed on the computer. Additional information: {1}", Global.Settings.GetString(SettingKey.ServiceLocation), ex2.Message);
					if (this.UpdateMessageEvent != null)
					{
						this.UpdateMessageEvent(this, new UploadMessageEventArgs(message, false, true));
					}
					errorOccurred = true;
					ProjectData.ClearProjectError();
				}
				catch (Exception expr_135)
				{
					ProjectData.SetProjectError(expr_135);
					Exception ex3 = expr_135;
					if (this.UpdateMessageEvent != null)
					{
						this.UpdateMessageEvent(this, new UploadMessageEventArgs("Upload error - " + ex3.Message, false, true));
					}
					errorOccurred = true;
					ProjectData.ClearProjectError();
				}
				finally
				{
					if (this._photoAdmin != null)
					{
						this._photoAdmin.Dispose();
						this._photoAdmin = null;
					}
				}
				this.Terminate(errorOccurred);
			}
		}
		private void CalculateProgressCount(string[] albums)
		{
			if (albums == null)
			{
				return;
			}
			checked
			{
				for (int i = 0; i < albums.Length; i++)
				{
					string album = albums[i];
					this._progressCount++;
					this._progressCount += FileManager.GetPhotoCount(album);
				}
			}
		}
		private void Terminate(bool errorOccurred)
		{
			if (!errorOccurred && this.UpdateMessageEvent != null)
			{
				this.UpdateMessageEvent(this, new UploadMessageEventArgs("Uploading operation complete", true, true));
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
		private bool Login()
		{
			LoginForm loginForm = new LoginForm();
			while (!this.PhotoAdmin.Login())
			{
				if (loginForm.ShowDialog(this._parent) != DialogResult.OK)
				{
					return false;
				}
				this.AttachCredentials();
			}
			return true;
		}
		private void SyncAlbums(string[] albums)
		{
			if (this.UpdateMessageEvent != null)
			{
				this.UpdateMessageEvent(this, new UploadMessageEventArgs("Requesting album information", true, false));
			}
			AlbumHashCode[] albumHashCodes = this.PhotoAdmin.GetAlbumHashCodes();
			if (albums == null)
			{
				this.DeleteAllAlbums(albumHashCodes);
				return;
			}
			if (albumHashCodes == null)
			{
				this.AddAllAlbums(albums);
				return;
			}
			checked
			{
				string[] array = new string[albums.Length - 1 + 1];
				Array.Copy(albums, array, albums.Length);
				AlbumHashCode[] array2 = albumHashCodes;
				for (int i = 0; i < array2.Length; i++)
				{
					AlbumHashCode albumHashCode = array2[i];
					int num = Array.BinarySearch(albums, albumHashCode.AlbumName);
					if (num < 0)
					{
						if (this.UpdateMessageEvent != null)
						{
							this.UpdateMessageEvent(this, new UploadMessageEventArgs("Deleting album " + albumHashCode.AlbumName, true, false));
						}
						this.PhotoAdmin.DeleteAlbum(albumHashCode.AlbumName);
						if (this.UpdateMessageEvent != null)
						{
							this.UpdateMessageEvent(this, new UploadMessageEventArgs("   Deleted album " + albumHashCode.AlbumName, true, true));
						}
					}
					else
					{
						this.UpdateAlbum(albumHashCode.AlbumName, albumHashCode);
						array[num] = null;
					}
				}
				string[] array3 = array;
				for (int j = 0; j < array3.Length; j++)
				{
					string text = array3[j];
					if (text != null)
					{
						this.UpdateAlbum(text, null);
					}
				}
			}
		}
		private void DeleteAllAlbums(AlbumHashCode[] serverHashCodes)
		{
			if (serverHashCodes == null)
			{
				return;
			}
			checked
			{
				for (int i = 0; i < serverHashCodes.Length; i++)
				{
					AlbumHashCode albumHashCode = serverHashCodes[i];
					if (this.UpdateMessageEvent != null)
					{
						this.UpdateMessageEvent(this, new UploadMessageEventArgs("Deleting album " + albumHashCode.AlbumName, true, false));
					}
					this.PhotoAdmin.DeleteAlbum(albumHashCode.AlbumName);
					if (this.UpdateMessageEvent != null)
					{
						this.UpdateMessageEvent(this, new UploadMessageEventArgs("   Deleted album " + albumHashCode.AlbumName, true, true));
					}
				}
			}
		}
		private void AddAllAlbums(string[] albums)
		{
			checked
			{
				for (int i = 0; i < albums.Length; i++)
				{
					string albumName = albums[i];
					this.UpdateAlbum(albumName, null);
				}
			}
		}
		private void DeleteAllPhotos(string albumName, PhotoHashCode[] serverHashCodes)
		{
			if (serverHashCodes == null)
			{
				return;
			}
			checked
			{
				for (int i = 0; i < serverHashCodes.Length; i++)
				{
					PhotoHashCode photoHashCode = serverHashCodes[i];
					if (this.UpdateMessageEvent != null)
					{
						this.UpdateMessageEvent(this, new UploadMessageEventArgs("Deleting photo " + photoHashCode.ImageFileName, true, false));
					}
					this.PhotoAdmin.DeletePhoto(albumName, photoHashCode.ImageFileName);
					if (this.UpdateMessageEvent != null)
					{
						this.UpdateMessageEvent(this, new UploadMessageEventArgs("   Deleted photo " + photoHashCode.ImageFileName, true, true));
					}
				}
			}
		}
		private void AddAllPhotos(string albumName, string[] localPhotos)
		{
			checked
			{
				for (int i = 0; i < localPhotos.Length; i++)
				{
					string photoName = localPhotos[i];
					this.UpdatePhoto(albumName, photoName, null);
				}
			}
		}
		private void SyncPhotos(string albumName)
		{
			PhotoHashCode[] photoHashCodes = this.PhotoAdmin.GetPhotoHashCodes(albumName);
			string[] photos = Publish.GetPhotos(albumName);
			if (photos == null)
			{
				this.DeleteAllPhotos(albumName, photoHashCodes);
				return;
			}
			if (photoHashCodes == null)
			{
				this.AddAllPhotos(albumName, photos);
				return;
			}
			Array.Sort(photos);
			checked
			{
				string[] array = new string[photos.Length - 1 + 1];
				Array.Copy(photos, array, photos.Length);
				PhotoHashCode[] array2 = photoHashCodes;
				for (int i = 0; i < array2.Length; i++)
				{
					PhotoHashCode photoHashCode = array2[i];
					int num = Array.BinarySearch(photos, photoHashCode.ImageFileName);
					if (num < 0)
					{
						if (this.UpdateMessageEvent != null)
						{
							this.UpdateMessageEvent(this, new UploadMessageEventArgs("Deleting photo " + photoHashCode.ImageFileName, true, false));
						}
						this.PhotoAdmin.DeletePhoto(albumName, photoHashCode.ImageFileName);
						if (this.UpdateMessageEvent != null)
						{
							this.UpdateMessageEvent(this, new UploadMessageEventArgs("   Deleted photo " + photoHashCode.ImageFileName, true, true));
						}
					}
					else
					{
						this.UpdatePhoto(albumName, photoHashCode.ImageFileName, photoHashCode);
						array[num] = null;
					}
				}
				string[] array3 = array;
				for (int j = 0; j < array3.Length; j++)
				{
					string text = array3[j];
					if (text != null)
					{
						this.UpdatePhoto(albumName, text, null);
					}
				}
			}
		}
		private void UpdatePhoto(string albumName, string photoName, PhotoHashCode hashCode)
		{
			if (this.UpdateMessageEvent != null)
			{
				this.UpdateMessageEvent(this, new UploadMessageEventArgs("Processing photo " + photoName, true, false));
			}
			PhotoHashCode photoHashCode = Publish.GetPhotoHashCode(albumName, photoName);
			byte[] array = null;
			if (hashCode == null || StringType.StrCmp(photoHashCode.ImageHash, hashCode.ImageHash, false) != 0)
			{
				array = this.ReadFile(photoHashCode.ImageFileName);
			}
			byte[] array2 = null;
			if (hashCode == null || StringType.StrCmp(photoHashCode.MetaDataHash, hashCode.MetaDataHash, false) != 0)
			{
				array2 = this.ReadFile(photoHashCode.MetaDataFileName);
			}
			if (hashCode == null || array != null || array2 != null)
			{
				this.PhotoAdmin.UpdatePhoto(albumName, photoName, array, array2);
				if (array == null)
				{
					if (this.UpdateMessageEvent != null)
					{
						this.UpdateMessageEvent(this, new UploadMessageEventArgs("   Uploaded photo " + photoName + " (data)", true, true));
					}
				}
				else
				{
					if (this.UpdateMessageEvent != null)
					{
						this.UpdateMessageEvent(this, new UploadMessageEventArgs("   Uploaded photo " + photoName, true, true));
					}
				}
			}
			if (this.UpdateProgressEvent != null)
			{
				this.UpdateProgressEvent(this, new UploadProgressEventArgs(this._progressPos, this._progressCount));
			}
			checked
			{
				this._progressPos++;
			}
		}
		private void UpdateAlbum(string albumName, AlbumHashCode hashCode)
		{
			if (this.UpdateMessageEvent != null)
			{
				this.UpdateMessageEvent(this, new UploadMessageEventArgs("Processing album " + albumName, true, false));
			}
			AlbumHashCode albumHashCode = Publish.GetAlbumHashCode(albumName);
			byte[] array = null;
			if (hashCode == null || StringType.StrCmp(albumHashCode.MetaDataHash, hashCode.MetaDataHash, false) != 0)
			{
				array = this.ReadFile(albumHashCode.MetaDataFileName);
			}
			if (hashCode == null || array != null)
			{
				this.PhotoAdmin.UpdateAlbum(albumName, array);
				if (hashCode == null)
				{
					if (this.UpdateMessageEvent != null)
					{
						this.UpdateMessageEvent(this, new UploadMessageEventArgs("   Created album " + albumName, true, true));
					}
				}
				else
				{
					if (this.UpdateMessageEvent != null)
					{
						this.UpdateMessageEvent(this, new UploadMessageEventArgs("   Uploaded album " + albumName + " (data)", true, true));
					}
				}
			}
			if (this.UpdateProgressEvent != null)
			{
				this.UpdateProgressEvent(this, new UploadProgressEventArgs(this._progressPos, this._progressCount));
			}
			checked
			{
				this._progressPos++;
			}
		}
		private byte[] ReadFile(string filePath)
		{
			if (!File.Exists(filePath))
			{
				return null;
			}
			checked
			{
				byte[] result;
                BinaryReader binaryReader = null;
                FileStream fileStream = null;
				try
				{
					FileInfo fileInfo = new FileInfo(filePath);
					byte[] array = new byte[(int)fileInfo.Length + 1];
                    fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
					binaryReader = new BinaryReader(fileStream);
					array = binaryReader.ReadBytes((int)fileInfo.Length);
					result = array;
				}
				finally
				{
					if (binaryReader != null)
					{
						binaryReader.Close();
					}

					if (fileStream != null)
					{
						fileStream.Close();
					}
				}
				return result;
			}
		}
		private void AttachCredentials()
		{
			this._adminHeader = new AdminHeader();
			this._adminHeader.Password = Hash.ComputeHash(DataProtection.Decrypt(Global.Settings.GetString(SettingKey.ServicePassword), DataProtection.Store.User));
			this._photoAdmin.AdminHeaderValue = this._adminHeader;
		}
	}
}
