using System;
namespace FotoVision
{
	public class CommandButtonClickedEventArgs : EventArgs
	{
		private DetailsCommandButton _button;
		public DetailsCommandButton Button
		{
			get
			{
				return this._button;
			}
		}
		public CommandButtonClickedEventArgs(DetailsCommandButton button)
		{
			this._button = button;
		}
	}
}
