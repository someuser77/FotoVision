using System;
namespace FotoVision
{
	public class UploadMessageEventArgs : EventArgs
	{
		private string _message;
		private bool _success;
		private bool _log;
		public string Message
		{
			get
			{
				return this._message;
			}
		}
		public bool Success
		{
			get
			{
				return this._success;
			}
		}
		public bool Log
		{
			get
			{
				return this._log;
			}
		}
		public UploadMessageEventArgs(string message, bool success, bool log)
		{
			this._message = message;
			this._success = success;
			this._log = log;
		}
	}
}
