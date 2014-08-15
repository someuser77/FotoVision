using System;
namespace FotoVision
{
	public class ProgressUpdateEventArgs : EventArgs
	{
		private string _message;
		private int _position;
		private int _total;
		public string Message
		{
			get
			{
				return this._message;
			}
			set
			{
				this._message = value;
			}
		}
		public int Position
		{
			get
			{
				return this._position;
			}
			set
			{
				this._position = value;
			}
		}
		public int Total
		{
			get
			{
				return this._total;
			}
			set
			{
				this._total = value;
			}
		}
		public ProgressUpdateEventArgs()
		{
		}
		public ProgressUpdateEventArgs(string message, int position, int total)
		{
			this._message = message;
			this._position = position;
			this._total = total;
		}
	}
}
