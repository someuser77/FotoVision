using System;
namespace FotoVision
{
	public class AlbumMetadataChangedEventArgs : EventArgs
	{
		private string _oldName;
		private Album _album;
		public string OldName
		{
			get
			{
				return this._oldName;
			}
		}
		public Album Album
		{
			get
			{
				return this._album;
			}
		}
		public AlbumMetadataChangedEventArgs(string oldName, Album album)
		{
			this._oldName = oldName;
			this._album = album;
		}
	}
}
