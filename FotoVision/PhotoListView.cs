using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Windows.Forms;
namespace FotoVision
{
	public class PhotoListView : ListView
	{
		private class Consts
		{
			public const int SelectedFrameSize = 3;
			public static Color FrameColor = Color.FromArgb(225, 223, 208);
			public static Color SelectedColor = Color.FromArgb(240, 237, 219);
			public static Color BackColor = Color.get_DarkGray();
		}
		public delegate void FilesDroppedEventHandler(object sender, FilesDroppedEventArgs e);
		public delegate void FilesDraggedEventHandler(object sender, EventArgs e);
		public delegate void PhotoMetadataChangedEventHandler(object sender, PhotoMetadataChangedEventArgs e);
		private class Win32
		{
			public enum Consts
			{
				WM_NCPAINT = 133,
				WM_ERASEBKGND = 20,
				WM_NOTIFY = 78,
				OCM_BASE = 8192,
				OCM_NOTIFY = 8270,
				NM_CUSTOMDRAW = -12,
				NM_SETFOCUS = -7,
				LVN_ITEMCHANGED = -101,
				CDRF_DODEFAULT = 0,
				CDRF_SKIPDEFAULT = 4,
				CDRF_NOTIFYITEMDRAW = 32,
				CDDS_PREPAINT = 1,
				CDDS_ITEM = 65536,
				CDDS_ITEMPREPAINT
			}
			public struct NMHDR
			{
				public IntPtr hwndFrom;
				public int idFrom;
				public int code;
			}
			public struct RECT
			{
				public int left;
				public int top;
				public int right;
				public int bottom;
			}
			public struct NMCUSTOMDRAW
			{
				public PhotoListView.Win32.NMHDR hdr;
				public int dwDrawStage;
				public IntPtr hdc;
				public PhotoListView.Win32.RECT rc;
				public int dwItemSpec;
				public int uItemState;
				public IntPtr lItemlParam;
			}
		}
		private PhotoListView.FilesDraggedEventHandler FilesDraggedEvent;
		private PhotoListView.FilesDroppedEventHandler FilesDroppedEvent;
		private PhotoListView.PhotoMetadataChangedEventHandler PhotoMetadataChangedEvent;
		private ImageList _imageList;
		private DropData _dropData;
		private bool _paintBackground;
		private bool _backgroundDirty;
		private Point _topItemPos;
		private Pen _penFrame;
		private Pen _penSelected;
		private Pen _penBack;
		private SolidBrush _brushSelected;
		private SolidBrush _brushBack;
		private StringFormat _format;
		public event PhotoListView.FilesDroppedEventHandler FilesDropped
		{
			[MethodImpl(32)]
			add
			{
				this.FilesDroppedEvent = (PhotoListView.FilesDroppedEventHandler)Delegate.Combine(this.FilesDroppedEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.FilesDroppedEvent = (PhotoListView.FilesDroppedEventHandler)Delegate.Remove(this.FilesDroppedEvent, value);
			}
		}
		public event PhotoListView.FilesDraggedEventHandler FilesDragged
		{
			[MethodImpl(32)]
			add
			{
				this.FilesDraggedEvent = (PhotoListView.FilesDraggedEventHandler)Delegate.Combine(this.FilesDraggedEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.FilesDraggedEvent = (PhotoListView.FilesDraggedEventHandler)Delegate.Remove(this.FilesDraggedEvent, value);
			}
		}
		public event PhotoListView.PhotoMetadataChangedEventHandler PhotoMetadataChanged
		{
			[MethodImpl(32)]
			add
			{
				this.PhotoMetadataChangedEvent = (PhotoListView.PhotoMetadataChangedEventHandler)Delegate.Combine(this.PhotoMetadataChangedEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.PhotoMetadataChangedEvent = (PhotoListView.PhotoMetadataChangedEventHandler)Delegate.Remove(this.PhotoMetadataChangedEvent, value);
			}
		}
		public PhotoListView()
		{
			this._paintBackground = true;
			this._backgroundDirty = false;
			this._topItemPos = new Point(0, 0);
			this._format = new StringFormat();
			this._dropData = new DropData(null);
			this.set_AllowDrop(true);
			this._imageList = new ImageList();
			ImageList arg_5C_0 = this._imageList;
			Size imageSize = new Size(120, 120);
			arg_5C_0.set_ImageSize(imageSize);
			this.set_LargeImageList(this._imageList);
			this.CreateGdiObjects();
		}
		public void ClearThumbnails()
		{
			this.get_Items().Clear();
			this._backgroundDirty = true;
		}
		public void SetThumbnails(string album, Photo[] list)
		{
			this.ClearThumbnails();
			checked
			{
				if (list != null)
				{
					if (list.get_Length() == 0)
					{
						return;
					}
					for (int i = 0; i < list.Length; i++)
					{
						Photo photo = list[i];
						ListViewItem listViewItem = this.get_Items().Add(photo.Title);
						listViewItem.set_Tag(photo);
					}
					this._backgroundDirty = true;
				}
			}
		}
		public int GetThumbnailIndex(Photo photo)
		{
			if (photo == null)
			{
				return -1;
			}
			try
			{
				IEnumerator enumerator = this.get_Items().GetEnumerator();
				while (enumerator.MoveNext())
				{
					ListViewItem listViewItem = (ListViewItem)enumerator.get_Current();
					Photo photo2 = (Photo)listViewItem.get_Tag();
					if (photo2.Equals(photo))
					{
						return listViewItem.get_Index();
					}
				}
			}
			finally
			{
				IEnumerator enumerator;
				if (enumerator is IDisposable)
				{
					((IDisposable)enumerator).Dispose();
				}
			}
			return -1;
		}
		private void CreateGdiObjects()
		{
			this._penFrame = new Pen(PhotoListView.Consts.FrameColor);
			this._penSelected = new Pen(PhotoListView.Consts.SelectedColor, 3f);
			this._penBack = new Pen(Color.get_DarkGray(), 3f);
			this._brushSelected = new SolidBrush(PhotoListView.Consts.SelectedColor);
			this._brushBack = new SolidBrush(PhotoListView.Consts.BackColor);
			this._format = new StringFormat();
			this._format.set_Alignment(1);
			this._format.set_Trimming(3);
		}
		protected override void OnAfterLabelEdit(LabelEditEventArgs e)
		{
			this._backgroundDirty = true;
			Photo photo = (Photo)this.get_Items().get_Item(e.get_Item()).get_Tag();
			if (e.get_Label() == null || e.get_Label().Trim().get_Length() == 0)
			{
				e.set_CancelEdit(true);
			}
			else
			{
				photo.Title = e.get_Label();
				photo.WriteXml();
			}
			if (this.PhotoMetadataChangedEvent != null)
			{
				this.PhotoMetadataChangedEvent(this, new PhotoMetadataChangedEventArgs(photo));
			}
			base.OnAfterLabelEdit(e);
		}
		private void DrawItem(Graphics g, int index)
		{
			ListViewItem listViewItem = this.get_Items().get_Item(index);
			int height = this.get_Font().get_Height();
			checked
			{
				Rectangle rectangle = new Rectangle(listViewItem.get_Bounds().get_Left() + (listViewItem.get_Bounds().get_Width() - 120) / 2, listViewItem.get_Bounds().get_Top() + 2, 120, 120);
				try
				{
					Photo photo = (Photo)listViewItem.get_Tag();
					Bitmap bitmap = new Bitmap(photo.ThumbnailPath);
					g.DrawImage(bitmap, rectangle.get_Left() + (120 - bitmap.get_Width()) / 2, rectangle.get_Top() + (120 - bitmap.get_Height()) / 2, bitmap.get_Width(), bitmap.get_Height());
				}
				catch (Exception expr_AF)
				{
					ProjectData.SetProjectError(expr_AF);
					ProjectData.ClearProjectError();
				}
				finally
				{
					Bitmap bitmap;
					if (bitmap != null)
					{
						bitmap.Dispose();
					}
				}
				if (!listViewItem.get_Selected())
				{
					g.DrawRectangle(this._penBack, rectangle);
				}
				g.DrawRectangle((Pen)Interaction.IIf(listViewItem.get_Selected(), this._penSelected, this._penFrame), rectangle);
				RectangleF rectangleF = new RectangleF((float)rectangle.get_Left(), (float)(rectangle.get_Bottom() + 4), (float)rectangle.get_Width(), (float)(height + 1));
				g.FillRectangle((SolidBrush)Interaction.IIf(listViewItem.get_Selected(), this._brushSelected, this._brushBack), rectangleF);
				g.DrawString(listViewItem.get_Text(), this.get_Font(), Brushes.get_Black(), rectangleF, this._format);
			}
		}
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\r\n               version=\"1\">\r\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\r\n                version=\"1\"\r\n                Flags=\"UnmanagedCode\"/>\r\n</PermissionSet>\r\n")]
		protected override void WndProc(ref Message m)
		{
			if (m.get_Msg() == 133)
			{
				this._backgroundDirty = true;
			}
			if (m.get_Msg() == 20 && !this.ProcessBackground())
			{
				return;
			}
			if (m.get_Msg() == 8270)
			{
				PhotoListView.Win32.NMHDR nMHDR = (PhotoListView.Win32.NMHDR)(m.GetLParam(typeof(PhotoListView.Win32.NMHDR)) ?? Activator.CreateInstance(typeof(PhotoListView.Win32.NMHDR)));
				if (nMHDR.code == -101)
				{
					this._paintBackground = false;
				}
				if (nMHDR.hwndFrom.Equals(this.get_Handle()) & nMHDR.code == -12)
				{
					this._paintBackground = true;
					if (this.ProcessListCustomDraw(ref m))
					{
						return;
					}
				}
			}
			base.WndProc(ref m);
		}
		private bool ProcessListCustomDraw(ref Message m)
		{
			bool result = false;
			PhotoListView.Win32.NMCUSTOMDRAW nMCUSTOMDRAW = (PhotoListView.Win32.NMCUSTOMDRAW)(m.GetLParam(typeof(PhotoListView.Win32.NMCUSTOMDRAW)) ?? Activator.CreateInstance(typeof(PhotoListView.Win32.NMCUSTOMDRAW)));
			int dwDrawStage = nMCUSTOMDRAW.dwDrawStage;
			if (dwDrawStage == 1)
			{
				IntPtr result2 = new IntPtr(32);
				m.set_Result(result2);
			}
			else
			{
				if (dwDrawStage == 65537)
				{
					IntPtr result2 = new IntPtr(4);
					m.set_Result(result2);
					if (this.IsItemVisible(nMCUSTOMDRAW.dwItemSpec))
					{
						Graphics graphics = Graphics.FromHdc(nMCUSTOMDRAW.hdc);
						try
						{
							this.DrawItem(graphics, nMCUSTOMDRAW.dwItemSpec);
							result = true;
							return result;
						}
						finally
						{
							graphics.Dispose();
						}
					}
					result = true;
				}
				else
				{
					IntPtr result2 = new IntPtr(0);
					m.set_Result(result2);
				}
			}
			return result;
		}
		private bool ProcessBackground()
		{
			bool result = true;
			if (!this._backgroundDirty && !this._paintBackground && this.get_Items().get_Count() > 0 && !this._topItemPos.Equals(this.GetItemRect(0).get_Location()))
			{
				this._topItemPos = this.GetItemRect(0).get_Location();
				this._paintBackground = true;
			}
			if (!this._paintBackground && !this._backgroundDirty)
			{
				result = false;
			}
			this._backgroundDirty = false;
			return result;
		}
		private bool IsItemVisible(int index)
		{
			if (this.get_Items().get_Count() <= index)
			{
				return false;
			}
			Rectangle itemRect = this.GetItemRect(index);
			return this.get_DisplayRectangle().Contains(itemRect.get_Left(), itemRect.get_Top()) | this.get_DisplayRectangle().Contains(itemRect.get_Right(), itemRect.get_Bottom());
		}
		protected override void OnDragEnter(DragEventArgs drgevent)
		{
			this._dropData.Enter(drgevent);
			base.OnDragEnter(drgevent);
		}
		protected override void OnDragOver(DragEventArgs drgevent)
		{
			this._dropData.Over(drgevent);
			if (Global.PerformingDrag)
			{
				drgevent.set_Effect(0);
			}
			base.OnDragOver(drgevent);
		}
		protected override void OnDragLeave(EventArgs e)
		{
			this._dropData.Leave();
			base.OnDragLeave(e);
		}
		protected override void OnDragDrop(DragEventArgs drgevent)
		{
			if (!Global.PerformingDrag)
			{
				DragDropEffects dragDropEffects = 1;
				if (this._dropData.MouseButtons == 2097152)
				{
					dragDropEffects = new DropContextMenu
					{
						EnableMove = false
					}.Display(this);
				}
				if (dragDropEffects == 1)
				{
					string[] array = this._dropData.Drop(drgevent);
					if (array != null && array.get_Length() > 0 && this.FilesDroppedEvent != null)
					{
						this.FilesDroppedEvent(this, new FilesDroppedEventArgs(array));
					}
				}
			}
			base.OnDragDrop(drgevent);
		}
		protected override void OnItemDrag(ItemDragEventArgs e)
		{
			Global.PerformingDrag = true;
			checked
			{
				if (this.get_SelectedItems().get_Count() > 0)
				{
					string[] array = new string[this.get_SelectedItems().get_Count() - 1 + 1];
					int arg_37_0 = 0;
					int num = array.get_Length() - 1;
					for (int i = arg_37_0; i <= num; i++)
					{
						Photo photo = (Photo)this.get_SelectedItems().get_Item(i).get_Tag();
						array[i] = photo.PhotoPath;
					}
					DataObject dataObject = new DataObject();
					dataObject.SetData(DataFormats.FileDrop, array);
					DragDropEffects dragDropEffects = this.DoDragDrop(dataObject, 3);
					if (dragDropEffects != 1 && this.FilesDraggedEvent != null)
					{
						this.FilesDraggedEvent(this, EventArgs.Empty);
					}
				}
				Global.PerformingDrag = false;
				base.OnItemDrag(e);
			}
		}
	}
}
