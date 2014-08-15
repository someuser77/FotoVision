using System;
namespace FotoVision
{
	public class AlbumNameEventArgs : EventArgs
	{
		private string _albumName;
		public string AlbumName
		{
			get
			{
				return this._albumName;
			}
		}
		public AlbumNameEventArgs(string albumName)
		{
			this._albumName = albumName;
		}
	}
}
