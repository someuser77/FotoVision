using System;
namespace FotoVision
{
	public class FilesDroppedEventArgs : EventArgs
	{
		private string[] _files;
		public string[] GetFiles()
		{
			return this._files;
		}
		public FilesDroppedEventArgs(string[] files)
		{
			this._files = files;
		}
	}
}
