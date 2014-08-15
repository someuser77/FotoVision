using Microsoft.VisualBasic;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace FotoVision
{
	public class PaneCaption : UserControl
	{
		private class Consts
		{
			public const int DefaultHeight = 20;
			public const string DefaultFontName = "arial";
			public const int DefaultFontSize = 9;
			public const int PosOffset = 4;
		}
		private bool _active;
		private bool _antiAlias;
		private bool _allowActive;
		private Color _colorActiveText;
		private Color _colorInactiveText;
		private Color _colorActiveLow;
		private Color _colorActiveHigh;
		private Color _colorInactiveLow;
		private Color _colorInactiveHigh;
		private SolidBrush _brushActiveText;
		private SolidBrush _brushInactiveText;
		private LinearGradientBrush _brushActive;
		private LinearGradientBrush _brushInactive;
		private StringFormat _format;
		private IContainer components;
		[Browsable(true), Category("Appearance"), Description("Text that is displayed in the label."), DesignerSerializationVisibility]
		public string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.set_Text(value);
				this.Invalidate();
			}
		}
		[Category("Appearance"), DefaultValue(false), Description("The active state of the caption, draws the caption with different gradient colors.")]
		public bool Active
		{
			get
			{
				return this._active;
			}
			set
			{
				this._active = value;
				this.Invalidate();
			}
		}
		[Category("Appearance"), DefaultValue(true), Description("True always uses the inactive state colors, false maintains an active and inactive state.")]
		public bool AllowActive
		{
			get
			{
				return this._allowActive;
			}
			set
			{
				this._allowActive = value;
				this.Invalidate();
			}
		}
		[Category("Appearance"), DefaultValue(true), Description("If should draw the text as antialiased.")]
		public bool AntiAlias
		{
			get
			{
				return this._antiAlias;
			}
			set
			{
				this._antiAlias = value;
				this.Invalidate();
			}
		}
		[Category("Appearance"), DefaultValue(typeof(Color), "Black"), Description("Color of the text when active.")]
		public Color ActiveTextColor
		{
			get
			{
				return this._colorActiveText;
			}
			set
			{
				if (value.Equals(Color.Empty))
				{
					value = Color.Black;
				}
				this._colorActiveText = value;
				this._brushActiveText = new SolidBrush(this._colorActiveText);
				this.Invalidate();
			}
		}
		[Category("Appearance"), DefaultValue(typeof(Color), "White"), Description("Color of the text when inactive.")]
		public Color InactiveTextColor
		{
			get
			{
				return this._colorInactiveText;
			}
			set
			{
				if (value.Equals(Color.Empty))
				{
					value = Color.White;
				}
				this._colorInactiveText = value;
				this._brushInactiveText = new SolidBrush(this._colorInactiveText);
				this.Invalidate();
			}
		}
		[Category("Appearance"), DefaultValue(typeof(Color), "255, 165, 78"), Description("Low color of the active gradient.")]
		public Color ActiveGradientLowColor
		{
			get
			{
				return this._colorActiveLow;
			}
			set
			{
				if (value.Equals(Color.Empty))
				{
					value = Color.FromArgb(255, 165, 78);
				}
				this._colorActiveLow = value;
				this.CreateGradientBrushes();
				this.Invalidate();
			}
		}
		[Category("Appearance"), DefaultValue(typeof(Color), "255, 225, 155"), Description("High color of the active gradient.")]
		public Color ActiveGradientHighColor
		{
			get
			{
				return this._colorActiveHigh;
			}
			set
			{
				if (value.Equals(Color.Empty))
				{
					value = Color.FromArgb(255, 225, 155);
				}
				this._colorActiveHigh = value;
				this.CreateGradientBrushes();
				this.Invalidate();
			}
		}
		[Category("Appearance"), DefaultValue(typeof(Color), "3, 55, 145"), Description("Low color of the inactive gradient.")]
		public Color InactiveGradientLowColor
		{
			get
			{
				return this._colorInactiveLow;
			}
			set
			{
				if (value.Equals(Color.Empty))
				{
					value = Color.FromArgb(3, 55, 145);
				}
				this._colorInactiveLow = value;
				this.CreateGradientBrushes();
				this.Invalidate();
			}
		}
		[Category("Appearance"), DefaultValue(typeof(Color), "90, 135, 215"), Description("High color of the inactive gradient.")]
		public Color InactiveGradientHighColor
		{
			get
			{
				return this._colorInactiveHigh;
			}
			set
			{
				if (value.Equals(Color.Empty))
				{
					value = Color.FromArgb(90, 135, 215);
				}
				this._colorInactiveHigh = value;
				this.CreateGradientBrushes();
				this.Invalidate();
			}
		}
		private SolidBrush TextBrush
		{
			get
			{
				return (SolidBrush)Interaction.IIf(this._active && this._allowActive, this._brushActiveText, this._brushInactiveText);
			}
		}
		private LinearGradientBrush BackBrush
		{
			get
			{
				return (LinearGradientBrush)Interaction.IIf(this._active && this._allowActive, this._brushActive, this._brushInactive);
			}
		}
		public PaneCaption()
		{
			this._active = false;
			this._antiAlias = true;
			this._allowActive = true;
			this._colorActiveText = Color.Black;
			this._colorInactiveText = Color.White;
			this._colorActiveLow = Color.FromArgb(255, 165, 78);
			this._colorActiveHigh = Color.FromArgb(255, 225, 155);
			this._colorInactiveLow = Color.FromArgb(3, 55, 145);
			this._colorInactiveHigh = Color.FromArgb(90, 135, 215);
			this.InitializeComponent();
			this.SetStyle(73746, true);
			this.set_Height(20);
			this._format = new StringFormat();
			this._format.set_FormatFlags(4096);
			this._format.set_LineAlignment(1);
			this._format.set_Trimming(3);
			this.set_Font(new Font("arial", 9f, 1));
			this.ActiveTextColor = this._colorActiveText;
			this.InactiveTextColor = this._colorInactiveText;
			this.CreateGradientBrushes();
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			this.DrawCaption(e.Graphics);
			base.OnPaint(e);
		}
		private void DrawCaption(Graphics g)
		{
			g.FillRectangle(this.BackBrush, this.DisplayRectangle);
			if (this._antiAlias)
			{
				g.set_TextRenderingHint(4);
			}
			RectangleF rectangleF = new RectangleF(4f, 0f, (float)checked(this.DisplayRectangle.Width - 4), (float)this.DisplayRectangle.Height);
			RectangleF rectangleF2 = rectangleF;
			g.DrawString(this.Text, this.Font, this.TextBrush, rectangleF2, this._format);
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (this._allowActive)
			{
				this.Focus();
			}
		}
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			this.CreateGradientBrushes();
		}
		private void CreateGradientBrushes()
		{
			if (this.Width > 0 && this.Height > 0)
			{
				if (this._brushActive != null)
				{
					this._brushActive.Dispose();
				}
				this._brushActive = new LinearGradientBrush(this.DisplayRectangle, this._colorActiveHigh, this._colorActiveLow, 1);
				if (this._brushInactive != null)
				{
					this._brushInactive.Dispose();
				}
				this._brushInactive = new LinearGradientBrush(this.DisplayRectangle, this._colorInactiveHigh, this._colorInactiveLow, 1);
			}
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}
		[DebuggerStepThrough]
		private void InitializeComponent()
		{
			this.set_Name("PaneCaption");
			Size size = new Size(150, 30);
			this.set_Size(size);
		}
	}
}
