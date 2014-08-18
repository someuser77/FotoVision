using System;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Windows.Forms;
namespace FotoVision
{
	public class TabTextBox : TextBox
	{
		public delegate void NextControlEventHandler(object sender, TabbedControlNavigateEventArgs e);
		public delegate void PreviousControlEventHandler(object sender, TabbedControlNavigateEventArgs e);
		private enum Win32
		{
			WM_KEYDOWN = 256
		}
		private TabTextBox.NextControlEventHandler NextControlEvent;
		private TabTextBox.PreviousControlEventHandler PreviousControlEvent;
		public event TabTextBox.NextControlEventHandler NextControl
		{
			[MethodImpl(32)]
			add
			{
				this.NextControlEvent = (TabTextBox.NextControlEventHandler)Delegate.Combine(this.NextControlEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.NextControlEvent = (TabTextBox.NextControlEventHandler)Delegate.Remove(this.NextControlEvent, value);
			}
		}
		public event TabTextBox.PreviousControlEventHandler PreviousControl
		{
			[MethodImpl(32)]
			add
			{
				this.PreviousControlEvent = (TabTextBox.PreviousControlEventHandler)Delegate.Combine(this.PreviousControlEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.PreviousControlEvent = (TabTextBox.PreviousControlEventHandler)Delegate.Remove(this.PreviousControlEvent, value);
			}
		}
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\r\n               version=\"1\">\r\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\r\n                version=\"1\"\r\n                Flags=\"UnmanagedCode\"/>\r\n</PermissionSet>\r\n")]
		public override bool PreProcessMessage(ref Message msg)
		{
			if (msg.Msg == 256 & msg.WParam.ToInt32() == 9)
			{
				TabbedControlNavigateEventArgs tabbedControlNavigateEventArgs = new TabbedControlNavigateEventArgs(false);
                if (Control.ModifierKeys == Keys.Shift)
				{
					if (this.PreviousControlEvent != null)
					{
						this.PreviousControlEvent(this, tabbedControlNavigateEventArgs);
					}
				}
				else
				{
					if (this.NextControlEvent != null)
					{
						this.NextControlEvent(this, tabbedControlNavigateEventArgs);
					}
				}
				if (tabbedControlNavigateEventArgs.Processed)
				{
					return true;
				}
			}
			return base.PreProcessMessage(ref msg);
		}
	}
}
