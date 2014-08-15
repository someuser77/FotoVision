using System;
namespace FotoVision
{
	public class PhotoMetadataChangedEventArgs : EventArgs
	{
		private Photo _photo;
		public Photo Photo
		{
			get
			{
				return this._photo;
			}
		}
		public PhotoMetadataChangedEventArgs(Photo photo)
		{
			this._photo = photo;
		}
	}
}
