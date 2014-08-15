using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace FotoVision
{
	public class BasePane : UserControl
	{
		public delegate void PaneActiveEventHandler(object sender, EventArgs e);
		private BasePane.PaneActiveEventHandler PaneActiveEvent;
		private PaneCaption caption;
		private Container components;
		public event BasePane.PaneActiveEventHandler PaneActive
		{
			[MethodImpl(32)]
			add
			{
				this.PaneActiveEvent = (BasePane.PaneActiveEventHandler)Delegate.Combine(this.PaneActiveEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.PaneActiveEvent = (BasePane.PaneActiveEventHandler)Delegate.Remove(this.PaneActiveEvent, value);
			}
		}
		protected PaneCaption CaptionControl
		{
			get
			{
				return this.caption;
			}
		}
		[Category("Appearance"), Description("The pane caption.")]
		public string CaptionText
		{
			get
			{
				return this.caption.Text;
			}
			set
			{
				this.caption.Text = value;
			}
		}
		public bool Active
		{
			get
			{
				return this.caption.Active;
			}
		}
		public BasePane()
		{
			this.components = null;
			this.SetStyle(73746, true);
			this.InitializeComponent();
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}
		private void InitializeComponent()
		{
			this.caption = new PaneCaption();
			this.SuspendLayout();
			this.caption.Dock = 1;
			this.caption.Font = new Font("Arial", 9f, 1);
			Control arg_48_0 = this.caption;
			Point location = new Point(1, 1);
			arg_48_0.Location = location;
			this.caption.Name = "caption";
			Control arg_72_0 = this.caption;
			Size size = new Size(214, 20);
			arg_72_0.Size = size;
			this.caption.TabIndex = 0;
			this.Controls.Add(this.caption);
			this.DockPadding.All = 1;
			this.Name = "BasePane";
			size = new Size(216, 248);
			this.Size = size;
			this.ResumeLayout(false);
		}
		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);
			this.caption.Active = true;
			if (this.PaneActiveEvent != null)
			{
				this.PaneActiveEvent(this, EventArgs.Empty);
			}
		}
		protected override void OnLeave(EventArgs e)
		{
			base.OnLeave(e);
			this.caption.Active = false;
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			checked
			{
				Rectangle rectangle = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
				rectangle.Inflate(0 - this.DockPadding.All + 1, 0 - this.DockPadding.All + 1);
				e.Graphics.DrawRectangle(SystemPens.ControlDark, rectangle);
				base.OnPaint(e);
			}
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (this.DesignMode)
			{
				this.caption.Width = this.Width;
			}
		}
	}
}
