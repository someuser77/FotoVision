using System;
namespace FotoVision
{
	public class UploadCompleteEventArgs : EventArgs
	{
		private bool _error;
		public bool ErrorOccurred
		{
			get
			{
				return this._error;
			}
		}
		public UploadCompleteEventArgs(bool errorOccurred)
		{
			this._error = errorOccurred;
		}
	}
}
