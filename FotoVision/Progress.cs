using System;
using System.Runtime.CompilerServices;
namespace FotoVision
{
	public class Progress
	{
		public delegate void ProgressCompleteEventHandler(object sender, EventArgs e);
		public delegate void ProgressUpdateEventHandler(object sender, ProgressUpdateEventArgs e);
		private Progress.ProgressUpdateEventHandler ProgressUpdateEvent;
		private Progress.ProgressCompleteEventHandler ProgressCompleteEvent;
		private ProgressUpdateEventArgs _updateArg;
		public event Progress.ProgressCompleteEventHandler ProgressComplete
		{
			[MethodImpl(32)]
			add
			{
				this.ProgressCompleteEvent = (Progress.ProgressCompleteEventHandler)Delegate.Combine(this.ProgressCompleteEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.ProgressCompleteEvent = (Progress.ProgressCompleteEventHandler)Delegate.Remove(this.ProgressCompleteEvent, value);
			}
		}
		public event Progress.ProgressUpdateEventHandler ProgressUpdate
		{
			[MethodImpl(32)]
			add
			{
				this.ProgressUpdateEvent = (Progress.ProgressUpdateEventHandler)Delegate.Combine(this.ProgressUpdateEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.ProgressUpdateEvent = (Progress.ProgressUpdateEventHandler)Delegate.Remove(this.ProgressUpdateEvent, value);
			}
		}
		public Progress()
		{
			this._updateArg = new ProgressUpdateEventArgs();
		}
		public void Update(object sender, string message, int pos, int total)
		{
			this._updateArg.Message = message;
			this._updateArg.Position = pos;
			this._updateArg.Total = total;
			if (this.ProgressUpdateEvent != null)
			{
				this.ProgressUpdateEvent(RuntimeHelpers.GetObjectValue(sender), this._updateArg);
			}
		}
		public void Complete(object sender)
		{
			if (this.ProgressCompleteEvent != null)
			{
				this.ProgressCompleteEvent(RuntimeHelpers.GetObjectValue(sender), EventArgs.Empty);
			}
		}
	}
}
