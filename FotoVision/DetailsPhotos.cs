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
					this.__textTitle.remove_Leave(new EventHandler(this.textControl_Leave));
					this.__textTitle.PreviousControl -= new TabTextBox.PreviousControlEventHandler(this.textTitle_PreviousControl);
				}
				this.__textTitle = value;
				if (this.__textTitle != null)
				{
					this.__textTitle.add_Leave(new EventHandler(this.textControl_Leave));
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
					this.__textDesc.remove_Leave(new EventHandler(this.textControl_Leave));
				}
				this.__textDesc = value;
				if (this.__textDesc != null)
				{
					this.__textDesc.add_Leave(new EventHandler(this.textControl_Leave));
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
					this.__textDate.remove_Leave(new EventHandler(this.textControl_Leave));
					this.__textDate.NextControl -= new TabTextBox.NextControlEventHandler(this.textDate_NextControl);
				}
				this.__textDate = value;
				if (this.__textDate != null)
				{
					this.__textDate.add_Leave(new EventHandler(this.textControl_Leave));
					this.__textDate.NextControl += new TabTextBox.NextControlEventHandler(this.textDate_NextControl);
				}
			}
		}
		public DetailsPhotos()
		{
			this._processingIndexChanged = false;
			this._curIndex = -1;
			this.set_BackColor(SystemColors.get_Control());
			this.set_DrawMode(1);
			this.set_ItemHeight(checked(this.get_Font().get_Height() * 8 + 35 + 19));
			this._textHeight = this.get_Font().get_Height();
			this.InitChildControls();
			this._format = new StringFormat();
			this._format.set_Trimming(3);
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
						this.get_Items().Add(photo);
					}
				}
			}
		}
		public void Clear()
		{
			this._curIndex = -1;
			this.HideControls();
			this.get_Items().Clear();
		}
		public void Save()
		{
			this.UpdateItem(this._curIndex);
		}
		private void textTitle_PreviousControl(object sender, TabbedControlNavigateEventArgs e)
		{
			e.Processed = false;
			if (this.get_SelectedIndex() != -1 & this.get_SelectedIndex() > 0)
			{
				this.set_SelectedIndex(checked(this.get_SelectedIndex() - 1));
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
				if (this.get_SelectedIndex() != -1 & this.get_SelectedIndex() < this.get_Items().get_Count() - 1)
				{
					this.set_SelectedIndex(this.get_SelectedIndex() + 1);
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
			if (this.get_SelectedIndex() != -1)
			{
				this.UpdateItem(this.get_SelectedIndex());
			}
		}
		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			base.OnDrawItem(e);
			if (this.get_DesignMode())
			{
				return;
			}
			if (e.get_Index() == -1)
			{
				return;
			}
			this.CreateOffscreenGraphics(e.get_Index());
			this._graphics.Clear(this.get_BackColor());
			this.DrawFields(this._graphics, 0);
			this.DrawValues(this._graphics, e.get_Index(), 0);
			this.DrawThumbnail(this._graphics, e.get_Index(), 0);
			e.get_Graphics().DrawImage(this._bmpItem, e.get_Bounds().get_Left(), e.get_Bounds().get_Top(), this._bmpItem.get_Width(), this._bmpItem.get_Height());
		}
		private void DrawFields(Graphics g, int top)
		{
			checked
			{
				Point point = new Point(70, top + 5);
				g.DrawString("Photo Title & Description:", this.get_Font(), SystemBrushes.get_ControlText(), (float)point.get_X(), (float)point.get_Y());
				point.set_Y(point.get_Y() + (this._textHeight + 2));
				this._titleOffset = point.get_Y() - top;
				g.FillRectangle(SystemBrushes.get_Window(), point.get_X(), point.get_Y(), 200, this._textHeight + 4);
				g.DrawRectangle(SystemPens.get_ControlDark(), point.get_X(), point.get_Y(), 200, this._textHeight + 4);
				point.set_Y(point.get_Y() + (this._textHeight + 4 + 5));
				this._descOffset = point.get_Y() - top;
				g.FillRectangle(SystemBrushes.get_Window(), point.get_X(), point.get_Y(), 200, this._textHeight * 4 + 2);
				g.DrawRectangle(SystemPens.get_ControlDark(), point.get_X(), point.get_Y(), 200, this._textHeight * 4 + 2);
				point.set_Y(point.get_Y() + (this._textHeight * 4 + 6 + 5));
				g.DrawString("Date Taken", this.get_Font(), SystemBrushes.get_ControlText(), (float)point.get_X(), (float)point.get_Y());
				int width = g.MeasureString("Date Taken", this.get_Font()).ToSize().get_Width();
				g.DrawString("(default: date created):", this.get_Font(), SystemBrushes.get_ControlText(), (float)(point.get_X() + width + 2), (float)point.get_Y());
				point.set_Y(point.get_Y() + (this._textHeight + 2));
				this._dateOffset = point.get_Y() - top;
				g.FillRectangle(SystemBrushes.get_Window(), point.get_X(), point.get_Y(), 100, this._textHeight + 4);
				g.DrawRectangle(SystemPens.get_ControlDark(), point.get_X(), point.get_Y(), 100, this._textHeight + 4);
				point.set_Y(point.get_Y() + (this._textHeight + 4 + 10));
				g.DrawLine(SystemPens.get_ControlDark(), 10, point.get_Y(), point.get_X() + 200, point.get_Y());
			}
		}
		private void DrawValues(Graphics g, int index, int top)
		{
			if (index >= this.get_Items().get_Count())
			{
				return;
			}
			Photo photo = (Photo)this.get_Items().get_Item(index);
			g.set_TextRenderingHint(1);
			string arg_64_1 = photo.Title;
			Font arg_64_2 = this.get_Font();
			Brush arg_64_3 = SystemBrushes.get_WindowText();
			checked
			{
				RectangleF rectangleF = new RectangleF(72f, (float)(top + this._titleOffset + 2), 200f, (float)this._textHeight);
				g.DrawString(arg_64_1, arg_64_2, arg_64_3, rectangleF, this._format);
				string arg_A7_1 = photo.Description;
				Font arg_A7_2 = this.get_Font();
				Brush arg_A7_3 = SystemBrushes.get_WindowText();
				rectangleF = new RectangleF(72f, (float)(top + this._descOffset + 2), 200f, (float)(this._textHeight * 4));
				g.DrawString(arg_A7_1, arg_A7_2, arg_A7_3, rectangleF, this._format);
				string arg_EA_1 = photo.DateTaken;
				Font arg_EA_2 = this.get_Font();
				Brush arg_EA_3 = SystemBrushes.get_WindowText();
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
					if (index < this.get_Items().get_Count())
					{
						Photo photo = (Photo)this.get_Items().get_Item(index);
						Image thumbnail = PhotoHelper.GetThumbnail(photo.ThumbnailPath, 50);
						int num = (50 - thumbnail.get_Width()) / 2;
						Rectangle rectangle = new Rectangle(5 + num + 3, 5 + top + 3, thumbnail.get_Width(), thumbnail.get_Height());
						g.DrawImage(thumbnail, rectangle);
						g.DrawRectangle(SystemPens.get_ControlDark(), rectangle);
						rectangle.Inflate(-1, -1);
						g.DrawRectangle(SystemPens.get_ControlDark(), rectangle);
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
			if (this._curIndex != this.get_SelectedIndex())
			{
				this.UpdateItem(this._curIndex);
				this._curIndex = this.get_SelectedIndex();
			}
		}
		private void UpdateItem(int index)
		{
			if (index == -1)
			{
				return;
			}
			Photo photo = (Photo)this.get_Items().get_Item(index);
			photo.Title = this._textTitle.get_Text();
			photo.Description = this._textDesc.get_Text();
			if (Global.ValidateDate(this._textDate.get_Text().Trim()))
			{
				photo.DateTaken = this._textDate.get_Text().Trim();
			}
			photo.WriteXml();
			if (this.PhotoMetadataChangedEvent != null)
			{
				this.PhotoMetadataChangedEvent(this, new PhotoMetadataChangedEventArgs(photo));
			}
		}
		private void HideControls()
		{
			this._textTitle.set_Visible(false);
			this._textDesc.set_Visible(false);
			this._textDate.set_Visible(false);
		}
		private void ShowControls()
		{
			if (this.get_SelectedIndex() == -1)
			{
				this._curIndex = -1;
				this.HideControls();
				return;
			}
			this.CheckCurrentItem();
			Photo photo = (Photo)this.get_Items().get_Item(this.get_SelectedIndex());
			Rectangle itemRectangle = this.GetItemRectangle(this.get_SelectedIndex());
			this.HideControls();
			this._textTitle.set_Left(74);
			checked
			{
				this._textTitle.set_Top(itemRectangle.get_Top() + this._titleOffset + 2);
				this._textTitle.set_Text(photo.Title);
				this._textTitle.set_Visible(true);
				this._textDesc.set_Left(74);
				this._textDesc.set_Top(itemRectangle.get_Top() + this._descOffset + 2);
				this._textDesc.set_Text(photo.Description);
				this._textDesc.set_Visible(true);
				this._textDate.set_Left(74);
				this._textDate.set_Top(itemRectangle.get_Top() + this._dateOffset + 2);
				this._textDate.set_Text(photo.DateTaken);
				this._textDate.set_Visible(true);
			}
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			Point point = new Point(e.get_X(), e.get_Y());
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
			this._textTitle.set_Visible(false);
			this._textDesc.set_Visible(false);
			this._textDate.set_Visible(false);
			this._textTitle.set_BorderStyle(0);
			this._textDesc.set_BorderStyle(0);
			this._textDate.set_BorderStyle(0);
			this._textDesc.set_Multiline(true);
			this._textDesc.set_WordWrap(true);
			this._textDesc.set_ScrollBars(2);
			this._textTitle.set_Width(196);
			this._textTitle.set_Height(this._textHeight);
			this._textDesc.set_Width(196);
			this._textDesc.set_Height(checked(this._textHeight * 4));
			this._textDate.set_Width(96);
			this._textDate.set_Height(this._textHeight);
			this.get_Controls().Add(this._textTitle);
			this.get_Controls().Add(this._textDesc);
			this.get_Controls().Add(this._textDate);
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
				if (index >= this.get_Items().get_Count())
				{
					return;
				}
				Rectangle itemRectangle = this.GetItemRectangle(index);
				if (this._bmpItem == null || this._bmpItem.get_Width() != itemRectangle.get_Width() || this._bmpItem.get_Height() != itemRectangle.get_Height())
				{
					if (this._bmpItem != null)
					{
						this._bmpItem.Dispose();
					}
					this._bmpItem = new Bitmap(itemRectangle.get_Width(), itemRectangle.get_Height());
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
				if (m.get_Msg() == 20 && this.get_Items().get_Count() > 0)
				{
					Rectangle itemRectangle = this.GetItemRectangle(this.get_Items().get_Count() - 1);
					if (itemRectangle.get_Bottom() == 0)
					{
						return;
					}
					if (itemRectangle.get_Bottom() < this.get_DisplayRectangle().get_Bottom())
					{
						Graphics graphics = Graphics.FromHwnd(this.get_Handle());
						graphics.FillRectangle(SystemBrushes.get_Control(), this.get_DisplayRectangle().get_Left(), itemRectangle.get_Bottom(), this.get_DisplayRectangle().get_Width(), this.get_DisplayRectangle().get_Bottom() - itemRectangle.get_Bottom());
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
