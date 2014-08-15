using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace FotoVision
{
	public class PhotosPane : BasePane
	{
		private class Consts
		{
			public const string CaptionManage = "Manage Photos";
			public const string CaptionShow = "Photo Show";
			public const string CaptionActions = "Photo Actions";
		}
		private class Win32
		{
			public const long LVM_SCROLL = 4116L;
			[DllImport("user32")]
			public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
		}
		public delegate void FilesDroppedEventHandler(object sender, FilesDroppedEventArgs e);
		public delegate void SelectionChangedEventHandler(object sender, EventArgs e);
		public delegate void PhotoMetadataChangedEventHandler(object sender, PhotoMetadataChangedEventArgs e);
		public delegate void CropDataChangedEventHandler(object sender, CropDataChangedEventArgs e);
		public delegate void OpenPhotoEventHandler(object sender, EventArgs e);
		public delegate void PhotosDeletedEventHandler(object sender, EventArgs e);
		public delegate void PhotosMenuClickedEventHandler(object sender, PhotosMenuClickedEventArgs e);
		[AccessedThroughProperty("menuPhotoShowDetails")]
		private MenuItem _menuPhotoShowDetails;
		private PhotosPane.OpenPhotoEventHandler OpenPhotoEvent;
		private PhotosPane.PhotosDeletedEventHandler PhotosDeletedEvent;
		[AccessedThroughProperty("photoViewer")]
		private PhotoViewer _photoViewer;
		[AccessedThroughProperty("menuOpen")]
		private MenuItem _menuOpen;
		private PhotosPane.PhotosMenuClickedEventHandler PhotosMenuClickedEvent;
		private PhotosPane.SelectionChangedEventHandler SelectionChangedEvent;
		[AccessedThroughProperty("menuSep2")]
		private MenuItem _menuSep2;
		[AccessedThroughProperty("menuSep1")]
		private MenuItem _menuSep1;
		private PhotosPane.CropDataChangedEventHandler CropDataChangedEvent;
		[AccessedThroughProperty("menuRotateLeft")]
		private MenuItem _menuRotateLeft;
		[AccessedThroughProperty("menuDelete")]
		private MenuItem _menuDelete;
		[AccessedThroughProperty("menuSep3")]
		private MenuItem _menuSep3;
		[AccessedThroughProperty("menuPhotoActions")]
		private MenuItem _menuPhotoActions;
		[AccessedThroughProperty("menuRename")]
		private MenuItem _menuRename;
		[AccessedThroughProperty("menuPhotoProperties")]
		private MenuItem _menuPhotoProperties;
		[AccessedThroughProperty("menuPhoto")]
		private ContextMenu _menuPhoto;
		[AccessedThroughProperty("menuSelectAll")]
		private MenuItem _menuSelectAll;
		[AccessedThroughProperty("menuThumbnails")]
		private ContextMenu _menuThumbnails;
		private PhotosPane.PhotoMetadataChangedEventHandler PhotoMetadataChangedEvent;
		[AccessedThroughProperty("menuPhotoShow")]
		private MenuItem _menuPhotoShow;
		[AccessedThroughProperty("menuProperties")]
		private MenuItem _menuProperties;
		private PhotosPane.FilesDroppedEventHandler FilesDroppedEvent;
		[AccessedThroughProperty("labelPos")]
		private Label _labelPos;
		[AccessedThroughProperty("listView")]
		private PhotoListView _listView;
		[AccessedThroughProperty("menuRotateRight")]
		private MenuItem _menuRotateRight;
		private PhotosMode _mode;
		private string _curAlbum;
		private bool _inLabelEdit;
		private int[] _selectedList;
		public event PhotosPane.FilesDroppedEventHandler FilesDropped
		{
			[MethodImpl(32)]
			add
			{
				this.FilesDroppedEvent = (PhotosPane.FilesDroppedEventHandler)Delegate.Combine(this.FilesDroppedEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.FilesDroppedEvent = (PhotosPane.FilesDroppedEventHandler)Delegate.Remove(this.FilesDroppedEvent, value);
			}
		}
		public event PhotosPane.SelectionChangedEventHandler SelectionChanged
		{
			[MethodImpl(32)]
			add
			{
				this.SelectionChangedEvent = (PhotosPane.SelectionChangedEventHandler)Delegate.Combine(this.SelectionChangedEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.SelectionChangedEvent = (PhotosPane.SelectionChangedEventHandler)Delegate.Remove(this.SelectionChangedEvent, value);
			}
		}
		public event PhotosPane.PhotoMetadataChangedEventHandler PhotoMetadataChanged
		{
			[MethodImpl(32)]
			add
			{
				this.PhotoMetadataChangedEvent = (PhotosPane.PhotoMetadataChangedEventHandler)Delegate.Combine(this.PhotoMetadataChangedEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.PhotoMetadataChangedEvent = (PhotosPane.PhotoMetadataChangedEventHandler)Delegate.Remove(this.PhotoMetadataChangedEvent, value);
			}
		}
		public event PhotosPane.CropDataChangedEventHandler CropDataChanged
		{
			[MethodImpl(32)]
			add
			{
				this.CropDataChangedEvent = (PhotosPane.CropDataChangedEventHandler)Delegate.Combine(this.CropDataChangedEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.CropDataChangedEvent = (PhotosPane.CropDataChangedEventHandler)Delegate.Remove(this.CropDataChangedEvent, value);
			}
		}
		public event PhotosPane.OpenPhotoEventHandler OpenPhoto
		{
			[MethodImpl(32)]
			add
			{
				this.OpenPhotoEvent = (PhotosPane.OpenPhotoEventHandler)Delegate.Combine(this.OpenPhotoEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.OpenPhotoEvent = (PhotosPane.OpenPhotoEventHandler)Delegate.Remove(this.OpenPhotoEvent, value);
			}
		}
		public event PhotosPane.PhotosDeletedEventHandler PhotosDeleted
		{
			[MethodImpl(32)]
			add
			{
				this.PhotosDeletedEvent = (PhotosPane.PhotosDeletedEventHandler)Delegate.Combine(this.PhotosDeletedEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.PhotosDeletedEvent = (PhotosPane.PhotosDeletedEventHandler)Delegate.Remove(this.PhotosDeletedEvent, value);
			}
		}
		public event PhotosPane.PhotosMenuClickedEventHandler PhotosMenuClicked
		{
			[MethodImpl(32)]
			add
			{
				this.PhotosMenuClickedEvent = (PhotosPane.PhotosMenuClickedEventHandler)Delegate.Combine(this.PhotosMenuClickedEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.PhotosMenuClickedEvent = (PhotosPane.PhotosMenuClickedEventHandler)Delegate.Remove(this.PhotosMenuClickedEvent, value);
			}
		}
		private PhotoListView listView
		{
			get
			{
				return this._listView;
			}
			[MethodImpl(32)]
			set
			{
				if (this._listView != null)
				{
					this._listView.MouseUp -= new MouseEventHandler(this.listView_MouseUp);
					this._listView.KeyUp -= new KeyEventHandler(this.listView_KeyUp);
					this._listView.DoubleClick -= new EventHandler(this.listView_DoubleClick);
					this._listView.PhotoMetadataChanged -= new PhotoListView.PhotoMetadataChangedEventHandler(this.listView_PhotoMetadataChanged);
					this._listView.FilesDragged -= new PhotoListView.FilesDraggedEventHandler(this.listView_FilesDragged);
					this._listView.FilesDropped -= new PhotoListView.FilesDroppedEventHandler(this.listView_FilesDropped);
					this._listView.AfterLabelEdit -= new LabelEditEventHandler(this.listView_AfterLabelEdit);
					this._listView.BeforeLabelEdit -= new LabelEditEventHandler(this.listView_BeforeLabelEdit);
				}
				this._listView = value;
				if (this._listView != null)
				{
					this._listView.MouseUp += new MouseEventHandler(this.listView_MouseUp);
					this._listView.KeyUp += new KeyEventHandler(this.listView_KeyUp);
					this._listView.DoubleClick += new EventHandler(this.listView_DoubleClick);
					this._listView.PhotoMetadataChanged += new PhotoListView.PhotoMetadataChangedEventHandler(this.listView_PhotoMetadataChanged);
					this._listView.FilesDragged += new PhotoListView.FilesDraggedEventHandler(this.listView_FilesDragged);
					this._listView.FilesDropped += new PhotoListView.FilesDroppedEventHandler(this.listView_FilesDropped);
					this._listView.AfterLabelEdit += new LabelEditEventHandler(this.listView_AfterLabelEdit);
					this._listView.BeforeLabelEdit += new LabelEditEventHandler(this.listView_BeforeLabelEdit);
				}
			}
		}
		private PhotoViewer photoViewer
		{
			get
			{
				return this._photoViewer;
			}
			[MethodImpl(32)]
			set
			{
				if (this._photoViewer != null)
				{
					this._photoViewer.CropDataChanged -= new PhotoViewer.CropDataChangedEventHandler(this.photoViewer_CropDataChanged);
					this._photoViewer.MouseDown -= new MouseEventHandler(this.photoViewer_MouseDown);
				}
				this._photoViewer = value;
				if (this._photoViewer != null)
				{
					this._photoViewer.CropDataChanged += new PhotoViewer.CropDataChangedEventHandler(this.photoViewer_CropDataChanged);
					this._photoViewer.MouseDown += new MouseEventHandler(this.photoViewer_MouseDown);
				}
			}
		}
		[Browsable(false)]
		public bool FullScreen
		{
			get
			{
				return !this.CaptionControl.Visible;
			}
			set
			{
				this.CaptionControl.Visible = !value;
				this.labelPos.Visible = value;
				if (value)
				{
					this.labelPos.Text = string.Format("Photo {0} of {1}", checked(this.CurrentPhotoIndex + 1), this.Count);
					this.photoViewer.ContextMenu = null;
				}
				else
				{
					this.photoViewer.ContextMenu = this.menuPhoto;
				}
			}
		}
		[Browsable(false)]
		public PhotoInfo PhotoInfo
		{
			get
			{
				return this.photoViewer.PhotoInfo;
			}
		}
		[Browsable(false)]
		public int Count
		{
			get
			{
				return this.listView.Items.Count;
			}
		}
		[Browsable(false)]
		public int CurrentPhotoIndex
		{
			get
			{
				return this.listView.GetThumbnailIndex(this.photoViewer.Photo);
			}
		}
		[Browsable(false)]
		public bool PhotoDirty
		{
			get
			{
				return this.photoViewer.PhotoDirty;
			}
		}
		[Browsable(false)]
		public int SelectedCount
		{
			get
			{
				return this.listView.SelectedItems.Count;
			}
		}
		[Browsable(false)]
		public Photo SelectedPhoto
		{
			get
			{
				if (this.listView.SelectedItems.Count != 1)
				{
					return null;
				}
				return (Photo)this.listView.SelectedItems.Item(0).get_Tag;
			}
		}
		[Browsable(false)]
		public bool CropMode
		{
			get
			{
				return this.photoViewer.CropMode;
			}
			set
			{
				this.photoViewer.CropMode = value;
			}
		}
		[Browsable(false)]
		public PhotosMode Mode
		{
			get
			{
				return this._mode;
			}
			set
			{
				this._mode = value;
				if (value == PhotosMode.Thumbnails)
				{
					this.ShowThumbnails();
				}
				else
				{
					this.photoViewer.EditMode = BooleanType.FromObject(Interaction.IIf(value == PhotosMode.PhotoAction, true, false));
				}
				this.UpdateCaption();
			}
		}
		[Browsable(false)]
		public Bitmap ImageWithActions
		{
			get
			{
				if (this.Mode == PhotosMode.Thumbnails)
				{
					return null;
				}
				return this.photoViewer.ImageWithActions;
			}
		}
		[Browsable(false)]
		public bool InLabelEdit
		{
			get
			{
				return this._inLabelEdit;
			}
		}
		private MenuItem menuOpen
		{
			get
			{
				return this._menuOpen;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuOpen != null)
				{
				}
				this._menuOpen = value;
				if (this._menuOpen != null)
				{
				}
			}
		}
		private MenuItem menuRotateLeft
		{
			get
			{
				return this._menuRotateLeft;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuRotateLeft != null)
				{
					this._menuRotateLeft.Click -= new EventHandler(this.menu_Click);
				}
				this._menuRotateLeft = value;
				if (this._menuRotateLeft != null)
				{
					this._menuRotateLeft.Click += new EventHandler(this.menu_Click);
				}
			}
		}
		private MenuItem menuRotateRight
		{
			get
			{
				return this._menuRotateRight;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuRotateRight != null)
				{
					this._menuRotateRight.Click -= new EventHandler(this.menu_Click);
				}
				this._menuRotateRight = value;
				if (this._menuRotateRight != null)
				{
					this._menuRotateRight.Click += new EventHandler(this.menu_Click);
				}
			}
		}
		private MenuItem menuRename
		{
			get
			{
				return this._menuRename;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuRename != null)
				{
					this._menuRename.Click -= new EventHandler(this.menu_Click);
				}
				this._menuRename = value;
				if (this._menuRename != null)
				{
					this._menuRename.Click += new EventHandler(this.menu_Click);
				}
			}
		}
		private MenuItem menuDelete
		{
			get
			{
				return this._menuDelete;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuDelete != null)
				{
					this._menuDelete.Click -= new EventHandler(this.menu_Click);
				}
				this._menuDelete = value;
				if (this._menuDelete != null)
				{
					this._menuDelete.Click += new EventHandler(this.menu_Click);
				}
			}
		}
		private MenuItem menuPhotoShow
		{
			get
			{
				return this._menuPhotoShow;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuPhotoShow != null)
				{
					this._menuPhotoShow.Click -= new EventHandler(this.menu_Click);
				}
				this._menuPhotoShow = value;
				if (this._menuPhotoShow != null)
				{
					this._menuPhotoShow.Click += new EventHandler(this.menu_Click);
				}
			}
		}
		private MenuItem menuPhotoShowDetails
		{
			get
			{
				return this._menuPhotoShowDetails;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuPhotoShowDetails != null)
				{
					this._menuPhotoShowDetails.Click -= new EventHandler(this.menu_Click);
				}
				this._menuPhotoShowDetails = value;
				if (this._menuPhotoShowDetails != null)
				{
					this._menuPhotoShowDetails.Click += new EventHandler(this.menu_Click);
				}
			}
		}
		private MenuItem menuPhotoActions
		{
			get
			{
				return this._menuPhotoActions;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuPhotoActions != null)
				{
					this._menuPhotoActions.Click -= new EventHandler(this.menu_Click);
				}
				this._menuPhotoActions = value;
				if (this._menuPhotoActions != null)
				{
					this._menuPhotoActions.Click += new EventHandler(this.menu_Click);
				}
			}
		}
		private MenuItem menuSelectAll
		{
			get
			{
				return this._menuSelectAll;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuSelectAll != null)
				{
					this._menuSelectAll.Click -= new EventHandler(this.menu_Click);
				}
				this._menuSelectAll = value;
				if (this._menuSelectAll != null)
				{
					this._menuSelectAll.Click += new EventHandler(this.menu_Click);
				}
			}
		}
		private Label labelPos
		{
			get
			{
				return this._labelPos;
			}
			[MethodImpl(32)]
			set
			{
				if (this._labelPos != null)
				{
				}
				this._labelPos = value;
				if (this._labelPos != null)
				{
				}
			}
		}
		private MenuItem menuProperties
		{
			get
			{
				return this._menuProperties;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuProperties != null)
				{
					this._menuProperties.Click -= new EventHandler(this.menuProperties_Click);
				}
				this._menuProperties = value;
				if (this._menuProperties != null)
				{
					this._menuProperties.Click += new EventHandler(this.menuProperties_Click);
				}
			}
		}
		private ContextMenu menuThumbnails
		{
			get
			{
				return this._menuThumbnails;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuThumbnails != null)
				{
					this._menuThumbnails.Popup -= new EventHandler(this.menuThumbnails_Popup);
				}
				this._menuThumbnails = value;
				if (this._menuThumbnails != null)
				{
					this._menuThumbnails.Popup += new EventHandler(this.menuThumbnails_Popup);
				}
			}
		}
		private ContextMenu menuPhoto
		{
			get
			{
				return this._menuPhoto;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuPhoto != null)
				{
				}
				this._menuPhoto = value;
				if (this._menuPhoto != null)
				{
				}
			}
		}
		private MenuItem menuPhotoProperties
		{
			get
			{
				return this._menuPhotoProperties;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuPhotoProperties != null)
				{
					this._menuPhotoProperties.Click -= new EventHandler(this.menuProperties_Click);
				}
				this._menuPhotoProperties = value;
				if (this._menuPhotoProperties != null)
				{
					this._menuPhotoProperties.Click += new EventHandler(this.menuProperties_Click);
				}
			}
		}
		private MenuItem menuSep1
		{
			get
			{
				return this._menuSep1;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuSep1 != null)
				{
				}
				this._menuSep1 = value;
				if (this._menuSep1 != null)
				{
				}
			}
		}
		private MenuItem menuSep2
		{
			get
			{
				return this._menuSep2;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuSep2 != null)
				{
				}
				this._menuSep2 = value;
				if (this._menuSep2 != null)
				{
				}
			}
		}
		private MenuItem menuSep3
		{
			get
			{
				return this._menuSep3;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuSep3 != null)
				{
				}
				this._menuSep3 = value;
				if (this._menuSep3 != null)
				{
				}
			}
		}
		public PhotosPane()
		{
			this._curAlbum = "";
			this._inLabelEdit = false;
			this.InitializeComponent();
			this.CaptionText = "Manage Photos";
		}
		private void InitializeComponent()
		{
			this.listView = new PhotoListView();
			this.menuThumbnails = new ContextMenu();
			this.menuOpen = new MenuItem();
			this.menuPhotoShow = new MenuItem();
			this.menuPhotoShowDetails = new MenuItem();
			this.menuPhotoActions = new MenuItem();
			this.menuSelectAll = new MenuItem();
			this.menuSep1 = new MenuItem();
			this.menuRotateLeft = new MenuItem();
			this.menuRotateRight = new MenuItem();
			this.menuSep2 = new MenuItem();
			this.menuDelete = new MenuItem();
			this.menuRename = new MenuItem();
			this.menuSep3 = new MenuItem();
			this.menuProperties = new MenuItem();
			this.photoViewer = new PhotoViewer();
			this.menuPhoto = new ContextMenu();
			this.menuPhotoProperties = new MenuItem();
			this.labelPos = new Label();
			this.photoViewer.SuspendLayout();
			this.SuspendLayout();
			this.listView.AllowDrop = true;
			this.listView.BackColor = Color.DarkGray;
			this.listView.BorderStyle = 0;
			this.listView.ContextMenu = this.menuThumbnails;
			this.listView.Dock = 5;
			this.listView.LabelEdit = true;
			Control arg_144_0 = this.listView;
			Point location = new Point(2, 22);
			arg_144_0.Location = location;
			this.listView.Name = "listView";
			Control arg_171_0 = this.listView;
			Size size = new Size(252, 256);
			arg_171_0.Size = size;
			this.listView.TabIndex = 1;
			this.menuThumbnails.MenuItems.AddRange(new MenuItem[]
			{
				this.menuOpen,
				this.menuSelectAll,
				this.menuSep1,
				this.menuRotateLeft,
				this.menuRotateRight,
				this.menuSep2,
				this.menuDelete,
				this.menuRename,
				this.menuSep3,
				this.menuProperties
			});
			this.menuOpen.Index = 0;
			this.menuOpen.MenuItems.AddRange(new MenuItem[]
			{
				this.menuPhotoShow,
				this.menuPhotoShowDetails,
				this.menuPhotoActions
			});
			this.menuOpen.Text = "Open";
			this.menuPhotoShow.Index = 0;
			this.menuPhotoShow.Text = "Photo Show";
			this.menuPhotoShowDetails.Index = 1;
			this.menuPhotoShowDetails.Text = "Photo Show with Descriptions";
			this.menuPhotoActions.Index = 2;
			this.menuPhotoActions.Text = "Photo Actions";
			this.menuSelectAll.Index = 1;
			this.menuSelectAll.Text = "Select All";
			this.menuSep1.Index = 2;
			this.menuSep1.Text = "-";
			this.menuRotateLeft.Index = 3;
			this.menuRotateLeft.Text = "Rotate Left";
			this.menuRotateRight.Index = 4;
			this.menuRotateRight.Text = "Rotate Right";
			this.menuSep2.Index = 5;
			this.menuSep2.Text = "-";
			this.menuDelete.Index = 6;
			this.menuDelete.Text = "Delete";
			this.menuRename.Index = 7;
			this.menuRename.Text = "Rename";
			this.menuSep3.Index = 8;
			this.menuSep3.Text = "-";
			this.menuProperties.Index = 9;
			this.menuProperties.Text = "Properties";
			this.photoViewer.BackColor = Color.DarkGray;
			this.photoViewer.ContextMenu = this.menuPhoto;
			this.photoViewer.Controls.Add(this.labelPos);
			this.photoViewer.CropMode = false;
			this.photoViewer.Dock = 5;
			this.photoViewer.EditMode = false;
			Control arg_402_0 = this.photoViewer;
			location = new Point(2, 22);
			arg_402_0.Location = location;
			this.photoViewer.Name = "photoViewer";
			this.photoViewer.Photo = null;
			Control arg_43B_0 = this.photoViewer;
			size = new Size(252, 256);
			arg_43B_0.Size = size;
			this.photoViewer.TabIndex = 2;
			this.photoViewer.Visible = false;
			this.menuPhoto.MenuItems.AddRange(new MenuItem[]
			{
				this.menuPhotoProperties
			});
			this.menuPhotoProperties.Index = 0;
			this.menuPhotoProperties.Text = "Properties";
			this.labelPos.Anchor = 10;
			this.labelPos.BackColor = Color.Transparent;
			Control arg_4CA_0 = this.labelPos;
			location = new Point(152, 240);
			arg_4CA_0.Location = location;
			this.labelPos.Name = "labelPos";
			Control arg_4F1_0 = this.labelPos;
			size = new Size(96, 16);
			arg_4F1_0.Size = size;
			this.labelPos.TabIndex = 3;
			this.labelPos.Text = "Photo xx of xx";
			this.labelPos.TextAlign = 1024;
			this.BackColor = SystemColors.Control;
			this.Controls.Add(this.photoViewer);
			this.Controls.Add(this.listView);
			this.DockPadding.All = 2;
			this.Name = "PhotosPane";
			size = new Size(256, 280);
			this.Size = size;
			this.Controls.SetChildIndex(this.listView, 0);
			this.Controls.SetChildIndex(this.photoViewer, 0);
			this.photoViewer.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		public void SelectAll()
		{
			try
			{
				IEnumerator enumerator = this.listView.Items.GetEnumerator();
				while (enumerator.MoveNext())
				{
					ListViewItem listViewItem = (ListViewItem)enumerator.Current;
					listViewItem.Selected = true;
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
			this.CheckSelectionChange();
		}
		public void UnselectAll()
		{
			try
			{
				IEnumerator enumerator = this.listView.Items.GetEnumerator();
				while (enumerator.MoveNext())
				{
					ListViewItem listViewItem = (ListViewItem)enumerator.Current;
					listViewItem.Selected = false;
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
			this.CheckSelectionChange();
		}
		public Photo GetPhoto(int index)
		{
			return (Photo)this.listView.Items.Item(index).get_Tag;
		}
		public Photo[] GetSelectedPhotos()
		{
			if (this.listView.SelectedItems == null || this.listView.SelectedItems.Count == 0)
			{
				return null;
			}
			checked
			{
				Photo[] array = new Photo[this.listView.SelectedItems.Count - 1 + 1];
				int arg_47_0 = 0;
				int num = array.Length - 1;
				for (int i = arg_47_0; i <= num; i++)
				{
					Photo photo = (Photo)this.listView.SelectedItems.Item(i).get_Tag;
					array[i] = photo;
				}
				return array;
			}
		}
		public int[] GetSelectedIndices()
		{
			if (this.SelectedCount == 0)
			{
				return null;
			}
			int[] array = new int[checked(this.listView.SelectedIndices.Count - 1 + 1)];
			this.listView.SelectedIndices.CopyTo(array, 0);
			return array;
		}
		public void SelectPhoto(int index)
		{
			this.listView.Items.get_Item(index).Selected = true;
		}
		public void ClearThumbnails()
		{
			this._curAlbum = "";
			this.listView.ClearThumbnails();
			this._selectedList = null;
			if (this.SelectionChangedEvent != null)
			{
				this.SelectionChangedEvent(this, EventArgs.Empty);
			}
		}
		public void UpdateThumbnails(string albumName)
		{
			int count = this.listView.Items.Count;
			bool flag = BooleanType.FromObject(Interaction.IIf(StringType.StrCmp(this._curAlbum, albumName, false) == 0, true, false));
			int lParam = 0;
			checked
			{
				if (flag && this.listView.Items.Count > 0)
				{
					lParam = 0 - this.listView.Items.Item(0).get_Bounds.Top;
				}
				this._curAlbum = albumName;
				this.listView.SetThumbnails(albumName, FileManager.GetPhotos(albumName, true));
				if (this.Mode == PhotosMode.PhotoAction || this.Mode == PhotosMode.PhotoShow)
				{
					this.photoViewer.UpdateLocation(albumName);
				}
				if (this._selectedList != null && flag && this.listView.Items.Count == count)
				{
					int[] selectedList = this._selectedList;
					for (int i = 0; i < selectedList.Length; i++)
					{
						int num = selectedList[i];
						this.listView.Items.get_Item(num).Selected = true;
					}
				}
				else
				{
					this._selectedList = null;
				}
				if (flag)
				{
					PhotosPane.Win32.SendMessage(this.listView.Handle, 4116, 0, lParam);
				}
				this.listView.Invalidate();
				if (this.SelectionChangedEvent != null)
				{
					this.SelectionChangedEvent(this, EventArgs.Empty);
				}
			}
		}
		public void UpdateMetadata(Photo photo)
		{
			try
			{
				IEnumerator enumerator = this.listView.Items.GetEnumerator();
				while (enumerator.MoveNext())
				{
					ListViewItem listViewItem = (ListViewItem)enumerator.Current;
					Photo photo2 = (Photo)listViewItem.Tag;
					if (StringType.StrCmp(photo2.PhotoName, photo.PhotoName, false) == 0)
					{
						listViewItem.Tag = photo;
						if (StringType.StrCmp(listViewItem.Text, photo.Title, false) != 0)
						{
							listViewItem.Text = photo.Title;
							this.listView.Invalidate();
						}
						break;
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
		}
		public void OpenPhotoAtIndex(int index)
		{
			this.ShowPhoto((Photo)this.listView.Items.Item(index).get_Tag);
			this.photoViewer.Invalidate();
			if (this.FullScreen)
			{
				this.labelPos.Text = string.Format("Photo {0} of {1}", checked(index + 1), this.Count);
			}
		}
		public void OpenSelectedPhoto()
		{
			if (this.listView.SelectedItems.Count == 1)
			{
				this.ShowPhoto((Photo)this.listView.SelectedItems.Item(0).get_Tag);
			}
		}
		public bool ApplyPhotoAction(ActionItem actionItem)
		{
			bool result = false;
			if (this.Mode == PhotosMode.Thumbnails)
			{
				if (this.listView.SelectedItems.Count > 0)
				{
					Cursor.Current = Cursors.WaitCursor;
					this.RotateThumbnails(actionItem);
					Global.Progress.Complete(this);
					result = true;
					Cursor.Current = Cursors.Default;
				}
			}
			else
			{
				this.photoViewer.ApplyPhotoAction(actionItem);
			}
			return result;
		}
		public void Undo()
		{
			this.photoViewer.Undo();
		}
		public void SavePhoto()
		{
			this.photoViewer.SavePhoto();
		}
		public void DiscardChanges()
		{
			this.photoViewer.DiscardChanges();
		}
		public void ClearCrop()
		{
			this.photoViewer.ClearCrop();
		}
		public void Rename()
		{
			if (this._inLabelEdit)
			{
				return;
			}
			if (this.listView.SelectedItems.Count == 1)
			{
				this.listView.SelectedItems.Item(0).BeginEdit;
			}
		}
		public void Delete()
		{
			if (this._inLabelEdit)
			{
				return;
			}
			if (Global.Settings.GetBool(SettingKey.PromptFileDelete) && new DeletePhotoForm
			{
				Count = this.SelectedCount
			}.ShowDialog() == 2)
			{
				return;
			}
			checked
			{
				if (this.Mode == PhotosMode.Thumbnails)
				{
					Photo[] selectedPhotos = this.GetSelectedPhotos();
					Photo[] array = selectedPhotos;
					for (int i = 0; i < array.Length; i++)
					{
						Photo photo = array[i];
						FileManager.DeletePhoto(photo.PhotoPath);
					}
					if (selectedPhotos.Length > 0)
					{
						if (this.PhotosDeletedEvent != null)
						{
							this.PhotosDeletedEvent(this, EventArgs.Empty);
						}
					}
				}
				else
				{
					FileManager.DeletePhoto(this.photoViewer.Photo.PhotoPath);
					this.photoViewer.ClearActions();
					if (this.PhotosDeletedEvent != null)
					{
						this.PhotosDeletedEvent(this, EventArgs.Empty);
					}
				}
			}
		}
		private void UpdateCaption()
		{
			switch (this.Mode)
			{
			case PhotosMode.Thumbnails:
				this.CaptionText = "Manage Photos";
				break;
			case PhotosMode.PhotoAction:
				this.CaptionText = "Photo Actions";
				break;
			case PhotosMode.PhotoShow:
				this.CaptionText = "Photo Show";
				break;
			}
		}
		private void CheckSelectionChange()
		{
			int[] selectedIndices = this.GetSelectedIndices();
			if (!this.CompareSelections(selectedIndices, this._selectedList))
			{
				if (selectedIndices == null)
				{
					this._selectedList = null;
				}
				else
				{
					this._selectedList = new int[checked(selectedIndices.Length - 1 + 1)];
					Array.Copy(selectedIndices, this._selectedList, selectedIndices.Length);
				}
				if (this.SelectionChangedEvent != null)
				{
					this.SelectionChangedEvent(this, EventArgs.Empty);
				}
			}
		}
		private bool CompareSelections(int[] oldList, int[] newList)
		{
			if (oldList == null & newList == null)
			{
				return true;
			}
			if (oldList == null & newList != null)
			{
				return false;
			}
			if (oldList != null & newList == null)
			{
				return false;
			}
			if (oldList.Length != newList.Length)
			{
				return false;
			}
			int arg_47_0 = 0;
			checked
			{
				int num = oldList.Length - 1;
				for (int i = arg_47_0; i <= num; i++)
				{
					if (oldList[i] != newList[i])
					{
						return false;
					}
				}
				return true;
			}
		}
		private void ShowThumbnails()
		{
			if (!this.listView.Visible)
			{
				this.listView.Visible = true;
				this.listView.Invalidate();
				this.listView.Update();
				this.photoViewer.Visible = false;
			}
		}
		private void ShowPhoto(Photo photo)
		{
			this.photoViewer.Photo = photo;
			this.photoViewer.Visible = true;
			this.listView.Visible = false;
		}
		private void RotateThumbnails(ActionItem actionItem)
		{
			checked
			{
				try
				{
					int num = 1;
					string message = string.Format("Processing photo{0}", RuntimeHelpers.GetObjectValue(Interaction.IIf(this.listView.SelectedItems.Count == 1, "", "s")));
					try
					{
						IEnumerator enumerator = this.listView.SelectedItems.GetEnumerator();
						while (enumerator.MoveNext())
						{
							ListViewItem listViewItem = (ListViewItem)enumerator.Current;
							Global.Progress.Update(this, message, num, this.listView.SelectedItems.Count);
							num++;
							RotateFlipType rotateFlipType = 0;
							switch (actionItem.Action)
							{
							case PhotoAction.RotateLeft:
								rotateFlipType = 3;
								break;
							case PhotoAction.RotateRight:
								rotateFlipType = 1;
								break;
							case PhotoAction.FlipHorizontal:
								rotateFlipType = 4;
								break;
							case PhotoAction.FlipVertical:
								rotateFlipType = 6;
								break;
							}
							Photo photo = (Photo)listViewItem.Tag;
							if (FileManager.IsFileReadOnly(photo.PhotoPath))
							{
								throw new ApplicationException(string.Format("The photo '{0}' is read-only.", photo.PhotoPath));
							}
							Bitmap bitmap = new Bitmap(photo.PhotoPath);
							Bitmap bitmap2 = new Bitmap(bitmap);
							ImageFormat rawFormat = bitmap.RawFormat;
							bitmap2.RotateFlip(rotateFlipType);
							if (Global.Settings.GetBool(SettingKey.MaintainExifInfo))
							{
								Exif.Copy(bitmap, bitmap2);
							}
							bitmap.Dispose();
							bitmap = null;
							bitmap2.Save(photo.PhotoPath, rawFormat);
							bitmap2.Dispose();
							bitmap2 = null;
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
				}
				catch (Exception expr_15D)
				{
					ProjectData.SetProjectError(expr_15D);
					Exception ex = expr_15D;
					Bitmap bitmap;
					if (bitmap != null)
					{
						bitmap.Dispose();
					}
					Bitmap bitmap2;
					if (bitmap2 != null)
					{
						bitmap2.Dispose();
					}
					Global.DisplayError("The photo could not be rotated.", ex);
					ProjectData.ClearProjectError();
				}
			}
		}
		private void listView_BeforeLabelEdit(object sender, LabelEditEventArgs e)
		{
			this._inLabelEdit = true;
		}
		private void listView_AfterLabelEdit(object sender, LabelEditEventArgs e)
		{
			this._inLabelEdit = false;
		}
		private void listView_FilesDropped(object sender, FilesDroppedEventArgs e)
		{
			if (this.FilesDroppedEvent != null)
			{
				this.FilesDroppedEvent(this, e);
			}
		}
		private void listView_FilesDragged(object sender, EventArgs e)
		{
			this.UpdateThumbnails(this._curAlbum);
		}
		private void listView_PhotoMetadataChanged(object sender, PhotoMetadataChangedEventArgs e)
		{
			if (this.PhotoMetadataChangedEvent != null)
			{
				this.PhotoMetadataChangedEvent(this, e);
			}
		}
		private void listView_DoubleClick(object sender, EventArgs e)
		{
			if (this.listView.SelectedItems.Count == 1 && this.OpenPhotoEvent != null)
			{
				this.OpenPhotoEvent(this, EventArgs.Empty);
			}
		}
		private void listView_KeyUp(object sender, KeyEventArgs e)
		{
			this.CheckSelectionChange();
		}
		private void listView_MouseUp(object sender, MouseEventArgs e)
		{
			this.CheckSelectionChange();
		}
		private void photoViewer_MouseDown(object sender, MouseEventArgs e)
		{
			if (this.FullScreen)
			{
				if (e.Button == 1048576 && this.PhotosMenuClickedEvent != null)
				{
					this.PhotosMenuClickedEvent(this, new PhotosMenuClickedEventArgs(PhotosContextAction.NextPhoto));
				}
				if (e.Button == 2097152 && this.PhotosMenuClickedEvent != null)
				{
					this.PhotosMenuClickedEvent(this, new PhotosMenuClickedEventArgs(PhotosContextAction.PreviousPhoto));
				}
			}
		}
		private void photoViewer_CropDataChanged(object sender, CropDataChangedEventArgs e)
		{
			if (this.CropDataChangedEvent != null)
			{
				this.CropDataChangedEvent(this, e);
			}
		}
		private void menuThumbnails_Popup(object sender, EventArgs e)
		{
			bool enabled = BooleanType.FromObject(Interaction.IIf(this.Count > 0 & this.SelectedCount == 1, true, false));
			bool enabled2 = BooleanType.FromObject(Interaction.IIf(this.Count > 0 & this.SelectedCount > 0, true, false));
			bool enabled3 = BooleanType.FromObject(Interaction.IIf(this.Count > 0, true, false));
			this.menuOpen.Enabled = enabled3;
			this.menuPhotoShow.Enabled = enabled3;
			this.menuPhotoShowDetails.Enabled = enabled3;
			this.menuPhotoActions.Enabled = enabled;
			this.menuSelectAll.Enabled = enabled3;
			this.menuRotateLeft.Enabled = enabled2;
			this.menuRotateRight.Enabled = enabled2;
			this.menuRename.Enabled = enabled;
			this.menuDelete.Enabled = enabled2;
			this.menuProperties.Enabled = enabled;
		}
		private void menu_Click(object sender, EventArgs e)
		{
			MenuItem menuItem = (MenuItem)sender;
			int hashCode = menuItem.GetHashCode();
			if (hashCode == this.menuPhotoShow.GetHashCode())
			{
				if (this.PhotosMenuClickedEvent != null)
				{
					this.PhotosMenuClickedEvent(this, new PhotosMenuClickedEventArgs(PhotosContextAction.PhotoShow));
				}
			}
			else
			{
				if (hashCode == this.menuPhotoShowDetails.GetHashCode())
				{
					if (this.PhotosMenuClickedEvent != null)
					{
						this.PhotosMenuClickedEvent(this, new PhotosMenuClickedEventArgs(PhotosContextAction.PhotoShowDetails));
					}
				}
				else
				{
					if (hashCode == this.menuPhotoActions.GetHashCode())
					{
						if (this.PhotosMenuClickedEvent != null)
						{
							this.PhotosMenuClickedEvent(this, new PhotosMenuClickedEventArgs(PhotosContextAction.PhotoActions));
						}
					}
					else
					{
						if (hashCode == this.menuSelectAll.GetHashCode())
						{
							if (this.PhotosMenuClickedEvent != null)
							{
								this.PhotosMenuClickedEvent(this, new PhotosMenuClickedEventArgs(PhotosContextAction.SelectAll));
							}
						}
						else
						{
							if (hashCode == this.menuRotateLeft.GetHashCode())
							{
								if (this.PhotosMenuClickedEvent != null)
								{
									this.PhotosMenuClickedEvent(this, new PhotosMenuClickedEventArgs(PhotosContextAction.RotateLeft));
								}
							}
							else
							{
								if (hashCode == this.menuRotateRight.GetHashCode())
								{
									if (this.PhotosMenuClickedEvent != null)
									{
										this.PhotosMenuClickedEvent(this, new PhotosMenuClickedEventArgs(PhotosContextAction.RotateRight));
									}
								}
								else
								{
									if (hashCode == this.menuRename.GetHashCode())
									{
										if (this.PhotosMenuClickedEvent != null)
										{
											this.PhotosMenuClickedEvent(this, new PhotosMenuClickedEventArgs(PhotosContextAction.Rename));
										}
									}
									else
									{
										if (hashCode == this.menuDelete.GetHashCode() && this.PhotosMenuClickedEvent != null)
										{
											this.PhotosMenuClickedEvent(this, new PhotosMenuClickedEventArgs(PhotosContextAction.Delete));
										}
									}
								}
							}
						}
					}
				}
			}
		}
		private void menuProperties_Click(object sender, EventArgs e)
		{
			Photo photo = (Photo)Interaction.IIf(this.Mode == PhotosMode.Thumbnails, this.SelectedPhoto, this.photoViewer.Photo);
			PropertiesForm propertiesForm = new PropertiesForm(photo.PhotoPath);
			propertiesForm.ShowDialog(this);
		}
	}
}
