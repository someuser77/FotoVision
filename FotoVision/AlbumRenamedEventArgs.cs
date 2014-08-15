using System;
namespace FotoVision
{
	public class AlbumRenamedEventArgs : EventArgs
	{
		private string _oldName;
		private string _newName;
		public string NewName
		{
			get
			{
				return this._newName;
			}
		}
		public string OldName
		{
			get
			{
				return this._oldName;
			}
		}
		public AlbumRenamedEventArgs(string oldName, string newName)
		{
			this._oldName = oldName;
			this._newName = newName;
		}
	}
}
