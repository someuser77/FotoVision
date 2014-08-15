using System;
using System.Drawing;
namespace FotoVision
{
	public class CropDataChangedEventArgs : EventArgs
	{
		private Size _orgSize;
		private Size _newSize;
		private Rectangle _cropBounds;
		public Size OrgSize
		{
			get
			{
				return this._orgSize;
			}
		}
		public Size NewSize
		{
			get
			{
				return this._newSize;
			}
		}
		public Rectangle CropBounds
		{
			get
			{
				return this._cropBounds;
			}
		}
		public CropDataChangedEventArgs(Size orgSize, Size newSize, Rectangle cropBounds)
		{
			this._orgSize = orgSize;
			this._newSize = newSize;
			this._cropBounds = cropBounds;
		}
	}
}
