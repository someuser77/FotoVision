using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace FotoVision
{
	public class AlbumsPane : BasePane
	{
		private enum PublishImage
		{
			Yes,
			No
		}
		private class Consts
		{
			public const string CaptionText = "My Albums";
			public const string DeleteAlbumTitle = "Confirm Album Delete";
			public const string ImportPhotoDialogTitle = "Import Photos";
			public const string ImportFolderDescription = "Please select a folder to import. If you delete this album in PhotoVision, the source files will not be deleted, only the copied files will be erased.";
		}
		public delegate void SelectedAlbumChangedEventHandler(object sender, EventArgs e);
		public delegate void SelectedAlbumClickedEventHandler(object sender, EventArgs e);
		public delegate void AlbumRenamedEventHandler(object sender, AlbumRenamedEventArgs e);
		public delegate void PhotosRemovedEventHandler(object sender, AlbumNameEventArgs e);
		public delegate void PublishAlbumClickedEventHandler(object sender, PublishAlbumEventArgs e);
		public delegate void PhotosAddedEventHandler(object sender, AlbumNameEventArgs e);
		public delegate void NewAlbumCreatedEventHandler(object sender, AlbumNameEventArgs e);
		[AccessedThroughProperty("menuSep1")]
		private MenuItem _menuSep1;
		[AccessedThroughProperty("colPhotos")]
		private ColumnHeader _colPhotos;
		private AlbumsPane.SelectedAlbumChangedEventHandler SelectedAlbumChangedEvent;
		[AccessedThroughProperty("menuSep2")]
		private MenuItem _menuSep2;
		[AccessedThroughProperty("colName")]
		private ColumnHeader _colName;
		[AccessedThroughProperty("menuImportPhotos")]
		private MenuItem _menuImportPhotos;
		private AlbumsPane.SelectedAlbumClickedEventHandler SelectedAlbumClickedEvent;
		private AlbumsPane.AlbumRenamedEventHandler AlbumRenamedEvent;
		[AccessedThroughProperty("menuPublish")]
		private MenuItem _menuPublish;
		private AlbumsPane.PhotosRemovedEventHandler PhotosRemovedEvent;
		[AccessedThroughProperty("menuAlbum")]
		private ContextMenu _menuAlbum;
		private AlbumsPane.PublishAlbumClickedEventHandler PublishAlbumClickedEvent;
		[AccessedThroughProperty("menuDelete")]
		private MenuItem _menuDelete;
		private AlbumsPane.PhotosAddedEventHandler PhotosAddedEvent;
		private AlbumsPane.NewAlbumCreatedEventHandler NewAlbumCreatedEvent;
		[AccessedThroughProperty("menuRename")]
		private MenuItem _menuRename;
		[AccessedThroughProperty("imageList")]
		private ImageList _imageList;
		[AccessedThroughProperty("menuNewAlbum")]
		private MenuItem _menuNewAlbum;
		[AccessedThroughProperty("listView")]
		private ListView _listView;
		[AccessedThroughProperty("menuImportFolder")]
		private MenuItem _menuImportFolder;
		private DropData _dropData;
		private bool _ignoreSelChange;
		private ListViewItem _orgSelection;
		private bool _albumChanged;
		private bool _inLabelEdit;
		private IContainer components;
		public event AlbumsPane.SelectedAlbumChangedEventHandler SelectedAlbumChanged
		{
			[MethodImpl(32)]
			add
			{
				this.SelectedAlbumChangedEvent = (AlbumsPane.SelectedAlbumChangedEventHandler)Delegate.Combine(this.SelectedAlbumChangedEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.SelectedAlbumChangedEvent = (AlbumsPane.SelectedAlbumChangedEventHandler)Delegate.Remove(this.SelectedAlbumChangedEvent, value);
			}
		}
		public event AlbumsPane.SelectedAlbumClickedEventHandler SelectedAlbumClicked
		{
			[MethodImpl(32)]
			add
			{
				this.SelectedAlbumClickedEvent = (AlbumsPane.SelectedAlbumClickedEventHandler)Delegate.Combine(this.SelectedAlbumClickedEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.SelectedAlbumClickedEvent = (AlbumsPane.SelectedAlbumClickedEventHandler)Delegate.Remove(this.SelectedAlbumClickedEvent, value);
			}
		}
		public event AlbumsPane.AlbumRenamedEventHandler AlbumRenamed
		{
			[MethodImpl(32)]
			add
			{
				this.AlbumRenamedEvent = (AlbumsPane.AlbumRenamedEventHandler)Delegate.Combine(this.AlbumRenamedEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.AlbumRenamedEvent = (AlbumsPane.AlbumRenamedEventHandler)Delegate.Remove(this.AlbumRenamedEvent, value);
			}
		}
		public event AlbumsPane.PhotosRemovedEventHandler PhotosRemoved
		{
			[MethodImpl(32)]
			add
			{
				this.PhotosRemovedEvent = (AlbumsPane.PhotosRemovedEventHandler)Delegate.Combine(this.PhotosRemovedEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.PhotosRemovedEvent = (AlbumsPane.PhotosRemovedEventHandler)Delegate.Remove(this.PhotosRemovedEvent, value);
			}
		}
		public event AlbumsPane.PublishAlbumClickedEventHandler PublishAlbumClicked
		{
			[MethodImpl(32)]
			add
			{
				this.PublishAlbumClickedEvent = (AlbumsPane.PublishAlbumClickedEventHandler)Delegate.Combine(this.PublishAlbumClickedEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.PublishAlbumClickedEvent = (AlbumsPane.PublishAlbumClickedEventHandler)Delegate.Remove(this.PublishAlbumClickedEvent, value);
			}
		}
		public event AlbumsPane.PhotosAddedEventHandler PhotosAdded
		{
			[MethodImpl(32)]
			add
			{
				this.PhotosAddedEvent = (AlbumsPane.PhotosAddedEventHandler)Delegate.Combine(this.PhotosAddedEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.PhotosAddedEvent = (AlbumsPane.PhotosAddedEventHandler)Delegate.Remove(this.PhotosAddedEvent, value);
			}
		}
		public event AlbumsPane.NewAlbumCreatedEventHandler NewAlbumCreated
		{
			[MethodImpl(32)]
			add
			{
				this.NewAlbumCreatedEvent = (AlbumsPane.NewAlbumCreatedEventHandler)Delegate.Combine(this.NewAlbumCreatedEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.NewAlbumCreatedEvent = (AlbumsPane.NewAlbumCreatedEventHandler)Delegate.Remove(this.NewAlbumCreatedEvent, value);
			}
		}
		[Browsable(false)]
		public int Count
		{
			get
			{
				return this.listView.get_Items().get_Count();
			}
		}
		[Browsable(false)]
		public int SelectedCount
		{
			get
			{
				return this.listView.get_SelectedItems().get_Count();
			}
		}
		[Browsable(false)]
		public string SelectedAlbum
		{
			get
			{
				if (this.listView.get_SelectedItems().get_Count() == 1)
				{
					return this.listView.get_SelectedItems().get_Item(0).get_Text();
				}
				return "";
			}
			set
			{
				try
				{
					IEnumerator enumerator = this.listView.get_Items().GetEnumerator();
					while (enumerator.MoveNext())
					{
						ListViewItem listViewItem = (ListViewItem)enumerator.get_Current();
						if (StringType.StrCmp(listViewItem.get_Text(), value, false) == 0)
						{
							listViewItem.set_Selected(true);
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
		}
		[Browsable(false)]
		public ListViewItem SelectedItem
		{
			get
			{
				if (this.listView.get_SelectedItems().get_Count() == 1)
				{
					return this.listView.get_SelectedItems().get_Item(0);
				}
				return null;
			}
		}
		private virtual ListView listView
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
					this._listView.remove_SelectedIndexChanged(new EventHandler(this.listView_SelectedIndexChanged));
					this._listView.remove_AfterLabelEdit(new LabelEditEventHandler(this.listView_AfterLabelEdit));
					this._listView.remove_BeforeLabelEdit(new LabelEditEventHandler(this.listView_BeforeLabelEdit));
					this._listView.remove_DragDrop(new DragEventHandler(this.listView_DragDrop));
					this._listView.remove_DragLeave(new EventHandler(this.listView_DragLeave));
					this._listView.remove_DragOver(new DragEventHandler(this.listView_DragOver));
					this._listView.remove_DragEnter(new DragEventHandler(this.listView_DragEnter));
					this._listView.remove_ItemActivate(new EventHandler(this.listView_ItemActivate));
				}
				this._listView = value;
				if (this._listView != null)
				{
					this._listView.add_SelectedIndexChanged(new EventHandler(this.listView_SelectedIndexChanged));
					this._listView.add_AfterLabelEdit(new LabelEditEventHandler(this.listView_AfterLabelEdit));
					this._listView.add_BeforeLabelEdit(new LabelEditEventHandler(this.listView_BeforeLabelEdit));
					this._listView.add_DragDrop(new DragEventHandler(this.listView_DragDrop));
					this._listView.add_DragLeave(new EventHandler(this.listView_DragLeave));
					this._listView.add_DragOver(new DragEventHandler(this.listView_DragOver));
					this._listView.add_DragEnter(new DragEventHandler(this.listView_DragEnter));
					this._listView.add_ItemActivate(new EventHandler(this.listView_ItemActivate));
				}
			}
		}
		private virtual MenuItem menuImportPhotos
		{
			get
			{
				return this._menuImportPhotos;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuImportPhotos != null)
				{
					this._menuImportPhotos.remove_Click(new EventHandler(this.menuImportPhotos_Click));
				}
				this._menuImportPhotos = value;
				if (this._menuImportPhotos != null)
				{
					this._menuImportPhotos.add_Click(new EventHandler(this.menuImportPhotos_Click));
				}
			}
		}
		private virtual MenuItem menuImportFolder
		{
			get
			{
				return this._menuImportFolder;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuImportFolder != null)
				{
					this._menuImportFolder.remove_Click(new EventHandler(this.menuImportFolder_Click));
				}
				this._menuImportFolder = value;
				if (this._menuImportFolder != null)
				{
					this._menuImportFolder.add_Click(new EventHandler(this.menuImportFolder_Click));
				}
			}
		}
		private virtual MenuItem menuNewAlbum
		{
			get
			{
				return this._menuNewAlbum;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuNewAlbum != null)
				{
					this._menuNewAlbum.remove_Click(new EventHandler(this.menuNewAlbum_Click));
				}
				this._menuNewAlbum = value;
				if (this._menuNewAlbum != null)
				{
					this._menuNewAlbum.add_Click(new EventHandler(this.menuNewAlbum_Click));
				}
			}
		}
		private virtual MenuItem menuRename
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
					this._menuRename.remove_Click(new EventHandler(this.menuRename_Click));
				}
				this._menuRename = value;
				if (this._menuRename != null)
				{
					this._menuRename.add_Click(new EventHandler(this.menuRename_Click));
				}
			}
		}
		private virtual MenuItem menuDelete
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
					this._menuDelete.remove_Click(new EventHandler(this.menuDelete_Click));
				}
				this._menuDelete = value;
				if (this._menuDelete != null)
				{
					this._menuDelete.add_Click(new EventHandler(this.menuDelete_Click));
				}
			}
		}
		private virtual ContextMenu menuAlbum
		{
			get
			{
				return this._menuAlbum;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuAlbum != null)
				{
					this._menuAlbum.remove_Popup(new EventHandler(this.menuAlbum_Popup));
				}
				this._menuAlbum = value;
				if (this._menuAlbum != null)
				{
					this._menuAlbum.add_Popup(new EventHandler(this.menuAlbum_Popup));
				}
			}
		}
		private virtual MenuItem menuPublish
		{
			get
			{
				return this._menuPublish;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuPublish != null)
				{
					this._menuPublish.remove_Click(new EventHandler(this.menuPublish_Click));
				}
				this._menuPublish = value;
				if (this._menuPublish != null)
				{
					this._menuPublish.add_Click(new EventHandler(this.menuPublish_Click));
				}
			}
		}
		private virtual ColumnHeader colName
		{
			get
			{
				return this._colName;
			}
			[MethodImpl(32)]
			set
			{
				if (this._colName != null)
				{
				}
				this._colName = value;
				if (this._colName != null)
				{
				}
			}
		}
		private virtual ColumnHeader colPhotos
		{
			get
			{
				return this._colPhotos;
			}
			[MethodImpl(32)]
			set
			{
				if (this._colPhotos != null)
				{
				}
				this._colPhotos = value;
				if (this._colPhotos != null)
				{
				}
			}
		}
		private virtual ImageList imageList
		{
			get
			{
				return this._imageList;
			}
			[MethodImpl(32)]
			set
			{
				if (this._imageList != null)
				{
				}
				this._imageList = value;
				if (this._imageList != null)
				{
				}
			}
		}
		private virtual MenuItem menuSep1
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
		private virtual MenuItem menuSep2
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
		public AlbumsPane()
		{
			base.add_Resize(new EventHandler(this.AlbumsPane_Resize));
			this._ignoreSelChange = false;
			this._inLabelEdit = false;
			this.InitializeComponent();
			this.CaptionText = "My Albums";
			this._dropData = new DropData(this.listView);
			this.listView.set_Activation(1);
		}
		private void InitializeComponent()
		{
			this.components = new Container();
			ResourceManager resourceManager = new ResourceManager(typeof(AlbumsPane));
			this.listView = new ListView();
			this.colName = new ColumnHeader();
			this.colPhotos = new ColumnHeader();
			this.menuAlbum = new ContextMenu();
			this.menuImportPhotos = new MenuItem();
			this.menuImportFolder = new MenuItem();
			this.menuSep1 = new MenuItem();
			this.menuPublish = new MenuItem();
			this.menuSep2 = new MenuItem();
			this.menuNewAlbum = new MenuItem();
			this.menuRename = new MenuItem();
			this.menuDelete = new MenuItem();
			this.imageList = new ImageList(this.components);
			this.SuspendLayout();
			this.listView.set_AllowDrop(true);
			this.listView.set_BorderStyle(0);
			this.listView.get_Columns().AddRange(new ColumnHeader[]
			{
				this.colName,
				this.colPhotos
			});
			this.listView.set_ContextMenu(this.menuAlbum);
			this.listView.set_Dock(5);
			this.listView.set_HeaderStyle(1);
			this.listView.set_HideSelection(false);
			this.listView.set_LabelEdit(true);
			Control arg_14E_0 = this.listView;
			Point location = new Point(2, 22);
			arg_14E_0.set_Location(location);
			this.listView.set_MultiSelect(false);
			this.listView.set_Name("listView");
			Control arg_187_0 = this.listView;
			Size size = new Size(220, 240);
			arg_187_0.set_Size(size);
			this.listView.set_SmallImageList(this.imageList);
			this.listView.set_TabIndex(1);
			this.listView.set_View(1);
			this.colName.set_Text("Album Name");
			this.colName.set_Width(162);
			this.colPhotos.set_Text("Photos");
			this.colPhotos.set_TextAlign(1);
			this.colPhotos.set_Width(50);
			this.menuAlbum.get_MenuItems().AddRange(new MenuItem[]
			{
				this.menuImportPhotos,
				this.menuImportFolder,
				this.menuSep1,
				this.menuPublish,
				this.menuSep2,
				this.menuNewAlbum,
				this.menuRename,
				this.menuDelete
			});
			this.menuImportPhotos.set_Index(0);
			this.menuImportPhotos.set_Text("Import Photos...");
			this.menuImportFolder.set_Index(1);
			this.menuImportFolder.set_Text("Import Folder...");
			this.menuSep1.set_Index(2);
			this.menuSep1.set_Text("-");
			this.menuPublish.set_Index(3);
			this.menuPublish.set_Text("Publish Album");
			this.menuSep2.set_Index(4);
			this.menuSep2.set_Text("-");
			this.menuNewAlbum.set_Index(5);
			this.menuNewAlbum.set_Text("New Album");
			this.menuRename.set_Index(6);
			this.menuRename.set_Text("Rename");
			this.menuDelete.set_Index(7);
			this.menuDelete.set_Text("Delete");
			ImageList arg_350_0 = this.imageList;
			size = new Size(24, 18);
			arg_350_0.set_ImageSize(size);
			this.imageList.set_ImageStream((ImageListStreamer)resourceManager.GetObject("imageList.ImageStream"));
			this.imageList.set_TransparentColor(Color.get_Lime());
			this.get_Controls().Add(this.listView);
			this.get_DockPadding().set_All(2);
			this.set_Name("AlbumsPane");
			size = new Size(224, 264);
			this.set_Size(size);
			this.get_Controls().SetChildIndex(this.listView, 0);
			this.ResumeLayout(false);
		}
		public void ImportPhotos()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.set_Title("Import Photos");
			openFileDialog.set_Multiselect(true);
			openFileDialog.set_Filter(FileManager.OpenFilter);
			openFileDialog.set_FilterIndex(Global.Settings.GetInt(SettingKey.ImportFilterIndex));
			openFileDialog.set_InitialDirectory(Global.Settings.GetString(SettingKey.ImportLocation));
			if (openFileDialog.ShowDialog() == 1)
			{
				Global.Settings.SetValue(SettingKey.ImportFilterIndex, openFileDialog.get_FilterIndex());
				if (openFileDialog.get_FileNames().get_Length() > 0)
				{
					Global.Settings.SetValue(SettingKey.ImportLocation, Path.GetDirectoryName(openFileDialog.get_FileName()));
				}
				this.ImportFiles(openFileDialog.get_FileNames());
			}
		}
		public void ImportFolder()
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			folderBrowserDialog.set_Description("Please select a folder to import. If you delete this album in PhotoVision, the source files will not be deleted, only the copied files will be erased.");
			folderBrowserDialog.set_ShowNewFolderButton(false);
			folderBrowserDialog.set_SelectedPath(Global.Settings.GetString(SettingKey.ImportLocation));
			if (folderBrowserDialog.ShowDialog() == 1)
			{
				Global.Settings.SetValue(SettingKey.ImportLocation, folderBrowserDialog.get_SelectedPath());
				this.ProcessDroppedFilesRoot(new string[]
				{
					folderBrowserDialog.get_SelectedPath()
				}, false);
			}
		}
		public void Rename()
		{
			if (this._inLabelEdit)
			{
				return;
			}
			if (this.listView.get_SelectedItems().get_Count() == 1)
			{
				this.listView.get_SelectedItems().get_Item(0).BeginEdit();
			}
		}
		public void Delete()
		{
			if (this._inLabelEdit)
			{
				return;
			}
			int photoCount = FileManager.GetPhotoCount(this.SelectedAlbum);
			string text = StringType.FromObject(Interaction.IIf(photoCount == 0, "The album will be deleted. Do you want to continue?", "All the photos in the album will be deleted. FotoVision only deletes the copies of the original photos it created during the import.\r\n\r\nDo you want to continue?"));
			DialogResult dialogResult = MessageBox.Show(this.get_TopLevelControl(), text, "Confirm Album Delete", 4, 32);
			if (dialogResult != 6)
			{
				return;
			}
			if (!FileManager.DeleteAlbum(this.SelectedAlbum))
			{
				if (this.SelectedAlbumChangedEvent != null)
				{
					this.SelectedAlbumChangedEvent(this, EventArgs.Empty);
					return;
				}
			}
			else
			{
				ListViewItem selectedItem = this.SelectedItem;
				int index = selectedItem.get_Index();
				selectedItem.Remove();
				if (index < this.Count)
				{
					this.listView.get_Items().get_Item(index).set_Selected(true);
				}
				else
				{
					if (this.Count > 0)
					{
						this.listView.get_Items().get_Item(checked(index - 1)).set_Selected(true);
					}
					else
					{
						if (this.SelectedAlbumChangedEvent != null)
						{
							this.SelectedAlbumChangedEvent(this, EventArgs.Empty);
						}
					}
				}
			}
		}
		public void CreateNewAlbum()
		{
			try
			{
				string albumName = FileManager.AddNewAlbum();
				this.AddItem(albumName);
				if (this.NewAlbumCreatedEvent != null)
				{
					this.NewAlbumCreatedEvent(this, new AlbumNameEventArgs(albumName));
				}
			}
			catch (Exception expr_2A)
			{
				ProjectData.SetProjectError(expr_2A);
				Exception ex = expr_2A;
				Global.DisplayError("A new album could not be created.", ex);
				ProjectData.ClearProjectError();
			}
		}
		public string[] GetPublishList()
		{
			checked
			{
				int num;
				try
				{
					IEnumerator enumerator = this.listView.get_Items().GetEnumerator();
					while (enumerator.MoveNext())
					{
						ListViewItem listViewItem = (ListViewItem)enumerator.get_Current();
						if (listViewItem.get_ImageIndex() == 0)
						{
							num++;
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
				if (num == 0)
				{
					return null;
				}
				string[] array = new string[num - 1 + 1];
				try
				{
					IEnumerator enumerator2 = this.listView.get_Items().GetEnumerator();
					while (enumerator2.MoveNext())
					{
						ListViewItem listViewItem2 = (ListViewItem)enumerator2.get_Current();
						if (listViewItem2.get_ImageIndex() == 0)
						{
							int num2;
							array[num2] = listViewItem2.get_Text();
							num2++;
						}
					}
				}
				finally
				{
					IEnumerator enumerator2;
					if (enumerator2 is IDisposable)
					{
						((IDisposable)enumerator2).Dispose();
					}
				}
				return array;
			}
		}
		public void SelectFirstAlbum()
		{
			if (this.listView.get_Items().get_Count() == 0)
			{
				return;
			}
			this.listView.get_Items().get_Item(0).set_Selected(true);
		}
		public void AddBitmap(Bitmap image)
		{
			try
			{
				ListViewItem listViewItem = this.SelectedItem;
				if (listViewItem == null)
				{
					string albumName = FileManager.AddNewAlbum();
					listViewItem = this.AddItem(albumName);
				}
				string text = listViewItem.get_Text();
				string newPhotoName = FileManager.GetNewPhotoName(text);
				image.Save(Path.Combine(FileManager.GetLocation(text), newPhotoName), ImageFormat.get_Jpeg());
				if (this.PhotosAddedEvent != null)
				{
					this.PhotosAddedEvent(this, new AlbumNameEventArgs(text));
				}
			}
			catch (Exception expr_59)
			{
				ProjectData.SetProjectError(expr_59);
				Exception ex = expr_59;
				Global.DisplayError("An error occurred adding the file.", ex);
				ProjectData.ClearProjectError();
			}
		}
		public void ImportFiles(string[] files)
		{
			ListViewItem listViewItem = this.SelectedItem;
			if (listViewItem == null)
			{
				string albumName = FileManager.AddNewAlbum();
				listViewItem = this.AddItem(albumName);
			}
			this.ProcessDroppedFiles(files, listViewItem, false);
			if (this.PhotosAddedEvent != null)
			{
				this.PhotosAddedEvent(this, new AlbumNameEventArgs(listViewItem.get_Text()));
			}
		}
		public bool UpdateAlbum(string albumName, Album album)
		{
			ListViewItem listViewItem = this.FindItem(albumName);
			listViewItem.get_SubItems().get_Item(1).set_Text(this.GetPhotoCount(album.Name));
			listViewItem.set_ImageIndex(IntegerType.FromObject(Interaction.IIf(album.Publish, AlbumsPane.PublishImage.Yes, AlbumsPane.PublishImage.No)));
			if (StringType.StrCmp(albumName, album.Name, false) != 0)
			{
				if (!this.RenameAlbum(albumName, album.Name))
				{
					return false;
				}
				listViewItem.set_Text(album.Name);
			}
			return true;
		}
		public void UpdateAlbumCount(string albumName)
		{
			ListViewItem listViewItem = this.FindItem(albumName);
			if (listViewItem == null)
			{
				return;
			}
			listViewItem.get_SubItems().get_Item(1).set_Text(this.GetPhotoCount(albumName));
		}
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.InitItems();
		}
		private void AlbumsPane_Resize(object sender, EventArgs e)
		{
			int num = this.listView.get_Columns().get_Item(1).get_Width();
			checked
			{
				if (this.listView.get_Width() > this.listView.get_DisplayRectangle().get_Width() + SystemInformation.get_Border3DSize().get_Width() * 2)
				{
					num += SystemInformation.get_VerticalScrollBarWidth();
				}
				this.listView.get_Columns().get_Item(0).set_Width(this.listView.get_Width() - num);
			}
		}
		private void listView_BeforeLabelEdit(object sender, LabelEditEventArgs e)
		{
			this._inLabelEdit = true;
		}
		private void listView_AfterLabelEdit(object sender, LabelEditEventArgs e)
		{
			this._inLabelEdit = false;
			if (e.get_Label() == null)
			{
				e.set_CancelEdit(true);
				return;
			}
			string text = this.listView.get_Items().get_Item(e.get_Item()).get_Text();
			string text2 = e.get_Label();
			if (!FileManager.IsValidAlbumName(text2))
			{
				e.set_CancelEdit(true);
				return;
			}
			if (text2.StartsWith(" ") || text2.EndsWith(" "))
			{
				e.set_CancelEdit(true);
				return;
			}
			if (FileManager.AlbumExists(text2.Trim()))
			{
				MessageBox.Show(this.get_TopLevelControl(), string.Format("The album '{0}' already exist. Please use a different album name.", text2.Trim()), "Cannot Rename Album", 0, 48);
				e.set_CancelEdit(true);
				return;
			}
			text2 = text2.Trim();
			if (!this.RenameAlbum(text, text2))
			{
				e.set_CancelEdit(true);
				return;
			}
			if (this.AlbumRenamedEvent != null)
			{
				this.AlbumRenamedEvent(this, new AlbumRenamedEventArgs(text, text2));
			}
		}
		private void listView_SelectedIndexChanged(object sender, EventArgs e)
		{
			if ((!this._dropData.IsInDragDrop & !this._ignoreSelChange) && this.SelectedAlbumChangedEvent != null)
			{
				this.SelectedAlbumChangedEvent(this, EventArgs.Empty);
			}
			this._albumChanged = true;
		}
		private void listView_ItemActivate(object sender, EventArgs e)
		{
			if (!this._albumChanged && this.SelectedAlbumClickedEvent != null)
			{
				this.SelectedAlbumClickedEvent(this, EventArgs.Empty);
			}
			this._albumChanged = false;
		}
		private ListViewItem FindItem(string itemName)
		{
			try
			{
				IEnumerator enumerator = this.listView.get_Items().GetEnumerator();
				while (enumerator.MoveNext())
				{
					ListViewItem listViewItem = (ListViewItem)enumerator.get_Current();
					if (StringType.StrCmp(listViewItem.get_Text(), itemName, false) == 0)
					{
						return listViewItem;
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
			return null;
		}
		private bool RenameAlbum(string oldName, string newName)
		{
			bool result;
			try
			{
				if (StringType.StrCmp(oldName, newName, false) != 0)
				{
					FileManager.RenameAlbum(oldName, newName);
				}
				result = true;
			}
			catch (Exception expr_16)
			{
				ProjectData.SetProjectError(expr_16);
				Exception ex = expr_16;
				Global.DisplayError(string.Format("The album '{0}' could not be renamed.", oldName), ex);
				result = false;
				ProjectData.ClearProjectError();
			}
			return result;
		}
		private void InitItems()
		{
			this.listView.get_Items().Clear();
			string[] albums = FileManager.GetAlbums();
			checked
			{
				if (albums != null)
				{
					string[] array = albums;
					for (int i = 0; i < array.Length; i++)
					{
						string albumName = array[i];
						this.AddItem(albumName);
					}
				}
			}
		}
		private ListViewItem AddItem(string albumName)
		{
			Album album = new Album();
			album.ReadXml(albumName);
			ListViewItem listViewItem = this.listView.get_Items().Add(albumName);
			listViewItem.get_SubItems().Add(this.GetPhotoCount(album.Name));
			listViewItem.set_ImageIndex(IntegerType.FromObject(Interaction.IIf(album.Publish, AlbumsPane.PublishImage.Yes, AlbumsPane.PublishImage.No)));
			return listViewItem;
		}
		private string GetPhotoCount(string albumName)
		{
			return StringType.FromInteger(FileManager.GetPhotoCount(albumName)) + " ";
		}
		private void ReselectOrgSelection()
		{
			if (this._orgSelection != null)
			{
				this._ignoreSelChange = true;
				this._orgSelection.set_Selected(true);
				this._orgSelection = null;
				this._ignoreSelChange = false;
			}
		}
		private void ProcessDroppedFiles(string[] files, ListViewItem item, bool moveFiles)
		{
			string text = item.get_Text();
			DialogResult dialogResult = 1;
			bool flag = false;
			this.AddFiles(text, files, moveFiles, ref dialogResult, ref flag);
			int arg_23_0 = 0;
			checked
			{
				int num = files.get_Length() - 1;
				int num2 = arg_23_0;
				while (num2 <= num && dialogResult != 2)
				{
					if (Directory.Exists(files[num2]))
					{
						string[] photoFileList = FileManager.GetPhotoFileList(files[num2]);
						if (photoFileList != null)
						{
							this.AddFiles(text, photoFileList, moveFiles, ref dialogResult, ref flag);
						}
					}
					num2++;
				}
				Global.Progress.Complete(this);
			}
		}
		private void ProcessDroppedFilesRoot(string[] files, bool moveFiles)
		{
			DialogResult dialogResult = 1;
			bool flag = false;
			bool flag2 = false;
			int arg_11_0 = 0;
			checked
			{
				int num = files.get_Length() - 1;
				int num2 = arg_11_0;
				while (num2 <= num && !flag2)
				{
					flag2 = FileManager.IsSupportedFile(files[num2]);
					num2++;
				}
				ListViewItem listViewItem;
				if (flag2)
				{
					string text = FileManager.AddNewAlbum();
					listViewItem = this.AddItem(text);
					this.AddFiles(text, files, moveFiles, ref dialogResult, ref flag);
				}
				int arg_5A_0 = 0;
				int num3 = files.get_Length() - 1;
				int num4 = arg_5A_0;
				while (num4 <= num3 && dialogResult != 2)
				{
					if (Directory.Exists(files[num4]))
					{
						string fileName = Path.GetFileName(files[num4]);
						if (!FileManager.AlbumExists(fileName))
						{
							FileManager.AddAlbum(fileName);
							listViewItem = this.AddItem(fileName);
						}
						string[] photoFileList = FileManager.GetPhotoFileList(files[num4]);
						if (photoFileList != null)
						{
							this.AddFiles(fileName, photoFileList, moveFiles, ref dialogResult, ref flag);
						}
					}
					num4++;
				}
				Global.Progress.Complete(this);
				if (listViewItem != null && this.PhotosAddedEvent != null)
				{
					this.PhotosAddedEvent(this, new AlbumNameEventArgs(listViewItem.get_Text()));
				}
			}
		}
		private void AddFiles(string folder, string[] files, bool moveFiles, ref DialogResult result, ref bool alwaysReplaceFile)
		{
			int arg_0A_0 = 0;
			checked
			{
				int num = files.get_Length() - 1;
				int num2 = arg_0A_0;
				while (num2 <= num && result != 2)
				{
					Global.Progress.Update(this, string.Format("{0} files", RuntimeHelpers.GetObjectValue(Interaction.IIf(moveFiles, "Moving", "Copying"))), num2 + 1, files.get_Length());
					if (FileManager.IsSupportedFile(files[num2]))
					{
						result = this.AddFile(folder, files[num2], alwaysReplaceFile, moveFiles);
						if (result == 6)
						{
							alwaysReplaceFile = true;
						}
					}
					num2++;
				}
				this.UpdateAlbumCount(folder);
				if (moveFiles)
				{
					string fileName = Path.GetFileName(Path.GetDirectoryName(files[0]));
					this.UpdateAlbumCount(fileName);
					if (this.PhotosRemovedEvent != null)
					{
						this.PhotosRemovedEvent(this, new AlbumNameEventArgs(fileName));
					}
				}
			}
		}
		private DialogResult AddFile(string folder, string file, bool alwaysReplaceFile, bool moveFile)
		{
			DialogResult dialogResult = 1;
			try
			{
				string text = Path.Combine(FileManager.GetLocation(folder), Path.GetFileName(file));
				if (StringType.StrCmp(text, file, false) == 0)
				{
					return dialogResult;
				}
				if (Global.Settings.GetBool(SettingKey.PromptInitialMessage))
				{
					InitialMessageForm initialMessageForm = new InitialMessageForm();
					initialMessageForm.ShowDialog(this);
				}
				if (File.Exists(text) & !alwaysReplaceFile)
				{
					ConfirmPhotoForm confirmPhotoForm = new ConfirmPhotoForm(text, file);
					dialogResult = confirmPhotoForm.ShowDialog(this.get_ParentForm());
				}
				if (dialogResult == 1 | dialogResult == 6)
				{
					FileManager.CopyMetaFile(file, text);
					FileManager.CopyFile(file, text);
					if (moveFile)
					{
						FileManager.DeleteMetaFile(file);
						FileManager.DeleteFile(file);
					}
				}
			}
			catch (Exception expr_92)
			{
				ProjectData.SetProjectError(expr_92);
				Exception ex = expr_92;
				Global.DisplayError("An error occurred adding the file.", ex);
				ProjectData.ClearProjectError();
			}
			return dialogResult;
		}
		private void listView_DragEnter(object sender, DragEventArgs e)
		{
			this._orgSelection = this.SelectedItem;
			this._dropData.Enter(e);
		}
		private void listView_DragOver(object sender, DragEventArgs e)
		{
			this._dropData.Over(e);
		}
		private void listView_DragLeave(object sender, EventArgs e)
		{
			this._dropData.Leave();
			if (this._orgSelection != null)
			{
				this._ignoreSelChange = true;
				this._orgSelection.set_Selected(true);
				this._ignoreSelChange = false;
			}
		}
		private void listView_DragDrop(object sender, DragEventArgs e)
		{
			string[] array = this._dropData.Drop(e);
			if (array == null || array.get_Length() == 0)
			{
				this.ReselectOrgSelection();
				return;
			}
			if (this._dropData.MouseButtons == 2097152)
			{
				DropContextMenu dropContextMenu = new DropContextMenu();
				if (!Global.PerformingDrag)
				{
					dropContextMenu.EnableMove = false;
				}
				e.set_Effect(dropContextMenu.Display(this));
			}
			if (e.get_Effect() == 0)
			{
				this.ReselectOrgSelection();
				return;
			}
			this.ReselectOrgSelection();
			bool moveFiles = BooleanType.FromObject(Interaction.IIf(e.get_Effect() == 2, true, false));
			if (this._dropData.TargetItem != null)
			{
				this.ProcessDroppedFiles(array, this._dropData.TargetItem, moveFiles);
				if (this.PhotosAddedEvent != null)
				{
					this.PhotosAddedEvent(this, new AlbumNameEventArgs(this._dropData.TargetItem.get_Text()));
				}
			}
			else
			{
				this.ProcessDroppedFilesRoot(array, moveFiles);
			}
		}
		private void menuAlbum_Popup(object sender, EventArgs e)
		{
			bool enabled = BooleanType.FromObject(Interaction.IIf(this.listView.get_SelectedItems().get_Count() > 0, true, false));
			this.menuDelete.set_Enabled(enabled);
			this.menuRename.set_Enabled(enabled);
			this.menuPublish.set_Enabled(enabled);
			if (this.SelectedItem == null)
			{
				this.menuPublish.set_Checked(false);
			}
			else
			{
				this.menuPublish.set_Checked(BooleanType.FromObject(Interaction.IIf(this.SelectedItem.get_ImageIndex() == 0, true, false)));
			}
		}
		private void menuImportPhotos_Click(object sender, EventArgs e)
		{
			this.ImportPhotos();
		}
		private void menuImportFolder_Click(object sender, EventArgs e)
		{
			this.ImportFolder();
		}
		private void menuNewAlbum_Click(object sender, EventArgs e)
		{
			this.CreateNewAlbum();
		}
		private void menuRename_Click(object sender, EventArgs e)
		{
			this.Rename();
		}
		private void menuDelete_Click(object sender, EventArgs e)
		{
			this.Delete();
		}
		private void menuPublish_Click(object sender, EventArgs e)
		{
			if (this.PublishAlbumClickedEvent != null)
			{
				this.PublishAlbumClickedEvent(this, new PublishAlbumEventArgs(this.SelectedAlbum, !this.menuPublish.get_Checked()));
			}
		}
	}
}
