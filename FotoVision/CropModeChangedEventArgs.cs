using System;
namespace FotoVision
{
	public class CropModeChangedEventArgs : EventArgs
	{
		private bool _cropMode;
		public bool CropMode
		{
			get
			{
				return this._cropMode;
			}
		}
		public CropModeChangedEventArgs(bool cropMode)
		{
			this._cropMode = cropMode;
		}
	}
}
