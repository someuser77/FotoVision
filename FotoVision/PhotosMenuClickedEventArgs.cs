using System;
namespace FotoVision
{
	public class PhotosMenuClickedEventArgs : EventArgs
	{
		private PhotosContextAction _action;
		public PhotosContextAction Action
		{
			get
			{
				return this._action;
			}
		}
		public PhotosMenuClickedEventArgs(PhotosContextAction action)
		{
			this._action = action;
		}
	}
}
