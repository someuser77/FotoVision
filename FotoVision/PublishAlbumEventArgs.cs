using System;
namespace FotoVision
{
	public class PublishAlbumEventArgs : EventArgs
	{
		private string _albumName;
		private bool _publish;
		public string AlbumName
		{
			get
			{
				return this._albumName;
			}
		}
		public bool Publish
		{
			get
			{
				return this._publish;
			}
		}
		public PublishAlbumEventArgs(string albumName, bool publish)
		{
			this._albumName = albumName;
			this._publish = publish;
		}
	}
}
