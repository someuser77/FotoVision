using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Windows.Forms;
namespace FotoVision
{
	public class DetailsPhotos : ListBox
	{
		private class Consts
		{
			public const int ItemHeight = 150;
			public const int Buffer = 5;
			public const int FieldsLeft = 70;
			public const int TitleWidth = 200;
			public const int DateTakenWidth = 100;
			public const int ThumbnailSize = 50;
			public const string TitleText = "Photo Title & Description:";
			public const string DateText = "Date Taken";
			public const string DateExtraText = "(default: date created):";
		}
		private class Win32
		{
			public const int WM_ERASEBKGND = 20;
		}
		public delegate void PhotoMetadataChangedEventHandler(object sender, PhotoMetadataChangedEventArgs e);
		private DetailsPhotos.PhotoMetadataChangedEventHandler PhotoMetadataChangedEvent;
		[AccessedThroughProperty("_textDesc")]
		private TextBox __textDesc;
		[AccessedThroughProperty("_textDate")]
		private TabTextBox __textDate;
		[AccessedThroughProperty("_textTitle")]
		private TabTextBox __textTitle;
		private bool _processingIndexChanged;
		private int _titleOffset;
		private int _descOffset;
		private int _dateOffset;
		private int _textHeight;
		private int _curIndex;
		private Bitmap _bmpItem;
		private Graphics _graphics;
		private StringFormat _format;
		public event DetailsPhotos.PhotoMetadataChangedEventHandler PhotoMetadataChanged
		{
			[MethodImpl(32)]
			add
			{
				this.PhotoMetadataChangedEvent = (DetailsPhotos.PhotoMetadataChangedEventHandler)Delegate.Combine(this.PhotoMetadataChangedEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.PhotoMetadataChangedEvent = (DetailsPhotos.PhotoMetadataChangedEventHandler)Delegate.Remove(this.PhotoMetadataChangedEvent, value);
			}
		}
		private TabTextBox _textTitle
		{
			get
			{
				return this.__textTitle;
			}
			[MethodImpl(32)]
			set
			{
				if (this.__textTitle != null)
				{
					this.__textTitle.Leave -= new EventHandler(this.textControl_Leave);
					this.__textTitle.PreviousControl -= new TabTextBox.PreviousControlEventHandler(this.textTitle_PreviousControl);
				}
				this.__textTitle = value;
				if (this.__textTitle != null)
				{
					this.__textTitle.Leave += new EventHandler(this.textControl_Leave);
					this.__textTitle.PreviousControl += new TabTextBox.PreviousControlEventHandler(this.textTitle_PreviousControl);
				}
			}
		}
		private TextBox _textDesc
		{
			get
			{
				return this.__textDesc;
			}
			[MethodImpl(32)]
			set
			{
				if (this.__textDesc != null)
				{
					this.__textDesc.Leave -= new EventHandler(this.textControl_Leave);
				}
				this.__textDesc = value;
				if (this.__textDesc != null)
				{
					this.__textDesc.Leave += new EventHandler(this.textControl_Leave);
				}
			}
		}
		private TabTextBox _textDate
		{
			get
			{
				return this.__textDate;
			}
			[MethodImpl(32)]
			set
			{
				if (this.__textDate != null)
				{
					this.__textDate.Leave -= new EventHandler(this.textControl_Leave);
					this.__textDate.NextControl -= new TabTextBox.NextControlEventHandler(this.textDate_NextControl);
				}
				this.__textDate = value;
				if (this.__textDate != null)
				{
					this.__textDate.Leave += new EventHandler(this.textControl_Leave);
					this.__textDate.NextControl += new TabTextBox.NextControlEventHandler(this.textDate_NextControl);
				}
			}
		}
		public DetailsPhotos()
		{
			this._processingIndexChanged = false;
			this._curIndex = -1;
			this.BackColor = SystemColors.Control;
			this.DrawMode = 1;
			this.ItemHeight = checked(this.Font.Height * 8 + 35 + 19);
			this._textHeight = this.Font.Height;
			this.InitChildControls();
			this._format = new StringFormat();
			this._format.Trimming = 3;
		}
		public void SetPhotos(Photo[] photos)
		{
			this.Clear();
			checked
			{
				if (photos != null)
				{
					for (int i = 0; i < photos.Length; i++)
					{
						Photo photo = photos[i];
						this.Items.Add(photo);
					}
				}
			}
		}
		public void Clear()
		{
			this._curIndex = -1;
			this.HideControls();
			this.Items.Clear();
		}
		public void Save()
		{
			this.UpdateItem(this._curIndex);
		}
		private void textTitle_PreviousControl(object sender, TabbedControlNavigateEventArgs e)
		{
			e.Processed = false;
			if (this.SelectedIndex != -1 & this.SelectedIndex > 0)
			{
				this.SelectedIndex = checked(this.SelectedIndex - 1);
				this._textDate.Focus();
				this._textDate.SelectAll();
				e.Processed = true;
			}
		}
		private void textDate_NextControl(object sender, TabbedControlNavigateEventArgs e)
		{
			e.Processed = false;
			checked
			{
				if (this.SelectedIndex != -1 & this.SelectedIndex < this.Items.Count - 1)
				{
					this.SelectedIndex = this.SelectedIndex + 1;
					this._textTitle.Focus();
					this._textTitle.SelectAll();
					e.Processed = true;
				}
			}
		}
		private void textControl_Leave(object sender, EventArgs e)
		{
			if (this._processingIndexChanged)
			{
				return;
			}
			if (this.SelectedIndex != -1)
			{
				this.UpdateItem(this.SelectedIndex);
			}
		}
		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			base.OnDrawItem(e);
			if (this.DesignMode)
			{
				return;
			}
			if (e.Index == -1)
			{
				return;
			}
			this.CreateOffscreenGraphics(e.Index);
			this._graphics.Clear(this.BackColor);
			this.DrawFields(this._graphics, 0);
			this.DrawValues(this._graphics, e.Index, 0);
			this.DrawThumbnail(this._graphics, e.Index, 0);
			e.Graphics.DrawImage(this._bmpItem, e.Bounds.Left, e.Bounds.Top, this._bmpItem.Width, this._bmpItem.Height);
		}
		private void DrawFields(Graphics g, int top)
		{
			checked
			{
				Point point = new Point(70, top + 5);
				g.DrawString("Photo Title & Description:", this.Font, SystemBrushes.ControlText, (float)point.X, (float)point.Y);
				point.Y = point.Y + (this._textHeight + 2);
				this._titleOffset = point.Y - top;
				g.FillRectangle(SystemBrushes.Window, point.X, point.Y, 200, this._textHeight + 4);
				g.DrawRectangle(SystemPens.ControlDark, point.X, point.Y, 200, this._textHeight + 4);
				point.Y = point.Y + (this._textHeight + 4 + 5);
				this._descOffset = point.Y - top;
				g.FillRectangle(SystemBrushes.Window, point.X, point.Y, 200, this._textHeight * 4 + 2);
				g.DrawRectangle(SystemPens.ControlDark, point.X, point.Y, 200, this._textHeight * 4 + 2);
				point.Y = point.Y + (this._textHeight * 4 + 6 + 5);
				g.DrawString("Date Taken", this.Font, SystemBrushes.ControlText, (float)point.X, (float)point.Y);
				int width = g.MeasureString("Date Taken", this.Font).ToSize().Width;
				g.DrawString("(default: date created):", this.Font, SystemBrushes.ControlText, (float)(point.X + width + 2), (float)point.Y);
				point.Y = point.Y + (this._textHeight + 2);
				this._dateOffset = point.Y - top;
				g.FillRectangle(SystemBrushes.Window, point.X, point.Y, 100, this._textHeight + 4);
				g.DrawRectangle(SystemPens.ControlDark, point.X, point.Y, 100, this._textHeight + 4);
				point.Y = point.Y + (this._textHeight + 4 + 10);
				g.DrawLine(SystemPens.ControlDark, 10, point.Y, point.X + 200, point.Y);
			}
		}
		private void DrawValues(Graphics g, int index, int top)
		{
			if (index >= this.Items.Count)
			{
				return;
			}
			Photo photo = (Photo)this.Items.get_Item(index);
			g.TextRenderingHint = 1;
			string arg_64_1 = photo.Title;
			Font arg_64_2 = this.Font;
			Brush arg_64_3 = SystemBrushes.WindowText;
			checked
			{
				RectangleF rectangleF = new RectangleF(72f, (float)(top + this._titleOffset + 2), 200f, (float)this._textHeight);
				g.DrawString(arg_64_1, arg_64_2, arg_64_3, rectangleF, this._format);
				string arg_A7_1 = photo.Description;
				Font arg_A7_2 = this.Font;
				Brush arg_A7_3 = SystemBrushes.WindowText;
				rectangleF = new RectangleF(72f, (float)(top + this._descOffset + 2), 200f, (float)(this._textHeight * 4));
				g.DrawString(arg_A7_1, arg_A7_2, arg_A7_3, rectangleF, this._format);
				string arg_EA_1 = photo.DateTaken;
				Font arg_EA_2 = this.Font;
				Brush arg_EA_3 = SystemBrushes.WindowText;
				rectangleF = new RectangleF(72f, (float)(top + this._dateOffset + 2), 100f, (float)(this._textHeight * 4));
				g.DrawString(arg_EA_1, arg_EA_2, arg_EA_3, rectangleF, this._format);
			}
		}
		private void DrawThumbnail(Graphics g, int index, int top)
		{
			checked
			{
				try
				{
					if (index < this.Items.Count)
					{
						Photo photo = (Photo)this.Items.get_Item(index);
						Image thumbnail = PhotoHelper.GetThumbnail(photo.ThumbnailPath, 50);
						int num = (50 - thumbnail.Width) / 2;
						Rectangle rectangle = new Rectangle(5 + num + 3, 5 + top + 3, thumbnail.Width, thumbnail.Height);
						g.DrawImage(thumbnail, rectangle);
						g.DrawRectangle(SystemPens.ControlDark, rectangle);
						rectangle.Inflate(-1, -1);
						g.DrawRectangle(SystemPens.ControlDark, rectangle);
					}
				}
				catch (Exception expr_87)
				{
					ProjectData.SetProjectError(expr_87);
					ProjectData.ClearProjectError();
				}
				finally
				{
					Image thumbnail;
					if (thumbnail != null)
					{
						thumbnail.Dispose();
					}
				}
			}
		}
		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			this._processingIndexChanged = true;
			this.ShowControls();
			base.OnSelectedIndexChanged(e);
			this._processingIndexChanged = false;
		}
		private void CheckCurrentItem()
		{
			if (this._curIndex != this.SelectedIndex)
			{
				this.UpdateItem(this._curIndex);
				this._curIndex = this.SelectedIndex;
			}
		}
		private void UpdateItem(int index)
		{
			if (index == -1)
			{
				return;
			}
			Photo photo = (Photo)this.Items.get_Item(index);
			photo.Title = this._textTitle.Text;
			photo.Description = this._textDesc.Text;
			if (Global.ValidateDate(this._textDate.Text.Trim()))
			{
				photo.DateTaken = this._textDate.Text.Trim();
			}
			photo.WriteXml();
			if (this.PhotoMetadataChangedEvent != null)
			{
				this.PhotoMetadataChangedEvent(this, new PhotoMetadataChangedEventArgs(photo));
			}
		}
		private void HideControls()
		{
			this._textTitle.Visible = false;
			this._textDesc.Visible = false;
			this._textDate.Visible = false;
		}
		private void ShowControls()
		{
			if (this.SelectedIndex == -1)
			{
				this._curIndex = -1;
				this.HideControls();
				return;
			}
			this.CheckCurrentItem();
			Photo photo = (Photo)this.Items.Item(this.get_SelectedIndex);
			Rectangle itemRectangle = this.GetItemRectangle(this.SelectedIndex);
			this.HideControls();
			this._textTitle.Left = 74;
			checked
			{
				this._textTitle.Top = itemRectangle.Top + this._titleOffset + 2;
				this._textTitle.Text = photo.Title;
				this._textTitle.Visible = true;
				this._textDesc.Left = 74;
				this._textDesc.Top = itemRectangle.Top + this._descOffset + 2;
				this._textDesc.Text = photo.Description;
				this._textDesc.Visible = true;
				this._textDate.Left = 74;
				this._textDate.Top = itemRectangle.Top + this._dateOffset + 2;
				this._textDate.Text = photo.DateTaken;
				this._textDate.Visible = true;
			}
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			Point point = new Point(e.X, e.Y);
			Control childAtPoint = this.GetChildAtPoint(point);
			if (childAtPoint != null)
			{
				childAtPoint.Focus();
			}
		}
		private void InitChildControls()
		{
			this._textTitle = new TabTextBox();
			this._textDesc = new TextBox();
			this._textDate = new TabTextBox();
			this._textTitle.Visible = false;
			this._textDesc.Visible = false;
			this._textDate.Visible = false;
			this._textTitle.BorderStyle = 0;
			this._textDesc.BorderStyle = 0;
			this._textDate.BorderStyle = 0;
			this._textDesc.Multiline = true;
			this._textDesc.WordWrap = true;
			this._textDesc.ScrollBars = 2;
			this._textTitle.Width = 196;
			this._textTitle.Height = this._textHeight;
			this._textDesc.Width = 196;
			this._textDesc.Height = checked(this._textHeight * 4);
			this._textDate.Width = 96;
			this._textDate.Height = this._textHeight;
			this.Controls.Add(this._textTitle);
			this.Controls.Add(this._textDesc);
			this.Controls.Add(this._textDate);
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			this.Invalidate();
		}
		private void CreateOffscreenGraphics(int index)
		{
			if (index >= 0)
			{
				if (index >= this.Items.Count)
				{
					return;
				}
				Rectangle itemRectangle = this.GetItemRectangle(index);
				if (this._bmpItem == null || this._bmpItem.Width != itemRectangle.Width || this._bmpItem.Height != itemRectangle.Height)
				{
					if (this._bmpItem != null)
					{
						this._bmpItem.Dispose();
					}
					this._bmpItem = new Bitmap(itemRectangle.Width, itemRectangle.Height);
					if (this._graphics != null)
					{
						this._graphics.Dispose();
					}
					this._graphics = Graphics.FromImage(this._bmpItem);
				}
			}
		}
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\r\n               version=\"1\">\r\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\r\n                version=\"1\"\r\n                Flags=\"UnmanagedCode\"/>\r\n</PermissionSet>\r\n")]
		protected override void WndProc(ref Message m)
		{
			checked
			{
				if (m.Msg == 20 && this.Items.Count > 0)
				{
					Rectangle itemRectangle = this.GetItemRectangle(this.Items.Count - 1);
					if (itemRectangle.Bottom == 0)
					{
						return;
					}
					if (itemRectangle.Bottom < this.DisplayRectangle.Bottom)
					{
						Graphics graphics = Graphics.FromHwnd(this.Handle);
						graphics.FillRectangle(SystemBrushes.Control, this.DisplayRectangle.Left, itemRectangle.Bottom, this.DisplayRectangle.Width, this.DisplayRectangle.Bottom - itemRectangle.Bottom);
						graphics.Dispose();
						return;
					}
				}
				else
				{
					base.WndProc(ref m);
				}
			}
		}
	}
}
