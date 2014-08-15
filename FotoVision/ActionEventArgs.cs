using System;
namespace FotoVision
{
	public class ActionEventArgs : EventArgs
	{
		private ActionItem _actionItem;
		public ActionItem ActionItem
		{
			get
			{
				return this._actionItem;
			}
		}
		public ActionEventArgs(ActionItem actionItem)
		{
			this._actionItem = actionItem;
		}
		public ActionEventArgs(PhotoAction action)
		{
			this._actionItem = new ActionItem(action);
		}
	}
}
