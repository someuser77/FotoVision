using System;
namespace FotoVision
{
	public class TabbedControlNavigateEventArgs : EventArgs
	{
		private bool _processed;
		public bool Processed
		{
			get
			{
				return this._processed;
			}
			set
			{
				this._processed = value;
			}
		}
		public TabbedControlNavigateEventArgs(bool processed)
		{
			this._processed = processed;
		}
	}
}
