using System;
namespace FotoVision
{
	public class UploadProgressEventArgs : EventArgs
	{
		private int _position;
		private int _total;
		public int Position
		{
			get
			{
				return this._position;
			}
		}
		public int Total
		{
			get
			{
				return this._total;
			}
		}
		public UploadProgressEventArgs(int position, int total)
		{
			this._position = position;
			this._total = total;
		}
	}
}
