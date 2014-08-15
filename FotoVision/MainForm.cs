using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms;
namespace FotoVision
{
	public class MainForm : Form
	{
		private enum ToolbarButtons
		{
			NewAlbum,
			ImportPhotos,
			Upload,
			Website,
			Email,
			Print,
			ManagePhotos,
			ManagePhotosDetails,
			PhotoShow,
			PhotoShowDetails,
			PhotoActions,
			Delete,
			RotateLeft,
			RotateRight,
			PreviousPhoto,
			NextPhoto
		}
		private enum Command
		{
			PrintPhoto,
			PreviousPhoto,
			NextPhoto,
			PhotoShow,
			PhotoActions,
			ThumbnailDetails,
			Save,
			Open,
			Rename,
			Delete,
			SelectAll,
			FullScreen,
			ClipboardCopy,
			ClipboardPaste,
			Rotate,
			Upload
		}
		private enum ChangeModeAction
		{
			Thumbnails,
			ThumbnailsDetails,
			Photo,
			PhotoDetails,
			PhotoActions,
			NewAlbumSelected
		}
		private enum Win32
		{
			WM_KEYDOWN = 256,
			WM_ACTIVATE = 6,
			WM_PAINT = 15,
			WM_NCPAINT = 133
		}
		[AccessedThroughProperty("menuAbout")]
		private MenuItem _menuAbout;
		[AccessedThroughProperty("paneDetails")]
		private DetailsPane _paneDetails;
		[AccessedThroughProperty("menuView")]
		private MenuItem _menuView;
		[AccessedThroughProperty("menuRedo")]
		private MenuItem _menuRedo;
		[AccessedThroughProperty("menuUndo")]
		private MenuItem _menuUndo;
		[AccessedThroughProperty("menuHelp")]
		private MenuItem _menuHelp;
		[AccessedThroughProperty("menuFlipHorz")]
		private MenuItem _menuFlipHorz;
		[AccessedThroughProperty("panePhotos")]
		private PhotosPane _panePhotos;
		[AccessedThroughProperty("menuFlipVert")]
		private MenuItem _menuFlipVert;
		[AccessedThroughProperty("menuItem7")]
		private MenuItem _menuItem7;
		[AccessedThroughProperty("menuNextPhoto")]
		private MenuItem _menuNextPhoto;
		[AccessedThroughProperty("panelPhotos")]
		private Panel _panelPhotos;
		[AccessedThroughProperty("menuRotate")]
		private MenuItem _menuRotate;
		[AccessedThroughProperty("toolBar")]
		private ToolBar _toolBar;
		[AccessedThroughProperty("splitterRight")]
		private Splitter _splitterRight;
		[AccessedThroughProperty("menuFile")]
		private MenuItem _menuFile;
		[AccessedThroughProperty("menuEdit")]
		private MenuItem _menuEdit;
		[AccessedThroughProperty("menuCopy")]
		private MenuItem _menuCopy;
		[AccessedThroughProperty("tbThumbnailsDetails")]
		private ToolBarButton _tbThumbnailsDetails;
		[AccessedThroughProperty("tbPhotoShowDetails")]
		private ToolBarButton _tbPhotoShowDetails;
		[AccessedThroughProperty("menuFullScreen")]
		private MenuItem _menuFullScreen;
		[AccessedThroughProperty("menuSave")]
		private MenuItem _menuSave;
		[AccessedThroughProperty("menuPaste")]
		private MenuItem _menuPaste;
		[AccessedThroughProperty("menuSep3")]
		private MenuItem _menuSep3;
		[AccessedThroughProperty("menuExit")]
		private MenuItem _menuExit;
		[AccessedThroughProperty("imageListStatusbar")]
		private ImageList _imageListStatusbar;
		[AccessedThroughProperty("menuPrint")]
		private MenuItem _menuPrint;
		[AccessedThroughProperty("tbImportPhotos")]
		private ToolBarButton _tbImportPhotos;
		[AccessedThroughProperty("menuEmailChanges")]
		private MenuItem _menuEmailChanges;
		[AccessedThroughProperty("tbSep3")]
		private ToolBarButton _tbSep3;
		[AccessedThroughProperty("tbSep4")]
		private ToolBarButton _tbSep4;
		[AccessedThroughProperty("menuDiscardChanges")]
		private MenuItem _menuDiscardChanges;
		[AccessedThroughProperty("tbSep5")]
		private ToolBarButton _tbSep5;
		[AccessedThroughProperty("_uploadForm")]
		private UploadForm __uploadForm;
		[AccessedThroughProperty("menuTools")]
		private MenuItem _menuTools;
		[AccessedThroughProperty("menuOptions")]
		private MenuItem _menuOptions;
		[AccessedThroughProperty("paneAlbums")]
		private AlbumsPane _paneAlbums;
		[AccessedThroughProperty("menuSelectAll")]
		private MenuItem _menuSelectAll;
		[AccessedThroughProperty("menuSep2")]
		private MenuItem _menuSep2;
		[AccessedThroughProperty("menuSep1")]
		private MenuItem _menuSep1;
		[AccessedThroughProperty("menuRotateRight")]
		private MenuItem _menuRotateRight;
		[AccessedThroughProperty("menuRotateLeft")]
		private MenuItem _menuRotateLeft;
		[AccessedThroughProperty("menuOpen")]
		private MenuItem _menuOpen;
		[AccessedThroughProperty("menuDelete")]
		private MenuItem _menuDelete;
		[AccessedThroughProperty("tbSep7")]
		private ToolBarButton _tbSep7;
		[AccessedThroughProperty("menuRename")]
		private MenuItem _menuRename;
		[AccessedThroughProperty("menuNewAlbum")]
		private MenuItem _menuNewAlbum;
		[AccessedThroughProperty("menuImportFolder")]
		private MenuItem _menuImportFolder;
		[AccessedThroughProperty("statusBar")]
		private StatusBar _statusBar;
		[AccessedThroughProperty("menuImportPhotos")]
		private MenuItem _menuImportPhotos;
		[AccessedThroughProperty("tbSep6")]
		private ToolBarButton _tbSep6;
		[AccessedThroughProperty("tbSep1")]
		private ToolBarButton _tbSep1;
		[AccessedThroughProperty("menuPrevPhoto")]
		private MenuItem _menuPrevPhoto;
		[AccessedThroughProperty("tbSep2")]
		private ToolBarButton _tbSep2;
		[AccessedThroughProperty("menuSep11")]
		private MenuItem _menuSep11;
		[AccessedThroughProperty("menuSep10")]
		private MenuItem _menuSep10;
		[AccessedThroughProperty("menuSep9")]
		private MenuItem _menuSep9;
		[AccessedThroughProperty("menuSep8")]
		private MenuItem _menuSep8;
		[AccessedThroughProperty("menuSep7")]
		private MenuItem _menuSep7;
		[AccessedThroughProperty("menuSep6")]
		private MenuItem _menuSep6;
		[AccessedThroughProperty("menuSep5")]
		private MenuItem _menuSep5;
		[AccessedThroughProperty("menuUploadAllChanges")]
		private MenuItem _menuUploadAllChanges;
		[AccessedThroughProperty("menuSep4")]
		private MenuItem _menuSep4;
		[AccessedThroughProperty("menuWebsite")]
		private MenuItem _menuWebsite;
		private string[] _orgStatusText;
		private int _curPhotoShowIndex;
		private bool _fullScreen;
		private bool _allowUpload;
		private bool _showThumbnailDetails;
		private StringFormat _format;
		private IContainer components;
		private MainMenu mainMenu;
		private Splitter splitterLeft;
		private StatusBarPanel statusPaneLeft;
		private StatusBarPanel statusPaneRight;
		private ProgressBar progressBar;
		private ImageList imageListToolbar;
		private ToolBarButton tbNewAlbum;
		private ToolBarButton tbRotateLeft;
		private ToolBarButton tbRotateRight;
		private ToolBarButton tbPrev;
		private ToolBarButton tbNext;
		private ToolBarButton tbThumbnails;
		private ToolBarButton tbDelete;
		private ToolBarButton tbUpload;
		private ToolBarButton tbEmail;
		private ToolBarButton tbWebsite;
		private ToolBarButton tbPhotoActions;
		private ToolBarButton tbPrint;
		private ToolBarButton tbPhotoShow;
		private UploadForm _uploadForm
		{
			get
			{
				return this.__uploadForm;
			}
			[MethodImpl(32)]
			set
			{
				if (this.__uploadForm != null)
				{
					this.__uploadForm.UploadComplete -= new UploadForm.UploadCompleteEventHandler(this.uploadForm_UploadComplete);
					this.__uploadForm.PublishComplete -= new UploadForm.PublishCompleteEventHandler(this.uploadForm_PublishComplete);
				}
				this.__uploadForm = value;
				if (this.__uploadForm != null)
				{
					this.__uploadForm.UploadComplete += new UploadForm.UploadCompleteEventHandler(this.uploadForm_UploadComplete);
					this.__uploadForm.PublishComplete += new UploadForm.PublishCompleteEventHandler(this.uploadForm_PublishComplete);
				}
			}
		}
		public bool FullScreen
		{
			get
			{
				return this._fullScreen;
			}
			set
			{
				this._fullScreen = value;
				this.DisplayFullScreen(this._fullScreen);
			}
		}
		private AlbumsPane paneAlbums
		{
			get
			{
				return this._paneAlbums;
			}
			[MethodImpl(32)]
			set
			{
				if (this._paneAlbums != null)
				{
					this._paneAlbums.PaneActive -= new BasePane.PaneActiveEventHandler(this.pane_PaneActive);
					this._paneAlbums.AlbumRenamed -= new AlbumsPane.AlbumRenamedEventHandler(this.paneAlbums_AlbumRenamed);
					this._paneAlbums.PhotosRemoved -= new AlbumsPane.PhotosRemovedEventHandler(this.paneAlbums_PhotosRemoved);
					this._paneAlbums.PhotosAdded -= new AlbumsPane.PhotosAddedEventHandler(this.paneAlbums_PhotosAdded);
					this._paneAlbums.NewAlbumCreated -= new AlbumsPane.NewAlbumCreatedEventHandler(this.paneAlbums_NewAlbumCreated);
					this._paneAlbums.PublishAlbumClicked -= new AlbumsPane.PublishAlbumClickedEventHandler(this.paneAlbums_PublishAlbumClicked);
					this._paneAlbums.SelectedAlbumClicked -= new AlbumsPane.SelectedAlbumClickedEventHandler(this.paneAlbums_SelectedAlbumClicked);
					this._paneAlbums.SelectedAlbumChanged -= new AlbumsPane.SelectedAlbumChangedEventHandler(this.paneAlbums_SelectedAlbumChanged);
				}
				this._paneAlbums = value;
				if (this._paneAlbums != null)
				{
					this._paneAlbums.PaneActive += new BasePane.PaneActiveEventHandler(this.pane_PaneActive);
					this._paneAlbums.AlbumRenamed += new AlbumsPane.AlbumRenamedEventHandler(this.paneAlbums_AlbumRenamed);
					this._paneAlbums.PhotosRemoved += new AlbumsPane.PhotosRemovedEventHandler(this.paneAlbums_PhotosRemoved);
					this._paneAlbums.PhotosAdded += new AlbumsPane.PhotosAddedEventHandler(this.paneAlbums_PhotosAdded);
					this._paneAlbums.NewAlbumCreated += new AlbumsPane.NewAlbumCreatedEventHandler(this.paneAlbums_NewAlbumCreated);
					this._paneAlbums.PublishAlbumClicked += new AlbumsPane.PublishAlbumClickedEventHandler(this.paneAlbums_PublishAlbumClicked);
					this._paneAlbums.SelectedAlbumClicked += new AlbumsPane.SelectedAlbumClickedEventHandler(this.paneAlbums_SelectedAlbumClicked);
					this._paneAlbums.SelectedAlbumChanged += new AlbumsPane.SelectedAlbumChangedEventHandler(this.paneAlbums_SelectedAlbumChanged);
				}
			}
		}
		private PhotosPane panePhotos
		{
			get
			{
				return this._panePhotos;
			}
			[MethodImpl(32)]
			set
			{
				if (this._panePhotos != null)
				{
					this._panePhotos.PaneActive -= new BasePane.PaneActiveEventHandler(this.pane_PaneActive);
					this._panePhotos.PhotoMetadataChanged -= new PhotosPane.PhotoMetadataChangedEventHandler(this.paneDetails_PhotoMetadataChanged);
					this._panePhotos.OpenPhoto -= new PhotosPane.OpenPhotoEventHandler(this.panePhotos_OpenPhoto);
					this._panePhotos.PhotosDeleted -= new PhotosPane.PhotosDeletedEventHandler(this.panePhotos_PhotosDeleted);
					this._panePhotos.PhotosMenuClicked -= new PhotosPane.PhotosMenuClickedEventHandler(this.panePhotos_PhotosMenuClicked);
					this._panePhotos.CropDataChanged -= new PhotosPane.CropDataChangedEventHandler(this.panePhotos_CropDataChanged);
					this._panePhotos.SelectionChanged -= new PhotosPane.SelectionChangedEventHandler(this.panePhotos_SelectionChanged);
					this._panePhotos.FilesDropped -= new PhotosPane.FilesDroppedEventHandler(this.panePhotos_FilesDropped);
				}
				this._panePhotos = value;
				if (this._panePhotos != null)
				{
					this._panePhotos.PaneActive += new BasePane.PaneActiveEventHandler(this.pane_PaneActive);
					this._panePhotos.PhotoMetadataChanged += new PhotosPane.PhotoMetadataChangedEventHandler(this.paneDetails_PhotoMetadataChanged);
					this._panePhotos.OpenPhoto += new PhotosPane.OpenPhotoEventHandler(this.panePhotos_OpenPhoto);
					this._panePhotos.PhotosDeleted += new PhotosPane.PhotosDeletedEventHandler(this.panePhotos_PhotosDeleted);
					this._panePhotos.PhotosMenuClicked += new PhotosPane.PhotosMenuClickedEventHandler(this.panePhotos_PhotosMenuClicked);
					this._panePhotos.CropDataChanged += new PhotosPane.CropDataChangedEventHandler(this.panePhotos_CropDataChanged);
					this._panePhotos.SelectionChanged += new PhotosPane.SelectionChangedEventHandler(this.panePhotos_SelectionChanged);
					this._panePhotos.FilesDropped += new PhotosPane.FilesDroppedEventHandler(this.panePhotos_FilesDropped);
				}
			}
		}
		private DetailsPane paneDetails
		{
			get
			{
				return this._paneDetails;
			}
			[MethodImpl(32)]
			set
			{
				if (this._paneDetails != null)
				{
					this._paneDetails.PaneActive -= new BasePane.PaneActiveEventHandler(this.pane_PaneActive);
					this._paneDetails.Action -= new DetailsPane.ActionEventHandler(this.paneDetails_Action);
					this._paneDetails.AlbumMetadataChanged -= new DetailsPane.AlbumMetadataChangedEventHandler(this.paneDetails_AlbumMetadataChanged);
					this._paneDetails.CommandButtonClicked -= new DetailsPane.CommandButtonClickedEventHandler(this.paneDetails_ActionCommand);
					this._paneDetails.CropModeChanged -= new DetailsPane.CropModeChangedEventHandler(this.paneDetails_CropModeChanged);
					this._paneDetails.PhotoMetadataChanged -= new DetailsPane.PhotoMetadataChangedEventHandler(this.paneDetails_PhotoMetadataChanged);
				}
				this._paneDetails = value;
				if (this._paneDetails != null)
				{
					this._paneDetails.PaneActive += new BasePane.PaneActiveEventHandler(this.pane_PaneActive);
					this._paneDetails.Action += new DetailsPane.ActionEventHandler(this.paneDetails_Action);
					this._paneDetails.AlbumMetadataChanged += new DetailsPane.AlbumMetadataChangedEventHandler(this.paneDetails_AlbumMetadataChanged);
					this._paneDetails.CommandButtonClicked += new DetailsPane.CommandButtonClickedEventHandler(this.paneDetails_ActionCommand);
					this._paneDetails.CropModeChanged += new DetailsPane.CropModeChangedEventHandler(this.paneDetails_CropModeChanged);
					this._paneDetails.PhotoMetadataChanged += new DetailsPane.PhotoMetadataChangedEventHandler(this.paneDetails_PhotoMetadataChanged);
				}
			}
		}
		private Panel panelPhotos
		{
			get
			{
				return this._panelPhotos;
			}
			[MethodImpl(32)]
			set
			{
				if (this._panelPhotos != null)
				{
				}
				this._panelPhotos = value;
				if (this._panelPhotos != null)
				{
				}
			}
		}
		private ToolBar toolBar
		{
			get
			{
				return this._toolBar;
			}
			[MethodImpl(32)]
			set
			{
				if (this._toolBar != null)
				{
					this._toolBar.remove_ButtonClick(new ToolBarButtonClickEventHandler(this.toolBar_ButtonClick));
				}
				this._toolBar = value;
				if (this._toolBar != null)
				{
					this._toolBar.add_ButtonClick(new ToolBarButtonClickEventHandler(this.toolBar_ButtonClick));
				}
			}
		}
		private Splitter splitterRight
		{
			get
			{
				return this._splitterRight;
			}
			[MethodImpl(32)]
			set
			{
				if (this._splitterRight != null)
				{
					this._splitterRight.remove_SplitterMoving(new SplitterEventHandler(this.splitterRight_SplitterMoving));
				}
				this._splitterRight = value;
				if (this._splitterRight != null)
				{
					this._splitterRight.add_SplitterMoving(new SplitterEventHandler(this.splitterRight_SplitterMoving));
				}
			}
		}
		private StatusBar statusBar
		{
			get
			{
				return this._statusBar;
			}
			[MethodImpl(32)]
			set
			{
				if (this._statusBar != null)
				{
					this._statusBar.remove_DrawItem(new StatusBarDrawItemEventHandler(this.statusBar_DrawItem));
				}
				this._statusBar = value;
				if (this._statusBar != null)
				{
					this._statusBar.add_DrawItem(new StatusBarDrawItemEventHandler(this.statusBar_DrawItem));
				}
			}
		}
		private MenuItem menuFile
		{
			get
			{
				return this._menuFile;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuFile != null)
				{
					this._menuFile.remove_Popup(new EventHandler(this.menuFile_Popup));
				}
				this._menuFile = value;
				if (this._menuFile != null)
				{
					this._menuFile.add_Popup(new EventHandler(this.menuFile_Popup));
				}
			}
		}
		private MenuItem menuEdit
		{
			get
			{
				return this._menuEdit;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuEdit != null)
				{
					this._menuEdit.remove_Popup(new EventHandler(this.menuEdit_Popup));
				}
				this._menuEdit = value;
				if (this._menuEdit != null)
				{
					this._menuEdit.add_Popup(new EventHandler(this.menuEdit_Popup));
				}
			}
		}
		private MenuItem menuNewAlbum
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
		private MenuItem menuImportPhotos
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
		private MenuItem menuImportFolder
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
		private MenuItem menuSave
		{
			get
			{
				return this._menuSave;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuSave != null)
				{
					this._menuSave.remove_Click(new EventHandler(this.menuSave_Click));
				}
				this._menuSave = value;
				if (this._menuSave != null)
				{
					this._menuSave.add_Click(new EventHandler(this.menuSave_Click));
				}
			}
		}
		private MenuItem menuDiscardChanges
		{
			get
			{
				return this._menuDiscardChanges;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuDiscardChanges != null)
				{
					this._menuDiscardChanges.remove_Click(new EventHandler(this.menuDiscardChanges_Click));
				}
				this._menuDiscardChanges = value;
				if (this._menuDiscardChanges != null)
				{
					this._menuDiscardChanges.add_Click(new EventHandler(this.menuDiscardChanges_Click));
				}
			}
		}
		private MenuItem menuUploadAllChanges
		{
			get
			{
				return this._menuUploadAllChanges;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuUploadAllChanges != null)
				{
					this._menuUploadAllChanges.remove_Click(new EventHandler(this.menuUploadAllChanges_Click));
				}
				this._menuUploadAllChanges = value;
				if (this._menuUploadAllChanges != null)
				{
					this._menuUploadAllChanges.add_Click(new EventHandler(this.menuUploadAllChanges_Click));
				}
			}
		}
		private MenuItem menuWebsite
		{
			get
			{
				return this._menuWebsite;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuWebsite != null)
				{
					this._menuWebsite.remove_Click(new EventHandler(this.menuWebsite_Click));
				}
				this._menuWebsite = value;
				if (this._menuWebsite != null)
				{
					this._menuWebsite.add_Click(new EventHandler(this.menuWebsite_Click));
				}
			}
		}
		private MenuItem menuEmailChanges
		{
			get
			{
				return this._menuEmailChanges;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuEmailChanges != null)
				{
					this._menuEmailChanges.remove_Click(new EventHandler(this.menuEmailChanges_Click));
				}
				this._menuEmailChanges = value;
				if (this._menuEmailChanges != null)
				{
					this._menuEmailChanges.add_Click(new EventHandler(this.menuEmailChanges_Click));
				}
			}
		}
		private MenuItem menuPrint
		{
			get
			{
				return this._menuPrint;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuPrint != null)
				{
					this._menuPrint.remove_Click(new EventHandler(this.menuPrint_Click));
				}
				this._menuPrint = value;
				if (this._menuPrint != null)
				{
					this._menuPrint.add_Click(new EventHandler(this.menuPrint_Click));
				}
			}
		}
		private MenuItem menuExit
		{
			get
			{
				return this._menuExit;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuExit != null)
				{
					this._menuExit.remove_Click(new EventHandler(this.menuExit_Click));
				}
				this._menuExit = value;
				if (this._menuExit != null)
				{
					this._menuExit.add_Click(new EventHandler(this.menuExit_Click));
				}
			}
		}
		private MenuItem menuCopy
		{
			get
			{
				return this._menuCopy;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuCopy != null)
				{
					this._menuCopy.remove_Click(new EventHandler(this.menuCopy_Click));
				}
				this._menuCopy = value;
				if (this._menuCopy != null)
				{
					this._menuCopy.add_Click(new EventHandler(this.menuCopy_Click));
				}
			}
		}
		private MenuItem menuPaste
		{
			get
			{
				return this._menuPaste;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuPaste != null)
				{
					this._menuPaste.remove_Click(new EventHandler(this.menuPaste_Click));
				}
				this._menuPaste = value;
				if (this._menuPaste != null)
				{
					this._menuPaste.add_Click(new EventHandler(this.menuPaste_Click));
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
					this._menuDelete.remove_Click(new EventHandler(this.menuDelete_Click));
				}
				this._menuDelete = value;
				if (this._menuDelete != null)
				{
					this._menuDelete.add_Click(new EventHandler(this.menuDelete_Click));
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
					this._menuRename.remove_Click(new EventHandler(this.menuRename_Click));
				}
				this._menuRename = value;
				if (this._menuRename != null)
				{
					this._menuRename.add_Click(new EventHandler(this.menuRename_Click));
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
					this._menuSelectAll.remove_Click(new EventHandler(this.menuSelectAll_Click));
				}
				this._menuSelectAll = value;
				if (this._menuSelectAll != null)
				{
					this._menuSelectAll.add_Click(new EventHandler(this.menuSelectAll_Click));
				}
			}
		}
		private MenuItem menuRotate
		{
			get
			{
				return this._menuRotate;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuRotate != null)
				{
				}
				this._menuRotate = value;
				if (this._menuRotate != null)
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
					this._menuRotateLeft.remove_Click(new EventHandler(this.menuRotateLeft_Click));
				}
				this._menuRotateLeft = value;
				if (this._menuRotateLeft != null)
				{
					this._menuRotateLeft.add_Click(new EventHandler(this.menuRotateLeft_Click));
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
					this._menuRotateRight.remove_Click(new EventHandler(this.menuRotateRight_Click));
				}
				this._menuRotateRight = value;
				if (this._menuRotateRight != null)
				{
					this._menuRotateRight.add_Click(new EventHandler(this.menuRotateRight_Click));
				}
			}
		}
		private MenuItem menuUndo
		{
			get
			{
				return this._menuUndo;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuUndo != null)
				{
					this._menuUndo.remove_Click(new EventHandler(this.menuUndo_Click));
				}
				this._menuUndo = value;
				if (this._menuUndo != null)
				{
					this._menuUndo.add_Click(new EventHandler(this.menuUndo_Click));
				}
			}
		}
		private MenuItem menuRedo
		{
			get
			{
				return this._menuRedo;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuRedo != null)
				{
					this._menuRedo.remove_Click(new EventHandler(this.menuRedo_Click));
				}
				this._menuRedo = value;
				if (this._menuRedo != null)
				{
					this._menuRedo.add_Click(new EventHandler(this.menuRedo_Click));
				}
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
					this._menuOpen.remove_Click(new EventHandler(this.menuOpen_Click));
				}
				this._menuOpen = value;
				if (this._menuOpen != null)
				{
					this._menuOpen.add_Click(new EventHandler(this.menuOpen_Click));
				}
			}
		}
		private MenuItem menuView
		{
			get
			{
				return this._menuView;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuView != null)
				{
					this._menuView.remove_Popup(new EventHandler(this.menuView_Popup));
				}
				this._menuView = value;
				if (this._menuView != null)
				{
					this._menuView.add_Popup(new EventHandler(this.menuView_Popup));
				}
			}
		}
		private MenuItem menuAbout
		{
			get
			{
				return this._menuAbout;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuAbout != null)
				{
					this._menuAbout.remove_Click(new EventHandler(this.menuAbout_Click));
				}
				this._menuAbout = value;
				if (this._menuAbout != null)
				{
					this._menuAbout.add_Click(new EventHandler(this.menuAbout_Click));
				}
			}
		}
		private MenuItem menuHelp
		{
			get
			{
				return this._menuHelp;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuHelp != null)
				{
				}
				this._menuHelp = value;
				if (this._menuHelp != null)
				{
				}
			}
		}
		private MenuItem menuFlipHorz
		{
			get
			{
				return this._menuFlipHorz;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuFlipHorz != null)
				{
					this._menuFlipHorz.remove_Click(new EventHandler(this.menuFlipHorz_Click));
				}
				this._menuFlipHorz = value;
				if (this._menuFlipHorz != null)
				{
					this._menuFlipHorz.add_Click(new EventHandler(this.menuFlipHorz_Click));
				}
			}
		}
		private MenuItem menuFlipVert
		{
			get
			{
				return this._menuFlipVert;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuFlipVert != null)
				{
					this._menuFlipVert.remove_Click(new EventHandler(this.menuFlipVert_Click));
				}
				this._menuFlipVert = value;
				if (this._menuFlipVert != null)
				{
					this._menuFlipVert.add_Click(new EventHandler(this.menuFlipVert_Click));
				}
			}
		}
		private MenuItem menuItem7
		{
			get
			{
				return this._menuItem7;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuItem7 != null)
				{
				}
				this._menuItem7 = value;
				if (this._menuItem7 != null)
				{
				}
			}
		}
		private ToolBarButton tbThumbnailsDetails
		{
			get
			{
				return this._tbThumbnailsDetails;
			}
			[MethodImpl(32)]
			set
			{
				if (this._tbThumbnailsDetails != null)
				{
				}
				this._tbThumbnailsDetails = value;
				if (this._tbThumbnailsDetails != null)
				{
				}
			}
		}
		private ToolBarButton tbPhotoShowDetails
		{
			get
			{
				return this._tbPhotoShowDetails;
			}
			[MethodImpl(32)]
			set
			{
				if (this._tbPhotoShowDetails != null)
				{
				}
				this._tbPhotoShowDetails = value;
				if (this._tbPhotoShowDetails != null)
				{
				}
			}
		}
		private ToolBarButton tbImportPhotos
		{
			get
			{
				return this._tbImportPhotos;
			}
			[MethodImpl(32)]
			set
			{
				if (this._tbImportPhotos != null)
				{
				}
				this._tbImportPhotos = value;
				if (this._tbImportPhotos != null)
				{
				}
			}
		}
		private MenuItem menuTools
		{
			get
			{
				return this._menuTools;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuTools != null)
				{
				}
				this._menuTools = value;
				if (this._menuTools != null)
				{
				}
			}
		}
		private MenuItem menuOptions
		{
			get
			{
				return this._menuOptions;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuOptions != null)
				{
					this._menuOptions.remove_Click(new EventHandler(this.menuOptions_Click));
				}
				this._menuOptions = value;
				if (this._menuOptions != null)
				{
					this._menuOptions.add_Click(new EventHandler(this.menuOptions_Click));
				}
			}
		}
		private MenuItem menuNextPhoto
		{
			get
			{
				return this._menuNextPhoto;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuNextPhoto != null)
				{
					this._menuNextPhoto.remove_Click(new EventHandler(this.menuNextPhoto_Click));
				}
				this._menuNextPhoto = value;
				if (this._menuNextPhoto != null)
				{
					this._menuNextPhoto.add_Click(new EventHandler(this.menuNextPhoto_Click));
				}
			}
		}
		private MenuItem menuPrevPhoto
		{
			get
			{
				return this._menuPrevPhoto;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuPrevPhoto != null)
				{
					this._menuPrevPhoto.remove_Click(new EventHandler(this.menuPrevPhoto_Click));
				}
				this._menuPrevPhoto = value;
				if (this._menuPrevPhoto != null)
				{
					this._menuPrevPhoto.add_Click(new EventHandler(this.menuPrevPhoto_Click));
				}
			}
		}
		private MenuItem menuFullScreen
		{
			get
			{
				return this._menuFullScreen;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuFullScreen != null)
				{
					this._menuFullScreen.remove_Click(new EventHandler(this.menuFullScreen_Click));
				}
				this._menuFullScreen = value;
				if (this._menuFullScreen != null)
				{
					this._menuFullScreen.add_Click(new EventHandler(this.menuFullScreen_Click));
				}
			}
		}
		private ImageList imageListStatusbar
		{
			get
			{
				return this._imageListStatusbar;
			}
			[MethodImpl(32)]
			set
			{
				if (this._imageListStatusbar != null)
				{
				}
				this._imageListStatusbar = value;
				if (this._imageListStatusbar != null)
				{
				}
			}
		}
		private ToolBarButton tbSep3
		{
			get
			{
				return this._tbSep3;
			}
			[MethodImpl(32)]
			set
			{
				if (this._tbSep3 != null)
				{
				}
				this._tbSep3 = value;
				if (this._tbSep3 != null)
				{
				}
			}
		}
		private ToolBarButton tbSep4
		{
			get
			{
				return this._tbSep4;
			}
			[MethodImpl(32)]
			set
			{
				if (this._tbSep4 != null)
				{
				}
				this._tbSep4 = value;
				if (this._tbSep4 != null)
				{
				}
			}
		}
		private ToolBarButton tbSep5
		{
			get
			{
				return this._tbSep5;
			}
			[MethodImpl(32)]
			set
			{
				if (this._tbSep5 != null)
				{
				}
				this._tbSep5 = value;
				if (this._tbSep5 != null)
				{
				}
			}
		}
		private ToolBarButton tbSep7
		{
			get
			{
				return this._tbSep7;
			}
			[MethodImpl(32)]
			set
			{
				if (this._tbSep7 != null)
				{
				}
				this._tbSep7 = value;
				if (this._tbSep7 != null)
				{
				}
			}
		}
		private ToolBarButton tbSep6
		{
			get
			{
				return this._tbSep6;
			}
			[MethodImpl(32)]
			set
			{
				if (this._tbSep6 != null)
				{
				}
				this._tbSep6 = value;
				if (this._tbSep6 != null)
				{
				}
			}
		}
		private ToolBarButton tbSep1
		{
			get
			{
				return this._tbSep1;
			}
			[MethodImpl(32)]
			set
			{
				if (this._tbSep1 != null)
				{
				}
				this._tbSep1 = value;
				if (this._tbSep1 != null)
				{
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
		private MenuItem menuSep4
		{
			get
			{
				return this._menuSep4;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuSep4 != null)
				{
				}
				this._menuSep4 = value;
				if (this._menuSep4 != null)
				{
				}
			}
		}
		private MenuItem menuSep5
		{
			get
			{
				return this._menuSep5;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuSep5 != null)
				{
				}
				this._menuSep5 = value;
				if (this._menuSep5 != null)
				{
				}
			}
		}
		private MenuItem menuSep6
		{
			get
			{
				return this._menuSep6;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuSep6 != null)
				{
				}
				this._menuSep6 = value;
				if (this._menuSep6 != null)
				{
				}
			}
		}
		private MenuItem menuSep7
		{
			get
			{
				return this._menuSep7;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuSep7 != null)
				{
				}
				this._menuSep7 = value;
				if (this._menuSep7 != null)
				{
				}
			}
		}
		private MenuItem menuSep8
		{
			get
			{
				return this._menuSep8;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuSep8 != null)
				{
				}
				this._menuSep8 = value;
				if (this._menuSep8 != null)
				{
				}
			}
		}
		private MenuItem menuSep9
		{
			get
			{
				return this._menuSep9;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuSep9 != null)
				{
				}
				this._menuSep9 = value;
				if (this._menuSep9 != null)
				{
				}
			}
		}
		private MenuItem menuSep10
		{
			get
			{
				return this._menuSep10;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuSep10 != null)
				{
				}
				this._menuSep10 = value;
				if (this._menuSep10 != null)
				{
				}
			}
		}
		private MenuItem menuSep11
		{
			get
			{
				return this._menuSep11;
			}
			[MethodImpl(32)]
			set
			{
				if (this._menuSep11 != null)
				{
				}
				this._menuSep11 = value;
				if (this._menuSep11 != null)
				{
				}
			}
		}
		private ToolBarButton tbSep2
		{
			get
			{
				return this._tbSep2;
			}
			[MethodImpl(32)]
			set
			{
				if (this._tbSep2 != null)
				{
				}
				this._tbSep2 = value;
				if (this._tbSep2 != null)
				{
				}
			}
		}
		[STAThread]
		public static void Main()
		{
			bool flag;
			Mutex mutex = new Mutex(true, Application.get_ProductName() + "manifest", ref flag);
			if (flag)
			{
				if (ThemeManifest.Create())
				{
					Process process = Process.Start(Application.get_ExecutablePath());
					process.WaitForInputIdle();
					Application.Exit();
				}
				else
				{
					MainForm.Run();
				}
				mutex.ReleaseMutex();
			}
			else
			{
				MainForm.Run();
			}
		}
		public static void Run()
		{
			bool flag;
			Mutex mutex = new Mutex(true, Application.get_ProductName() + "single", ref flag);
			if (flag)
			{
				Application.Run(new MainForm());
				mutex.ReleaseMutex();
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
			this.components = new Container();
			ResourceManager resourceManager = new ResourceManager(typeof(MainForm));
			this.paneAlbums = new AlbumsPane();
			this.splitterLeft = new Splitter();
			this.panelPhotos = new Panel();
			this.panePhotos = new PhotosPane();
			this.splitterRight = new Splitter();
			this.paneDetails = new DetailsPane();
			this.statusBar = new StatusBar();
			this.statusPaneLeft = new StatusBarPanel();
			this.statusPaneRight = new StatusBarPanel();
			this.toolBar = new ToolBar();
			this.tbNewAlbum = new ToolBarButton();
			this.tbImportPhotos = new ToolBarButton();
			this.tbSep3 = new ToolBarButton();
			this.tbUpload = new ToolBarButton();
			this.tbWebsite = new ToolBarButton();
			this.tbSep4 = new ToolBarButton();
			this.tbEmail = new ToolBarButton();
			this.tbPrint = new ToolBarButton();
			this.tbSep2 = new ToolBarButton();
			this.tbThumbnails = new ToolBarButton();
			this.tbThumbnailsDetails = new ToolBarButton();
			this.tbPhotoShow = new ToolBarButton();
			this.tbPhotoShowDetails = new ToolBarButton();
			this.tbSep5 = new ToolBarButton();
			this.tbPhotoActions = new ToolBarButton();
			this.tbSep7 = new ToolBarButton();
			this.tbDelete = new ToolBarButton();
			this.tbSep6 = new ToolBarButton();
			this.tbRotateLeft = new ToolBarButton();
			this.tbRotateRight = new ToolBarButton();
			this.tbSep1 = new ToolBarButton();
			this.tbPrev = new ToolBarButton();
			this.tbNext = new ToolBarButton();
			this.imageListToolbar = new ImageList(this.components);
			this.mainMenu = new MainMenu();
			this.menuFile = new MenuItem();
			this.menuNewAlbum = new MenuItem();
			this.menuSep1 = new MenuItem();
			this.menuImportPhotos = new MenuItem();
			this.menuImportFolder = new MenuItem();
			this.menuSep2 = new MenuItem();
			this.menuOpen = new MenuItem();
			this.menuRename = new MenuItem();
			this.menuDelete = new MenuItem();
			this.menuSep3 = new MenuItem();
			this.menuSave = new MenuItem();
			this.menuSep4 = new MenuItem();
			this.menuUploadAllChanges = new MenuItem();
			this.menuWebsite = new MenuItem();
			this.menuSep5 = new MenuItem();
			this.menuEmailChanges = new MenuItem();
			this.menuSep6 = new MenuItem();
			this.menuPrint = new MenuItem();
			this.menuSep7 = new MenuItem();
			this.menuExit = new MenuItem();
			this.menuEdit = new MenuItem();
			this.menuUndo = new MenuItem();
			this.menuRedo = new MenuItem();
			this.menuDiscardChanges = new MenuItem();
			this.menuSep8 = new MenuItem();
			this.menuRotate = new MenuItem();
			this.menuRotateLeft = new MenuItem();
			this.menuRotateRight = new MenuItem();
			this.menuItem7 = new MenuItem();
			this.menuFlipHorz = new MenuItem();
			this.menuFlipVert = new MenuItem();
			this.menuSep9 = new MenuItem();
			this.menuCopy = new MenuItem();
			this.menuPaste = new MenuItem();
			this.menuSep10 = new MenuItem();
			this.menuSelectAll = new MenuItem();
			this.menuView = new MenuItem();
			this.menuNextPhoto = new MenuItem();
			this.menuPrevPhoto = new MenuItem();
			this.menuSep11 = new MenuItem();
			this.menuFullScreen = new MenuItem();
			this.menuTools = new MenuItem();
			this.menuOptions = new MenuItem();
			this.menuHelp = new MenuItem();
			this.menuAbout = new MenuItem();
			this.progressBar = new ProgressBar();
			this.imageListStatusbar = new ImageList(this.components);
			this.panelPhotos.SuspendLayout();
			this.statusPaneLeft.BeginInit();
			this.statusPaneRight.BeginInit();
			this.SuspendLayout();
			this.paneAlbums.CaptionText = "My Albums";
			this.paneAlbums.set_Dock(3);
			this.paneAlbums.get_DockPadding().set_All(2);
			Control arg_413_0 = this.paneAlbums;
			Point location = new Point(0, 39);
			arg_413_0.set_Location(location);
			this.paneAlbums.set_Name("paneAlbums");
			this.paneAlbums.SelectedAlbum = "";
			Control arg_451_0 = this.paneAlbums;
			Size size = new Size(184, 434);
			arg_451_0.set_Size(size);
			this.paneAlbums.set_TabIndex(0);
			this.splitterLeft.set_BorderStyle(2);
			Control arg_484_0 = this.splitterLeft;
			location = new Point(184, 39);
			arg_484_0.set_Location(location);
			this.splitterLeft.set_Name("splitterLeft");
			Control arg_4AE_0 = this.splitterLeft;
			size = new Size(3, 434);
			arg_4AE_0.set_Size(size);
			this.splitterLeft.set_TabIndex(1);
			this.splitterLeft.set_TabStop(false);
			this.panelPhotos.get_Controls().Add(this.panePhotos);
			this.panelPhotos.get_Controls().Add(this.splitterRight);
			this.panelPhotos.get_Controls().Add(this.paneDetails);
			this.panelPhotos.set_Dock(5);
			Control arg_52F_0 = this.panelPhotos;
			location = new Point(187, 39);
			arg_52F_0.set_Location(location);
			this.panelPhotos.set_Name("panelPhotos");
			Control arg_55D_0 = this.panelPhotos;
			size = new Size(555, 434);
			arg_55D_0.set_Size(size);
			this.panelPhotos.set_TabIndex(2);
			this.panePhotos.set_BackColor(SystemColors.get_Control());
			this.panePhotos.CaptionText = "Manage Photos";
			this.panePhotos.CropMode = false;
			this.panePhotos.set_Dock(5);
			this.panePhotos.get_DockPadding().set_All(2);
			this.panePhotos.FullScreen = false;
			Control arg_5D4_0 = this.panePhotos;
			location = new Point(0, 0);
			arg_5D4_0.set_Location(location);
			this.panePhotos.Mode = PhotosMode.Thumbnails;
			this.panePhotos.set_Name("panePhotos");
			Control arg_60E_0 = this.panePhotos;
			size = new Size(240, 434);
			arg_60E_0.set_Size(size);
			this.panePhotos.set_TabIndex(0);
			this.splitterRight.set_BorderStyle(2);
			this.splitterRight.set_Dock(4);
			Control arg_64C_0 = this.splitterRight;
			location = new Point(240, 0);
			arg_64C_0.set_Location(location);
			this.splitterRight.set_Name("splitterRight");
			Control arg_676_0 = this.splitterRight;
			size = new Size(3, 434);
			arg_676_0.set_Size(size);
			this.splitterRight.set_TabIndex(1);
			this.splitterRight.set_TabStop(false);
			this.paneDetails.AlbumName = null;
			this.paneDetails.CaptionText = "Album Description";
			this.paneDetails.set_Dock(4);
			this.paneDetails.get_DockPadding().set_All(2);
			Control arg_6E1_0 = this.paneDetails;
			location = new Point(243, 0);
			arg_6E1_0.set_Location(location);
			this.paneDetails.Mode = DetailsMode.AlbumDetails;
			this.paneDetails.set_Name("paneDetails");
			Control arg_71B_0 = this.paneDetails;
			size = new Size(312, 434);
			arg_71B_0.set_Size(size);
			this.paneDetails.set_TabIndex(2);
			Control arg_741_0 = this.statusBar;
			location = new Point(0, 473);
			arg_741_0.set_Location(location);
			this.statusBar.set_Name("statusBar");
			this.statusBar.get_Panels().AddRange(new StatusBarPanel[]
			{
				this.statusPaneLeft,
				this.statusPaneRight
			});
			this.statusBar.set_ShowPanels(true);
			Control arg_7A2_0 = this.statusBar;
			size = new Size(742, 22);
			arg_7A2_0.set_Size(size);
			this.statusBar.set_TabIndex(3);
			this.statusPaneLeft.set_Style(2);
			this.statusPaneLeft.set_Width(150);
			this.statusPaneRight.set_AutoSize(2);
			this.statusPaneRight.set_Style(2);
			this.statusPaneRight.set_Width(576);
			this.toolBar.set_Appearance(1);
			this.toolBar.get_Buttons().AddRange(new ToolBarButton[]
			{
				this.tbNewAlbum,
				this.tbImportPhotos,
				this.tbSep3,
				this.tbUpload,
				this.tbWebsite,
				this.tbSep4,
				this.tbEmail,
				this.tbPrint,
				this.tbSep2,
				this.tbThumbnails,
				this.tbThumbnailsDetails,
				this.tbPhotoShow,
				this.tbPhotoShowDetails,
				this.tbSep5,
				this.tbPhotoActions,
				this.tbSep7,
				this.tbDelete,
				this.tbSep6,
				this.tbRotateLeft,
				this.tbRotateRight,
				this.tbSep1,
				this.tbPrev,
				this.tbNext
			});
			this.toolBar.set_DropDownArrows(true);
			this.toolBar.set_ImageList(this.imageListToolbar);
			Control arg_927_0 = this.toolBar;
			location = new Point(0, 0);
			arg_927_0.set_Location(location);
			this.toolBar.set_Name("toolBar");
			this.toolBar.set_ShowToolTips(true);
			Control arg_95E_0 = this.toolBar;
			size = new Size(742, 39);
			arg_95E_0.set_Size(size);
			this.toolBar.set_TabIndex(4);
			this.toolBar.set_TextAlign(1);
			this.toolBar.set_Wrappable(false);
			this.tbNewAlbum.set_ImageIndex(5);
			this.tbNewAlbum.set_ToolTipText("New Album");
			this.tbImportPhotos.set_ImageIndex(16);
			this.tbImportPhotos.set_ToolTipText("Import Photos");
			this.tbSep3.set_Style(3);
			this.tbUpload.set_ImageIndex(3);
			this.tbUpload.set_Text("Upload");
			this.tbUpload.set_ToolTipText("Upload Albums");
			this.tbWebsite.set_ImageIndex(14);
			this.tbWebsite.set_Text("My Site");
			this.tbWebsite.set_ToolTipText("View Website");
			this.tbSep4.set_Style(3);
			this.tbEmail.set_ImageIndex(13);
			this.tbEmail.set_ToolTipText("Email");
			this.tbPrint.set_ImageIndex(10);
			this.tbPrint.set_ToolTipText("Print");
			this.tbSep2.set_Style(3);
			this.tbThumbnails.set_ImageIndex(4);
			this.tbThumbnails.set_ToolTipText("Manage Photos");
			this.tbThumbnailsDetails.set_ImageIndex(1);
			this.tbThumbnailsDetails.set_ToolTipText("Manage Photos with Descriptions");
			this.tbPhotoShow.set_ImageIndex(8);
			this.tbPhotoShow.set_ToolTipText("Photo Show");
			this.tbPhotoShowDetails.set_ImageIndex(15);
			this.tbPhotoShowDetails.set_ToolTipText("Photo Show with Descriptions");
			this.tbSep5.set_Style(3);
			this.tbPhotoActions.set_ImageIndex(7);
			this.tbPhotoActions.set_ToolTipText("Photo Actions");
			this.tbSep7.set_Style(3);
			this.tbDelete.set_ImageIndex(0);
			this.tbDelete.set_ToolTipText("Delete");
			this.tbSep6.set_Style(3);
			this.tbRotateLeft.set_ImageIndex(11);
			this.tbRotateLeft.set_ToolTipText("Rotate Left");
			this.tbRotateRight.set_ImageIndex(12);
			this.tbRotateRight.set_ToolTipText("Rotate Right");
			this.tbSep1.set_Style(3);
			this.tbPrev.set_ImageIndex(9);
			this.tbPrev.set_ToolTipText("Previous Photo");
			this.tbNext.set_ImageIndex(6);
			this.tbNext.set_ToolTipText("Next Photo");
			ImageList arg_BD6_0 = this.imageListToolbar;
			size = new Size(27, 27);
			arg_BD6_0.set_ImageSize(size);
			this.imageListToolbar.set_ImageStream((ImageListStreamer)resourceManager.GetObject("imageListToolbar.ImageStream"));
			this.imageListToolbar.set_TransparentColor(Color.get_Lime());
			this.mainMenu.get_MenuItems().AddRange(new MenuItem[]
			{
				this.menuFile,
				this.menuEdit,
				this.menuView,
				this.menuTools,
				this.menuHelp
			});
			this.menuFile.set_Index(0);
			this.menuFile.get_MenuItems().AddRange(new MenuItem[]
			{
				this.menuNewAlbum,
				this.menuSep1,
				this.menuImportPhotos,
				this.menuImportFolder,
				this.menuSep2,
				this.menuOpen,
				this.menuRename,
				this.menuDelete,
				this.menuSep3,
				this.menuSave,
				this.menuSep4,
				this.menuUploadAllChanges,
				this.menuWebsite,
				this.menuSep5,
				this.menuEmailChanges,
				this.menuSep6,
				this.menuPrint,
				this.menuSep7,
				this.menuExit
			});
			this.menuFile.set_Text("&File");
			this.menuNewAlbum.set_Index(0);
			this.menuNewAlbum.set_Text("&New Album");
			this.menuSep1.set_Index(1);
			this.menuSep1.set_Text("-");
			this.menuImportPhotos.set_Index(2);
			this.menuImportPhotos.set_Text("&Import Photos...");
			this.menuImportFolder.set_Index(3);
			this.menuImportFolder.set_Text("Import &Folder...");
			this.menuSep2.set_Index(4);
			this.menuSep2.set_Text("-");
			this.menuOpen.set_Enabled(false);
			this.menuOpen.set_Index(5);
			this.menuOpen.set_Shortcut(131151);
			this.menuOpen.set_Text("&Open");
			this.menuRename.set_Enabled(false);
			this.menuRename.set_Index(6);
			this.menuRename.set_Text("&Rename");
			this.menuDelete.set_Enabled(false);
			this.menuDelete.set_Index(7);
			this.menuDelete.set_Text("&Delete");
			this.menuSep3.set_Index(8);
			this.menuSep3.set_Text("-");
			this.menuSave.set_Enabled(false);
			this.menuSave.set_Index(9);
			this.menuSave.set_Shortcut(131155);
			this.menuSave.set_Text("&Save");
			this.menuSep4.set_Index(10);
			this.menuSep4.set_Text("-");
			this.menuUploadAllChanges.set_Enabled(false);
			this.menuUploadAllChanges.set_Index(11);
			this.menuUploadAllChanges.set_Text("&Upload All Changes");
			this.menuWebsite.set_Index(12);
			this.menuWebsite.set_Text("Open &Website");
			this.menuSep5.set_Index(13);
			this.menuSep5.set_Text("-");
			this.menuEmailChanges.set_Index(14);
			this.menuEmailChanges.set_Text("&Email Changes to Friends");
			this.menuSep6.set_Index(15);
			this.menuSep6.set_Text("-");
			this.menuPrint.set_Enabled(false);
			this.menuPrint.set_Index(16);
			this.menuPrint.set_Shortcut(131152);
			this.menuPrint.set_Text("&Print...");
			this.menuSep7.set_Index(17);
			this.menuSep7.set_Text("-");
			this.menuExit.set_Index(18);
			this.menuExit.set_Text("E&xit");
			this.menuEdit.set_Index(1);
			this.menuEdit.get_MenuItems().AddRange(new MenuItem[]
			{
				this.menuUndo,
				this.menuRedo,
				this.menuDiscardChanges,
				this.menuSep8,
				this.menuRotate,
				this.menuSep9,
				this.menuCopy,
				this.menuPaste,
				this.menuSep10,
				this.menuSelectAll
			});
			this.menuEdit.set_Text("&Edit");
			this.menuUndo.set_Enabled(false);
			this.menuUndo.set_Index(0);
			this.menuUndo.set_Shortcut(131162);
			this.menuUndo.set_Text("&Undo");
			this.menuRedo.set_Enabled(false);
			this.menuRedo.set_Index(1);
			this.menuRedo.set_Shortcut(131161);
			this.menuRedo.set_Text("&Redo");
			this.menuDiscardChanges.set_Enabled(false);
			this.menuDiscardChanges.set_Index(2);
			this.menuDiscardChanges.set_Text("R&eset Photo");
			this.menuSep8.set_Index(3);
			this.menuSep8.set_Text("-");
			this.menuRotate.set_Enabled(false);
			this.menuRotate.set_Index(4);
			this.menuRotate.get_MenuItems().AddRange(new MenuItem[]
			{
				this.menuRotateLeft,
				this.menuRotateRight,
				this.menuItem7,
				this.menuFlipHorz,
				this.menuFlipVert
			});
			this.menuRotate.set_Text("R&otate Photo");
			this.menuRotateLeft.set_Index(0);
			this.menuRotateLeft.set_Text("Rotate &Left");
			this.menuRotateRight.set_Index(1);
			this.menuRotateRight.set_Text("Rotate &Right");
			this.menuItem7.set_Index(2);
			this.menuItem7.set_Text("-");
			this.menuFlipHorz.set_Index(3);
			this.menuFlipHorz.set_Text("Flip &Horizontal");
			this.menuFlipVert.set_Index(4);
			this.menuFlipVert.set_Text("Flip &Vertical");
			this.menuSep9.set_Index(5);
			this.menuSep9.set_Text("-");
			this.menuCopy.set_Enabled(false);
			this.menuCopy.set_Index(6);
			this.menuCopy.set_Shortcut(131139);
			this.menuCopy.set_Text("&Copy");
			this.menuPaste.set_Enabled(false);
			this.menuPaste.set_Index(7);
			this.menuPaste.set_Shortcut(131158);
			this.menuPaste.set_Text("&Paste");
			this.menuSep10.set_Index(8);
			this.menuSep10.set_Text("-");
			this.menuSelectAll.set_Enabled(false);
			this.menuSelectAll.set_Index(9);
			this.menuSelectAll.set_Shortcut(131137);
			this.menuSelectAll.set_Text("&Select All");
			this.menuView.set_Index(2);
			this.menuView.get_MenuItems().AddRange(new MenuItem[]
			{
				this.menuNextPhoto,
				this.menuPrevPhoto,
				this.menuSep11,
				this.menuFullScreen
			});
			this.menuView.set_Text("&View");
			this.menuNextPhoto.set_Index(0);
			this.menuNextPhoto.set_Text("&Next Photo");
			this.menuPrevPhoto.set_Index(1);
			this.menuPrevPhoto.set_Text("&Previous Photo");
			this.menuSep11.set_Index(2);
			this.menuSep11.set_Text("-");
			this.menuFullScreen.set_Index(3);
			this.menuFullScreen.set_Shortcut(131142);
			this.menuFullScreen.set_Text("&Full Screen");
			this.menuTools.set_Index(3);
			this.menuTools.get_MenuItems().AddRange(new MenuItem[]
			{
				this.menuOptions
			});
			this.menuTools.set_Text("&Tools");
			this.menuOptions.set_Index(0);
			this.menuOptions.set_Text("&Options...");
			this.menuHelp.set_Index(4);
			this.menuHelp.get_MenuItems().AddRange(new MenuItem[]
			{
				this.menuAbout
			});
			this.menuHelp.set_Text("&Help");
			this.menuAbout.set_Index(0);
			this.menuAbout.set_Text("&About FotoVision...");
			this.progressBar.set_Anchor(14);
			Control arg_1499_0 = this.progressBar;
			location = new Point(156, 478);
			arg_1499_0.set_Location(location);
			this.progressBar.set_Name("progressBar");
			Control arg_14C4_0 = this.progressBar;
			size = new Size(566, 14);
			arg_14C4_0.set_Size(size);
			this.progressBar.set_TabIndex(5);
			this.progressBar.set_Visible(false);
			ImageList arg_14F4_0 = this.imageListStatusbar;
			size = new Size(9, 14);
			arg_14F4_0.set_ImageSize(size);
			this.imageListStatusbar.set_ImageStream((ImageListStreamer)resourceManager.GetObject("imageListStatusbar.ImageStream"));
			this.imageListStatusbar.set_TransparentColor(Color.get_Lime());
			size = new Size(5, 13);
			this.set_AutoScaleBaseSize(size);
			size = new Size(742, 495);
			this.set_ClientSize(size);
			this.get_Controls().Add(this.progressBar);
			this.get_Controls().Add(this.panelPhotos);
			this.get_Controls().Add(this.splitterLeft);
			this.get_Controls().Add(this.paneAlbums);
			this.get_Controls().Add(this.toolBar);
			this.get_Controls().Add(this.statusBar);
			this.set_Icon((Icon)resourceManager.GetObject("$this.Icon"));
			this.set_Menu(this.mainMenu);
			size = new Size(320, 320);
			this.set_MinimumSize(size);
			this.set_Name("MainForm");
			this.set_Text("FotoVision");
			this.panelPhotos.ResumeLayout(false);
			this.statusPaneLeft.EndInit();
			this.statusPaneRight.EndInit();
			this.ResumeLayout(false);
		}
		public MainForm()
		{
			this._orgStatusText = new string[2];
			this._allowUpload = true;
			this._showThumbnailDetails = true;
			this.InitializeComponent();
			Global.Progress.ProgressComplete += new Progress.ProgressCompleteEventHandler(this.ProgressComplete);
			Global.Progress.ProgressUpdate += new Progress.ProgressUpdateEventHandler(this.ProgressUpdate);
			this._format = new StringFormat();
			this._format.set_FormatFlags(4096);
			this._format.set_LineAlignment(1);
			this._format.set_Trimming(3);
			this.InitToolbarButtons();
		}
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.RestoreWindowSettings();
			string @string = Global.Settings.GetString(SettingKey.LastAlbum);
			if (StringType.StrCmp(@string, "", false) == 0)
			{
				this.paneAlbums.SelectFirstAlbum();
			}
			else
			{
				this.paneAlbums.SelectedAlbum = @string;
			}
			this.UpdateToolbar();
		}
		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
			this.ClosingCurrentPhoto(false);
			this.paneDetails.Save();
			this.SaveSettings();
			if (this._uploadForm != null)
			{
				this._uploadForm.Abort();
			}
		}
		private void InitToolbarButtons()
		{
			int arg_14_0 = 0;
			checked
			{
				int num = this.toolBar.get_Buttons().get_Count() - 1;
				for (int i = arg_14_0; i <= num; i++)
				{
					if (this.toolBar.get_Buttons().get_Item(i).get_Style() != 3)
					{
						int num2;
						this.toolBar.get_Buttons().get_Item(i).set_Tag(num2);
						num2++;
					}
				}
			}
		}
		private void toolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
		{
			switch ((MainForm.ToolbarButtons)e.get_Button().get_Tag())
			{
			case MainForm.ToolbarButtons.NewAlbum:
				this.menuNewAlbum_Click(this, EventArgs.Empty);
				break;
			case MainForm.ToolbarButtons.ImportPhotos:
				this.menuImportPhotos_Click(this, EventArgs.Empty);
				break;
			case MainForm.ToolbarButtons.Upload:
				this.menuUploadAllChanges_Click(this, EventArgs.Empty);
				break;
			case MainForm.ToolbarButtons.Website:
				this.menuWebsite_Click(this, EventArgs.Empty);
				break;
			case MainForm.ToolbarButtons.Email:
				this.menuEmailChanges_Click(this, EventArgs.Empty);
				break;
			case MainForm.ToolbarButtons.Print:
				this.menuPrint_Click(this, EventArgs.Empty);
				break;
			case MainForm.ToolbarButtons.ManagePhotos:
				if (!this.tbThumbnails.get_Pushed())
				{
					this.ChangeMode(MainForm.ChangeModeAction.Thumbnails);
				}
				break;
			case MainForm.ToolbarButtons.ManagePhotosDetails:
				if (!this.tbThumbnailsDetails.get_Pushed())
				{
					this.ChangeMode(MainForm.ChangeModeAction.ThumbnailsDetails);
				}
				break;
			case MainForm.ToolbarButtons.PhotoShow:
				if (!this.tbPhotoShow.get_Pushed())
				{
					this.ChangeMode(MainForm.ChangeModeAction.Photo);
				}
				break;
			case MainForm.ToolbarButtons.PhotoShowDetails:
				if (!this.tbPhotoShowDetails.get_Pushed())
				{
					this.ChangeMode(MainForm.ChangeModeAction.PhotoDetails);
				}
				break;
			case MainForm.ToolbarButtons.PhotoActions:
				if (!this.tbPhotoActions.get_Pushed())
				{
					this.ChangeMode(MainForm.ChangeModeAction.PhotoActions);
				}
				break;
			case MainForm.ToolbarButtons.Delete:
				this.menuDelete_Click(this, EventArgs.Empty);
				break;
			case MainForm.ToolbarButtons.RotateLeft:
				this.paneDetails_Action(this, new ActionEventArgs(PhotoAction.RotateLeft));
				break;
			case MainForm.ToolbarButtons.RotateRight:
				this.paneDetails_Action(this, new ActionEventArgs(PhotoAction.RotateRight));
				break;
			case MainForm.ToolbarButtons.PreviousPhoto:
				this.menuPrevPhoto_Click(this, EventArgs.Empty);
				break;
			case MainForm.ToolbarButtons.NextPhoto:
				this.menuNextPhoto_Click(this, EventArgs.Empty);
				break;
			}
		}
		private void pane_PaneActive(object sender, EventArgs e)
		{
			this.UpdateToolbar();
		}
		private void ProgressUpdate(object sender, ProgressUpdateEventArgs e)
		{
			this.set_Cursor(Cursors.get_WaitCursor());
			if (this.FullScreen)
			{
				return;
			}
			string text = e.Message + "...";
			if (!this.progressBar.get_Visible())
			{
				this._orgStatusText[0] = this.statusBar.get_Panels().get_Item(0).get_Text();
				this._orgStatusText[1] = this.statusBar.get_Panels().get_Item(1).get_Text();
				this.statusBar.get_Panels().get_Item(1).set_Text("");
				this.statusBar.get_Panels().get_Item(1).set_BorderStyle(1);
			}
			this.statusBar.get_Panels().get_Item(0).set_Text(text);
			this.progressBar.set_Maximum(e.Total);
			this.progressBar.set_Value(e.Position);
			this.progressBar.Show();
			this.progressBar.Refresh();
			this.statusBar.Refresh();
			Global.Busy = true;
			this.set_Capture(true);
			Application.DoEvents();
			this.set_Capture(false);
			Global.Busy = false;
		}
		private void ProgressComplete(object sender, EventArgs e)
		{
			this.set_Cursor(Cursors.get_Default());
			if (this.progressBar.get_Visible())
			{
				this.progressBar.Hide();
				this.statusBar.get_Panels().get_Item(0).set_Text(this._orgStatusText[0]);
				this.statusBar.get_Panels().get_Item(1).set_Text(this._orgStatusText[1]);
				this.statusBar.get_Panels().get_Item(1).set_BorderStyle(3);
			}
		}
		private void UpdateDetailsMode()
		{
			if (this.panePhotos.Mode != PhotosMode.Thumbnails)
			{
				return;
			}
			if (this.paneDetails.Mode != DetailsMode.AlbumDetails & this.paneDetails.Mode != DetailsMode.PhotoDetails)
			{
				return;
			}
			if (this.paneDetails.Mode == DetailsMode.AlbumDetails & this.panePhotos.SelectedCount > 0)
			{
				this.paneDetails.Mode = DetailsMode.PhotoDetails;
			}
			if (this.paneDetails.Mode == DetailsMode.PhotoDetails & this.panePhotos.SelectedCount == 0)
			{
				this.paneDetails.Mode = DetailsMode.AlbumDetails;
			}
		}
		private void UpdateDetailCommandButtons()
		{
			this.paneDetails.EnableCommandButtons(this.panePhotos.PhotoDirty);
		}
		private void OpenPhoto(int index)
		{
			try
			{
				this.ClosingCurrentPhoto(false);
				this.panePhotos.OpenPhotoAtIndex(index);
			}
			catch (Exception expr_15)
			{
				ProjectData.SetProjectError(expr_15);
				Exception ex = expr_15;
				this.ProgressComplete(this, EventArgs.Empty);
				Global.DisplayError("The photo could not be opened.", ex);
				ProjectData.ClearProjectError();
			}
			this.UpdateStatusbar();
		}
		private void SaveCurrentPhoto()
		{
			this.panePhotos.SavePhoto();
			this.UpdateThumbnails();
			this.UpdateDetailCommandButtons();
			this.UpdateToolbar();
			this.UpdateStatusbar();
			this.paneDetails.SetActionValues(0, 0, 0, 0);
		}
		private void ClosingCurrentPhoto(bool displayThumbnails)
		{
			if (this.panePhotos.PhotoDirty)
			{
				DialogResult dialogResult;
				if (Global.ActionList.OnlyRotates)
				{
					dialogResult = 6;
				}
				else
				{
					dialogResult = MessageBox.Show(this, "The photo has been modified. Do you want to save the changes?", "Save Changes", 4, 32);
				}
				if (dialogResult == 6)
				{
					this.SaveCurrentPhoto();
				}
			}
			Global.ActionList.Clear();
		}
		private void UpdateThumbnails()
		{
			this.panePhotos.UpdateThumbnails(this.paneAlbums.SelectedAlbum);
		}
		private void UpdateToolbar()
		{
			this.tbNewAlbum.set_Enabled(true);
			this.tbDelete.set_Enabled(this.IsCommandEnabled(MainForm.Command.Delete));
			this.tbUpload.set_Enabled(this.IsCommandEnabled(MainForm.Command.Upload));
			this.tbWebsite.set_Enabled(true);
			this.tbEmail.set_Enabled(true);
			this.tbPrint.set_Enabled(this.IsCommandEnabled(MainForm.Command.PrintPhoto));
			this.tbThumbnails.set_Pushed(BooleanType.FromObject(Interaction.IIf(this.panePhotos.Mode == PhotosMode.Thumbnails & this.paneDetails.Mode == DetailsMode.None, true, false)));
			this.tbThumbnailsDetails.set_Pushed(BooleanType.FromObject(Interaction.IIf(this.panePhotos.Mode == PhotosMode.Thumbnails & (this.paneDetails.Mode == DetailsMode.PhotoDetails | this.paneDetails.Mode == DetailsMode.AlbumDetails), true, false)));
			this.tbPhotoShow.set_Enabled(this.IsCommandEnabled(MainForm.Command.PhotoShow));
			this.tbPhotoShow.set_Pushed(BooleanType.FromObject(Interaction.IIf(this.panePhotos.Mode == PhotosMode.PhotoShow & this.paneDetails.Mode == DetailsMode.None, true, false)));
			this.tbPhotoShowDetails.set_Enabled(this.IsCommandEnabled(MainForm.Command.PhotoShow));
			this.tbPhotoShowDetails.set_Pushed(BooleanType.FromObject(Interaction.IIf(this.panePhotos.Mode == PhotosMode.PhotoShow & this.paneDetails.Mode == DetailsMode.PhotoDetails, true, false)));
			this.tbPhotoActions.set_Enabled(this.IsCommandEnabled(MainForm.Command.PhotoActions));
			this.tbPhotoActions.set_Pushed(BooleanType.FromObject(Interaction.IIf(this.panePhotos.Mode == PhotosMode.PhotoAction, true, false)));
			this.tbPrev.set_Enabled(this.IsCommandEnabled(MainForm.Command.PreviousPhoto));
			this.tbNext.set_Enabled(this.IsCommandEnabled(MainForm.Command.NextPhoto));
			bool enabled = this.IsCommandEnabled(MainForm.Command.Rotate);
			this.tbRotateLeft.set_Enabled(enabled);
			this.tbRotateRight.set_Enabled(enabled);
			this.tbUpload.set_Enabled(this.IsCommandEnabled(MainForm.Command.Upload));
		}
		private void UpdateStatusbar()
		{
			bool flag = BooleanType.FromObject(Interaction.IIf(this.panePhotos.Mode == PhotosMode.Thumbnails, true, false));
			if (flag)
			{
				int count = this.paneAlbums.Count;
				this._orgStatusText[0] = string.Format("{0} Album{1}", count, StringType.FromObject(Interaction.IIf(count == 1, "", "s")));
				int photoCount = FileManager.GetPhotoCount(this.paneAlbums.SelectedAlbum);
				this._orgStatusText[1] = string.Format("{0} Photo{1}", photoCount, StringType.FromObject(Interaction.IIf(photoCount == 1, "", "s")));
			}
			else
			{
				PhotoInfo photoInfo = this.panePhotos.PhotoInfo;
				this._orgStatusText[0] = photoInfo.FileName;
				this._orgStatusText[1] = string.Format("Dimensions: {0} x {1}   Type: {2}   Size: {3}", new object[]
				{
					photoInfo.ImageSize.get_Width(),
					photoInfo.ImageSize.get_Height(),
					photoInfo.ImageType,
					photoInfo.FileLengthString
				});
			}
			if (!this.progressBar.get_Visible())
			{
				this.statusBar.get_Panels().get_Item(0).set_Text(this._orgStatusText[0]);
				this.statusBar.get_Panels().get_Item(1).set_Text(this._orgStatusText[1]);
			}
		}
		private void statusBar_DrawItem(object sender, StatusBarDrawItemEventArgs sbdevent)
		{
			Graphics graphics = sbdevent.get_Graphics();
			RectangleF rectangleF = new RectangleF((float)sbdevent.get_Bounds().get_Left(), (float)sbdevent.get_Bounds().get_Top(), (float)sbdevent.get_Bounds().get_Width(), (float)sbdevent.get_Bounds().get_Height());
			RectangleF rectangleF2 = rectangleF;
			if (sbdevent.get_Index() == 0 && this.progressBar.get_Visible())
			{
				checked
				{
					this.imageListStatusbar.Draw(graphics, (int)Math.Round((double)rectangleF2.get_Left()) + 2, (int)Math.Round((double)rectangleF2.get_Top()) + ((int)Math.Round((double)rectangleF2.get_Height()) - this.imageListStatusbar.get_ImageSize().get_Height()) / 2, 0);
				}
				rectangleF2.set_X(rectangleF2.get_X() + (float)checked(this.imageListStatusbar.get_ImageSize().get_Width() + 5));
				rectangleF2.set_Width(rectangleF2.get_Width() - (float)checked(this.imageListStatusbar.get_ImageSize().get_Width() + 5));
			}
			graphics.DrawString(sbdevent.get_Panel().get_Text(), this.statusBar.get_Font(), SystemBrushes.get_ControlText(), rectangleF2, this._format);
		}
		private bool IsCommandEnabled(MainForm.Command command)
		{
			bool photoDirty = this.panePhotos.PhotoDirty;
			bool flag = BooleanType.FromObject(Interaction.IIf(this.panePhotos.Mode == PhotosMode.Thumbnails, true, false));
			int selectedCount = this.panePhotos.SelectedCount;
			bool flag2 = BooleanType.FromObject(Interaction.IIf(flag & this.panePhotos.SelectedPhoto != null, true, false));
			switch (command)
			{
			case MainForm.Command.PrintPhoto:
				return BooleanType.FromObject(Interaction.IIf((flag & selectedCount > 0) | !flag, true, false));
			case MainForm.Command.PreviousPhoto:
				return this.panePhotos.Mode == PhotosMode.PhotoShow && BooleanType.FromObject(Interaction.IIf(this._curPhotoShowIndex > 0, true, false));
			case MainForm.Command.NextPhoto:
				return this.panePhotos.Mode == PhotosMode.PhotoShow && BooleanType.FromObject(Interaction.IIf(this._curPhotoShowIndex < checked(this.panePhotos.Count - 1), true, false));
			case MainForm.Command.PhotoShow:
				return BooleanType.FromObject(Interaction.IIf(this.panePhotos.Count > 0, true, false));
			case MainForm.Command.PhotoActions:
				return BooleanType.FromObject(Interaction.IIf(flag2 | !flag, true, false));
			case MainForm.Command.ThumbnailDetails:
				return BooleanType.FromObject(Interaction.IIf(this.panePhotos.Mode == PhotosMode.Thumbnails | this.panePhotos.Mode == PhotosMode.PhotoShow, true, false));
			case MainForm.Command.Save:
				return photoDirty;
			case MainForm.Command.Open:
				return BooleanType.FromObject(Interaction.IIf(flag & selectedCount == 1, true, false));
			case MainForm.Command.Rename:
				if (this.paneAlbums.Active)
				{
					return BooleanType.FromObject(Interaction.IIf(this.paneAlbums.SelectedCount == 1, true, false));
				}
				return this.panePhotos.Active && flag && BooleanType.FromObject(Interaction.IIf(selectedCount == 1, true, false));
			case MainForm.Command.Delete:
				if (this.paneAlbums.Active)
				{
					return BooleanType.FromObject(Interaction.IIf(this.paneAlbums.SelectedCount == 1, true, false));
				}
				return this.panePhotos.Active && (!flag || BooleanType.FromObject(Interaction.IIf(selectedCount > 0, true, false)));
			case MainForm.Command.SelectAll:
				return BooleanType.FromObject(Interaction.IIf(flag && this.panePhotos.Count > 0, true, false));
			case MainForm.Command.FullScreen:
				return BooleanType.FromObject(Interaction.IIf(this.panePhotos.Mode == PhotosMode.PhotoShow && this.panePhotos.Count > 0, true, false));
			case MainForm.Command.ClipboardCopy:
				return this.panePhotos.Active && (!flag || (selectedCount == 1 && !this.panePhotos.InLabelEdit));
			case MainForm.Command.ClipboardPaste:
			{
				if (!this.panePhotos.Active)
				{
					return false;
				}
				if (!flag)
				{
					return true;
				}
				if (this.panePhotos.InLabelEdit)
				{
					return false;
				}
				IDataObject dataObject = Clipboard.GetDataObject();
				return BooleanType.FromObject(Interaction.IIf(dataObject != null && dataObject.GetDataPresent(DataFormats.Bitmap), true, false));
			}
			case MainForm.Command.Rotate:
				return !this.paneAlbums.Active && BooleanType.FromObject(Interaction.IIf(!flag | this.panePhotos.SelectedCount > 0, true, false));
			case MainForm.Command.Upload:
				return this._allowUpload;
			default:
				return false;
			}
		}
		private void ChangeMode(MainForm.ChangeModeAction action)
		{
			bool flag = BooleanType.FromObject(Interaction.IIf(this.panePhotos.Mode == PhotosMode.Thumbnails, true, false));
			bool flag2 = false;
			try
			{
				switch (action)
				{
				case MainForm.ChangeModeAction.Thumbnails:
				case MainForm.ChangeModeAction.ThumbnailsDetails:
					this.ClosePhotosView();
					this.paneDetails.SetPhotoList(this.panePhotos.GetSelectedPhotos());
					this.panePhotos.Mode = PhotosMode.Thumbnails;
					this.paneDetails.Mode = (DetailsMode)Interaction.IIf(action == MainForm.ChangeModeAction.Thumbnails, DetailsMode.None, DetailsMode.PhotoDetails);
					this._showThumbnailDetails = BooleanType.FromObject(Interaction.IIf(action == MainForm.ChangeModeAction.ThumbnailsDetails, true, false));
					this.UpdateDetailsMode();
					this.UpdateStatusbar();
					break;
				case MainForm.ChangeModeAction.Photo:
				case MainForm.ChangeModeAction.PhotoDetails:
					if (flag)
					{
						if (this.panePhotos.SelectedCount == 1)
						{
							this.panePhotos.OpenSelectedPhoto();
							this._curPhotoShowIndex = this.panePhotos.CurrentPhotoIndex;
						}
						else
						{
							this.ClosingCurrentPhoto(false);
							int[] selectedIndices = this.panePhotos.GetSelectedIndices();
							if (selectedIndices == null)
							{
								this.panePhotos.OpenPhotoAtIndex(0);
								this._curPhotoShowIndex = 0;
							}
							else
							{
								this.panePhotos.OpenPhotoAtIndex(selectedIndices[0]);
								this._curPhotoShowIndex = selectedIndices[0];
							}
						}
						flag2 = true;
					}
					else
					{
						this.ClosingCurrentPhoto(false);
						this.panePhotos.OpenPhotoAtIndex(this._curPhotoShowIndex);
						flag2 = true;
					}
					this.panePhotos.Mode = PhotosMode.PhotoShow;
					this.paneDetails.Mode = (DetailsMode)Interaction.IIf(action == MainForm.ChangeModeAction.Photo, DetailsMode.None, DetailsMode.PhotoDetails);
					this.paneDetails.SetPhotoList(this.panePhotos.GetPhoto(this._curPhotoShowIndex));
					break;
				case MainForm.ChangeModeAction.PhotoActions:
					if (flag)
					{
						this.panePhotos.OpenSelectedPhoto();
						flag2 = true;
						this._curPhotoShowIndex = this.panePhotos.CurrentPhotoIndex;
					}
					else
					{
						this.ClosingCurrentPhoto(false);
					}
					this.paneDetails.Mode = DetailsMode.PhotoActions;
					this.paneDetails.Mode = DetailsMode.PhotoActions;
					this.panePhotos.Mode = PhotosMode.PhotoAction;
					break;
				case MainForm.ChangeModeAction.NewAlbumSelected:
				{
					if (!flag)
					{
						this.ClosePhotosView();
					}
					string selectedAlbum = this.paneAlbums.SelectedAlbum;
					if (StringType.StrCmp(selectedAlbum, "", false) == 0)
					{
						this.panePhotos.ClearThumbnails();
					}
					else
					{
						this.panePhotos.UpdateThumbnails(selectedAlbum);
					}
					this.paneDetails.AlbumName = selectedAlbum;
					this.panePhotos.Mode = PhotosMode.Thumbnails;
					if (!this._showThumbnailDetails)
					{
						this.paneDetails.Mode = DetailsMode.None;
					}
					else
					{
						this.UpdateDetailsMode();
					}
					this.UpdateStatusbar();
					break;
				}
				}
				if (flag2)
				{
					this.UpdateStatusbar();
					this.UpdateDetailCommandButtons();
				}
			}
			catch (Exception expr_28B)
			{
				ProjectData.SetProjectError(expr_28B);
				Exception ex = expr_28B;
				this.ProgressComplete(this, EventArgs.Empty);
				Global.DisplayError("An error occurred processing the command.", ex);
				ProjectData.ClearProjectError();
			}
			this.UpdateToolbar();
		}
		private void ClosePhotosView()
		{
			this.ClosingCurrentPhoto(true);
			this.panePhotos.Mode = PhotosMode.Thumbnails;
			this.paneDetails.Mode = DetailsMode.PhotoDetails;
			this.UpdateToolbar();
			this.UpdateStatusbar();
		}
		private void SaveSettings()
		{
			Global.Settings.SetValue(SettingKey.LastAlbum, this.paneAlbums.SelectedAlbum);
			this.SaveWindowSettings();
		}
		private void SaveWindowSettings()
		{
			Global.Settings.SetValue(SettingKey.WindowPlacement, string.Format("{0},{1},{2},{3}", new object[]
			{
				this.get_Bounds().get_X(),
				this.get_Bounds().get_Y(),
				this.get_Bounds().get_Width(),
				this.get_Bounds().get_Height()
			}));
			Global.Settings.SetValue(SettingKey.AlbumPaneWidth, this.paneAlbums.get_Width());
			Global.Settings.SetValue(SettingKey.DetailsPaneWidth, this.paneDetails.get_Width());
		}
		private void RestoreWindowSettings()
		{
			try
			{
				string[] array = Global.Settings.GetString(SettingKey.WindowPlacement).Split(new char[]
				{
					','
				});
				if (array.get_Length() == 4)
				{
					Rectangle rectangle = new Rectangle(IntegerType.FromString(array[0]), IntegerType.FromString(array[1]), IntegerType.FromString(array[2]), IntegerType.FromString(array[3]));
					Rectangle rectangle2 = rectangle;
					rectangle2 = Rectangle.Intersect(Screen.get_PrimaryScreen().get_WorkingArea(), rectangle2);
					if (rectangle2.get_Width() > 0 & rectangle2.get_Height() > 0)
					{
						this.set_StartPosition(0);
						this.SetBounds(rectangle2.get_Left(), rectangle2.get_Top(), rectangle2.get_Width(), rectangle2.get_Height());
						this.paneAlbums.set_Width(Global.Settings.GetInt(SettingKey.AlbumPaneWidth));
						this.paneDetails.set_Width(Global.Settings.GetInt(SettingKey.DetailsPaneWidth));
					}
				}
			}
			catch (Exception expr_D5)
			{
				ProjectData.SetProjectError(expr_D5);
				this.set_StartPosition(2);
				ProjectData.ClearProjectError();
			}
		}
		private bool HandleUncheckedPreviousPhoto()
		{
			if (this.IsCommandEnabled(MainForm.Command.PreviousPhoto))
			{
				this.menuPrevPhoto_Click(this, EventArgs.Empty);
				return true;
			}
			if (this.FullScreen)
			{
				this.FullScreen = false;
				return true;
			}
			bool result;
			return result;
		}
		private bool HandleUncheckedNextPhoto()
		{
			if (this.IsCommandEnabled(MainForm.Command.NextPhoto))
			{
				this.menuNextPhoto_Click(this, EventArgs.Empty);
				return true;
			}
			if (this.FullScreen)
			{
				this.FullScreen = false;
				return true;
			}
			bool result;
			return result;
		}
		private void DisplayFullScreen(bool fullScreen)
		{
			if (fullScreen)
			{
				this.SaveWindowSettings();
				this.set_FormBorderStyle(0);
				this.set_WindowState(2);
				this.set_Menu(null);
				this.paneAlbums.set_Visible(false);
				this.statusBar.set_Visible(false);
				this.toolBar.set_Visible(false);
				this.panePhotos.FullScreen = true;
			}
			else
			{
				this.set_FormBorderStyle(4);
				this.set_WindowState(0);
				this.paneAlbums.set_Visible(true);
				this.statusBar.set_Visible(true);
				this.toolBar.set_Visible(true);
				this.panePhotos.FullScreen = false;
				this.set_Menu(this.mainMenu);
				this.RestoreWindowSettings();
			}
		}
		private void splitterRight_SplitterMoving(object sender, SplitterEventArgs e)
		{
			checked
			{
				if (e.get_SplitX() < this.panelPhotos.get_Width() - 312)
				{
					e.set_SplitX(this.panelPhotos.get_Width() - 312);
				}
			}
		}
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\r\n               version=\"1\">\r\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\r\n                version=\"1\"\r\n                Flags=\"UnmanagedCode\"/>\r\n</PermissionSet>\r\n")]
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (Global.Busy)
			{
				return true;
			}
			if (msg.get_Msg() != 256)
			{
				return base.ProcessCmdKey(ref msg, keyData);
			}
			bool flag = this.panePhotos.Active && this.panePhotos.Mode == PhotosMode.PhotoShow;
			if (keyData == 46)
			{
				if (!this.FullScreen && this.IsCommandEnabled(MainForm.Command.Delete))
				{
					this.menuDelete_Click(this, EventArgs.Empty);
				}
			}
			else
			{
				if (keyData == 113)
				{
					if (!this.FullScreen && this.IsCommandEnabled(MainForm.Command.Rename))
					{
						this.menuRename_Click(this, EventArgs.Empty);
					}
				}
				else
				{
					if (keyData == 37)
					{
						if (flag)
						{
							if (this.HandleUncheckedPreviousPhoto())
							{
								return true;
							}
						}
					}
					else
					{
						if (keyData == 39)
						{
							if (flag)
							{
								if (this.HandleUncheckedNextPhoto())
								{
									return true;
								}
							}
						}
						else
						{
							if (keyData == 27 && flag && this.FullScreen)
							{
								this.FullScreen = false;
								return true;
							}
						}
					}
				}
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\r\n               version=\"1\">\r\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\r\n                version=\"1\"\r\n                Flags=\"UnmanagedCode\"/>\r\n</PermissionSet>\r\n")]
		protected override void WndProc(ref Message m)
		{
			if (Global.Busy)
			{
				if (m.get_Msg() == 15 || m.get_Msg() == 133)
				{
					base.WndProc(ref m);
				}
				return;
			}
			if (m.get_Msg() == 6 && m.get_WParam().ToInt32() != 0 && Global.PublishingFiles)
			{
				this._uploadForm.Activate();
				return;
			}
			base.WndProc(ref m);
		}
		private void menuFile_Popup(object sender, EventArgs e)
		{
			this.menuSave.set_Enabled(this.IsCommandEnabled(MainForm.Command.Save));
			this.menuOpen.set_Enabled(this.IsCommandEnabled(MainForm.Command.Open));
			this.menuRename.set_Enabled(this.IsCommandEnabled(MainForm.Command.Rename));
			this.menuDelete.set_Enabled(this.IsCommandEnabled(MainForm.Command.Delete));
			this.menuPrint.set_Enabled(this.IsCommandEnabled(MainForm.Command.PrintPhoto));
			this.menuUploadAllChanges.set_Enabled(this.IsCommandEnabled(MainForm.Command.Upload));
			if (this.paneAlbums.Active)
			{
				this.menuRename.set_Text("&Rename Album");
				this.menuDelete.set_Text("&Delete Album");
			}
			else
			{
				this.menuRename.set_Text("&Rename Photo");
				this.menuDelete.set_Text(string.Format("&Delete Photo{0}", RuntimeHelpers.GetObjectValue(Interaction.IIf(this.panePhotos.SelectedCount > 1, "s", ""))));
			}
		}
		private void menuSave_Click(object sender, EventArgs e)
		{
			this.SaveCurrentPhoto();
		}
		private void menuNewAlbum_Click(object sender, EventArgs e)
		{
			this.paneAlbums.CreateNewAlbum();
		}
		private void menuImportPhotos_Click(object sender, EventArgs e)
		{
			this.paneAlbums.ImportPhotos();
		}
		private void menuImportFolder_Click(object sender, EventArgs e)
		{
			this.paneAlbums.ImportFolder();
		}
		private void menuWebsite_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start(Global.Settings.GetString(SettingKey.ServiceLocation));
			}
			catch (Exception expr_14)
			{
				ProjectData.SetProjectError(expr_14);
				Exception ex = expr_14;
				Global.DisplayError("The website could not be opened.", ex);
				ProjectData.ClearProjectError();
			}
		}
		private void menuEmailChanges_Click(object sender, EventArgs e)
		{
			try
			{
				string text = Global.Settings.GetString(SettingKey.EmailSubject);
				if (text.get_Length() == 0)
				{
					text = " ";
				}
				Process.Start(string.Format("mailto:?subject={0}&body={1}", text, Global.Settings.GetString(SettingKey.ServiceLocation)));
			}
			catch (Exception expr_3A)
			{
				ProjectData.SetProjectError(expr_3A);
				Exception ex = expr_3A;
				Global.DisplayError("The email message could not be created.", ex);
				ProjectData.ClearProjectError();
			}
		}
		private void menuOpen_Click(object sender, EventArgs e)
		{
			this.ChangeMode(MainForm.ChangeModeAction.Photo);
		}
		private void menuDelete_Click(object sender, EventArgs e)
		{
			if (this.paneAlbums.Active)
			{
				this.paneAlbums.Delete();
				return;
			}
			if (this.panePhotos.Active)
			{
				this.panePhotos.Delete();
				return;
			}
			if (this.paneDetails.Mode == DetailsMode.PhotoActions)
			{
				this.panePhotos.Delete();
				return;
			}
		}
		private void menuRename_Click(object sender, EventArgs e)
		{
			if (this.paneAlbums.Active)
			{
				this.paneAlbums.Rename();
				return;
			}
			if (this.panePhotos.Active)
			{
				this.panePhotos.Rename();
				return;
			}
		}
		private void menuPrint_Click(object sender, EventArgs e)
		{
			bool flag = BooleanType.FromObject(Interaction.IIf(this.panePhotos.Mode == PhotosMode.Thumbnails, true, false));
			try
			{
				if (flag)
				{
					Print.PrintFiles(this.panePhotos.GetSelectedPhotos());
				}
				else
				{
					if (!this.panePhotos.PhotoDirty)
					{
						Print.PrintFile(this.panePhotos.PhotoInfo.Path);
					}
					else
					{
						Cursor.set_Current(Cursors.get_WaitCursor());
						Bitmap imageWithActions = this.panePhotos.ImageWithActions;
						if (imageWithActions != null)
						{
							Cursor.set_Current(Cursors.get_WaitCursor());
							string tempFileName = Path.GetTempFileName();
							imageWithActions.Save(tempFileName);
							Cursor.set_Current(Cursors.get_Default());
							Print.PrintFile(tempFileName);
						}
					}
				}
			}
			catch (Exception expr_A7)
			{
				ProjectData.SetProjectError(expr_A7);
				Exception ex = expr_A7;
				Global.DisplayError("The photo could not be printed.", ex);
				ProjectData.ClearProjectError();
			}
			finally
			{
				Bitmap imageWithActions;
				if (imageWithActions != null)
				{
					imageWithActions.Dispose();
				}
				string tempFileName;
				if (tempFileName != null)
				{
					FileManager.DeleteFile(tempFileName);
				}
				Cursor.set_Current(Cursors.get_Default());
			}
		}
		private void menuExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		private void menuEdit_Popup(object sender, EventArgs e)
		{
			bool photoDirty = this.panePhotos.PhotoDirty;
			this.menuCopy.set_Enabled(this.IsCommandEnabled(MainForm.Command.ClipboardCopy));
			this.menuPaste.set_Enabled(this.IsCommandEnabled(MainForm.Command.ClipboardPaste));
			this.menuUndo.set_Enabled(photoDirty);
			if (photoDirty)
			{
				this.menuUndo.set_Text(string.Format("&Undo - {0}", Global.ActionList.LastItem.ToString()));
			}
			else
			{
				this.menuUndo.set_Text("&Undo");
			}
			this.menuRedo.set_Enabled(Global.ActionList.ContainsRedo);
			if (Global.ActionList.ContainsRedo)
			{
				this.menuRedo.set_Text(string.Format("&Redo - {0}", Global.ActionList.RedoItem.ToString()));
			}
			else
			{
				this.menuRedo.set_Text("&Redo");
			}
			this.menuDiscardChanges.set_Enabled(photoDirty);
			this.menuRotate.set_Enabled(this.IsCommandEnabled(MainForm.Command.Rotate));
			this.menuSelectAll.set_Enabled(this.IsCommandEnabled(MainForm.Command.SelectAll));
		}
		private void menuDiscardChanges_Click(object sender, EventArgs e)
		{
			this.panePhotos.DiscardChanges();
			this.UpdateToolbar();
			this.UpdateDetailCommandButtons();
			this.paneDetails.SetActionValues(0, 0, 0, 0);
		}
		private void menuUndo_Click(object sender, EventArgs e)
		{
			this.panePhotos.Undo();
			this.UpdateToolbar();
			this.UpdateDetailCommandButtons();
			ActionItem lastItem = Global.ActionList.LastItem;
			if (lastItem == null)
			{
				this.paneDetails.SetActionValues(0, 0, 0, 0);
			}
			else
			{
				this.paneDetails.SetActionValues(lastItem.SliderValues.Contrast, lastItem.SliderValues.Brightness, lastItem.SliderValues.Gamma, lastItem.SliderValues.Saturation);
			}
		}
		private void menuRedo_Click(object sender, EventArgs e)
		{
			ActionItem redoItem = Global.ActionList.RedoItem;
			Global.SetSliderValues(redoItem.SliderValues.Contrast, redoItem.SliderValues.Brightness, redoItem.SliderValues.Gamma, redoItem.SliderValues.Saturation);
			this.paneDetails_Action(this, new ActionEventArgs(redoItem));
			this.paneDetails.SetActionValues(redoItem.SliderValues.Contrast, redoItem.SliderValues.Brightness, redoItem.SliderValues.Gamma, redoItem.SliderValues.Saturation);
		}
		private void menuCopy_Click(object sender, EventArgs e)
		{
			Cursor.set_Current(Cursors.get_WaitCursor());
			try
			{
				Cursor.set_Current(Cursors.get_WaitCursor());
				Global.Progress.Update(this, "Copying to clipboard", 1, 2);
				bool flag = BooleanType.FromObject(Interaction.IIf(this.panePhotos.Mode == PhotosMode.Thumbnails, true, false));
				Bitmap bitmap2;
				if (flag)
				{
					Photo selectedPhoto = this.panePhotos.SelectedPhoto;
					if (selectedPhoto != null)
					{
						Bitmap bitmap = new Bitmap(selectedPhoto.PhotoPath);
						bitmap2 = new Bitmap(bitmap);
					}
				}
				if (!flag && !this.panePhotos.PhotoDirty)
				{
					Bitmap bitmap = new Bitmap(this.panePhotos.PhotoInfo.Path);
					bitmap2 = new Bitmap(bitmap);
				}
				if (!flag && this.panePhotos.PhotoDirty)
				{
					bitmap2 = this.panePhotos.ImageWithActions;
				}
				if (bitmap2 != null)
				{
					Global.Progress.Update(this, "Copying to clipboard", 2, 2);
					Clipboard.SetDataObject(bitmap2);
				}
			}
			catch (Exception expr_DA)
			{
				ProjectData.SetProjectError(expr_DA);
				Exception ex = expr_DA;
				Global.DisplayError("The photo could not be copied to the clipboard.", ex);
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
			Global.Progress.Complete(this);
			Cursor.set_Current(Cursors.get_Default());
		}
		private void menuPaste_Click(object sender, EventArgs e)
		{
			IDataObject dataObject = Clipboard.GetDataObject();
			if (dataObject.GetDataPresent(DataFormats.Bitmap))
			{
				Cursor.set_Current(Cursors.get_WaitCursor());
				Bitmap bitmap = (Bitmap)dataObject.GetData(DataFormats.Bitmap);
				if (bitmap != null)
				{
					this.paneAlbums.AddBitmap(bitmap);
					this.paneAlbums.UpdateAlbumCount(this.paneAlbums.SelectedAlbum);
					this.UpdateStatusbar();
				}
				Cursor.set_Current(Cursors.get_Default());
			}
		}
		private void menuSelectAll_Click(object sender, EventArgs e)
		{
			this.panePhotos.SelectAll();
		}
		private void menuRotateLeft_Click(object sender, EventArgs e)
		{
			this.paneDetails_Action(this, new ActionEventArgs(PhotoAction.RotateLeft));
		}
		private void menuRotateRight_Click(object sender, EventArgs e)
		{
			this.paneDetails_Action(this, new ActionEventArgs(PhotoAction.RotateRight));
		}
		private void menuFlipHorz_Click(object sender, EventArgs e)
		{
			this.paneDetails_Action(this, new ActionEventArgs(PhotoAction.FlipHorizontal));
		}
		private void menuFlipVert_Click(object sender, EventArgs e)
		{
			this.paneDetails_Action(this, new ActionEventArgs(PhotoAction.FlipVertical));
		}
		private void menuView_Popup(object sender, EventArgs e)
		{
			this.menuPrevPhoto.set_Enabled(this.IsCommandEnabled(MainForm.Command.PreviousPhoto));
			this.menuNextPhoto.set_Enabled(this.IsCommandEnabled(MainForm.Command.NextPhoto));
			this.menuFullScreen.set_Enabled(this.IsCommandEnabled(MainForm.Command.FullScreen));
		}
		private void menuFullScreen_Click(object sender, EventArgs e)
		{
			this.FullScreen = true;
		}
		private void menuPrevPhoto_Click(object sender, EventArgs e)
		{
			this._curPhotoShowIndex = checked(this.panePhotos.CurrentPhotoIndex - 1);
			this.OpenPhoto(this._curPhotoShowIndex);
			if (this.paneDetails.Mode == DetailsMode.PhotoDetails)
			{
				this.paneDetails.SetPhotoList(this.panePhotos.GetPhoto(this._curPhotoShowIndex));
			}
			this.UpdateToolbar();
		}
		private void menuNextPhoto_Click(object sender, EventArgs e)
		{
			this._curPhotoShowIndex = checked(this.panePhotos.CurrentPhotoIndex + 1);
			this.OpenPhoto(this._curPhotoShowIndex);
			if (this.paneDetails.Mode == DetailsMode.PhotoDetails)
			{
				this.paneDetails.SetPhotoList(this.panePhotos.GetPhoto(this._curPhotoShowIndex));
			}
			this.UpdateToolbar();
		}
		private void menuOptions_Click(object sender, EventArgs e)
		{
			int @int = Global.Settings.GetInt(SettingKey.PublishPhotoSize);
			int int2 = Global.Settings.GetInt(SettingKey.PublishPhotoQuality);
			SettingsForm settingsForm = new SettingsForm();
			settingsForm.ShowDialog(this);
			int int3 = Global.Settings.GetInt(SettingKey.PublishPhotoSize);
			int int4 = Global.Settings.GetInt(SettingKey.PublishPhotoQuality);
			if (@int != int3 || int2 != int4)
			{
				Publish.Delete();
			}
		}
		private void menuAbout_Click(object sender, EventArgs e)
		{
			AboutForm aboutForm = new AboutForm();
			aboutForm.ShowDialog(this);
		}
		private void paneAlbums_SelectedAlbumChanged(object sender, EventArgs e)
		{
			this.ChangeMode(MainForm.ChangeModeAction.NewAlbumSelected);
		}
		private void paneAlbums_SelectedAlbumClicked(object sender, EventArgs e)
		{
			if (this.panePhotos.Mode == PhotosMode.Thumbnails && this.panePhotos.SelectedCount > 0 && this.paneDetails.Mode == DetailsMode.PhotoDetails)
			{
				this.panePhotos.UnselectAll();
			}
		}
		private void paneAlbums_PublishAlbumClicked(object sender, PublishAlbumEventArgs e)
		{
			this.paneDetails.PublishAlbum(e.AlbumName, e.Publish);
		}
		private void paneAlbums_NewAlbumCreated(object sender, AlbumNameEventArgs e)
		{
			if (this.panePhotos.Mode == PhotosMode.Thumbnails)
			{
				this.paneAlbums.SelectedAlbum = e.AlbumName;
			}
		}
		private void paneAlbums_PhotosAdded(object sender, AlbumNameEventArgs e)
		{
			if (StringType.StrCmp(this.paneAlbums.SelectedAlbum, e.AlbumName, false) == 0)
			{
				this.panePhotos.UpdateThumbnails(e.AlbumName);
				this._curPhotoShowIndex = this.panePhotos.CurrentPhotoIndex;
				this.UpdateToolbar();
				this.UpdateStatusbar();
				return;
			}
			if (this.panePhotos.Mode == PhotosMode.Thumbnails & StringType.StrCmp(this.paneAlbums.SelectedAlbum, "", false) == 0)
			{
				this.paneAlbums.SelectedAlbum = e.AlbumName;
			}
		}
		private void paneAlbums_PhotosRemoved(object sender, AlbumNameEventArgs e)
		{
			this.UpdateStatusbar();
		}
		private void paneAlbums_AlbumRenamed(object sender, AlbumRenamedEventArgs e)
		{
			this.panePhotos.UpdateThumbnails(e.NewName);
			this.paneDetails.AlbumName = e.NewName;
			if (this.paneDetails.Mode == DetailsMode.PhotoDetails)
			{
				this.paneDetails.SetPhotoList(this.panePhotos.GetPhoto(this._curPhotoShowIndex));
			}
			this.UpdateToolbar();
		}
		private void panePhotos_FilesDropped(object sender, FilesDroppedEventArgs e)
		{
			this.paneAlbums.ImportFiles(e.GetFiles());
		}
		private void panePhotos_SelectionChanged(object sender, EventArgs e)
		{
			this.paneDetails.SetPhotoList(this.panePhotos.GetSelectedPhotos());
			this.UpdateDetailsMode();
			this.UpdateToolbar();
		}
		private void panePhotos_CropDataChanged(object sender, CropDataChangedEventArgs e)
		{
			this.paneDetails.CropDataChanged(e.OrgSize, e.NewSize, e.CropBounds);
		}
		private void panePhotos_PhotosMenuClicked(object sender, PhotosMenuClickedEventArgs e)
		{
			switch (e.Action)
			{
			case PhotosContextAction.PhotoShow:
				if (!this.tbPhotoShow.get_Pushed())
				{
					this.ChangeMode(MainForm.ChangeModeAction.Photo);
				}
				break;
			case PhotosContextAction.PhotoShowDetails:
				if (!this.tbPhotoShowDetails.get_Pushed())
				{
					this.ChangeMode(MainForm.ChangeModeAction.PhotoDetails);
				}
				break;
			case PhotosContextAction.PhotoActions:
				if (!this.tbPhotoActions.get_Pushed())
				{
					this.ChangeMode(MainForm.ChangeModeAction.PhotoActions);
				}
				break;
			case PhotosContextAction.SelectAll:
				this.menuSelectAll_Click(this, EventArgs.Empty);
				break;
			case PhotosContextAction.RotateLeft:
				this.menuRotateLeft_Click(this, EventArgs.Empty);
				break;
			case PhotosContextAction.RotateRight:
				this.menuRotateRight_Click(this, EventArgs.Empty);
				break;
			case PhotosContextAction.Rename:
				this.menuRename_Click(this, EventArgs.Empty);
				break;
			case PhotosContextAction.Delete:
				this.menuDelete_Click(this, EventArgs.Empty);
				break;
			case PhotosContextAction.PreviousPhoto:
				this.HandleUncheckedPreviousPhoto();
				break;
			case PhotosContextAction.NextPhoto:
				this.HandleUncheckedNextPhoto();
				break;
			}
		}
		private void panePhotos_PhotosDeleted(object sender, EventArgs e)
		{
			this.paneAlbums.UpdateAlbumCount(this.paneAlbums.SelectedAlbum);
			this.UpdateStatusbar();
			if (this.panePhotos.Mode == PhotosMode.Thumbnails)
			{
				this.UpdateThumbnails();
				return;
			}
			this.UpdateThumbnails();
			if (this.panePhotos.Count == 0)
			{
				this.ChangeMode(MainForm.ChangeModeAction.ThumbnailsDetails);
				return;
			}
			checked
			{
				if (this._curPhotoShowIndex == this.panePhotos.Count)
				{
					this._curPhotoShowIndex--;
				}
				if (this.panePhotos.Mode == PhotosMode.PhotoAction)
				{
					this.ChangeMode(MainForm.ChangeModeAction.Photo);
				}
				else
				{
					this.ChangeMode((MainForm.ChangeModeAction)Interaction.IIf(this.paneDetails.Mode == DetailsMode.PhotoDetails, MainForm.ChangeModeAction.PhotoDetails, MainForm.ChangeModeAction.Photo));
				}
			}
		}
		private void panePhotos_OpenPhoto(object sender, EventArgs e)
		{
			this.menuOpen_Click(this, EventArgs.Empty);
		}
		private void paneDetails_PhotoMetadataChanged(object sender, PhotoMetadataChangedEventArgs e)
		{
			if (sender == this.paneDetails)
			{
				this.panePhotos.UpdateMetadata(e.Photo);
			}
			if (sender == this.panePhotos)
			{
				this.paneDetails.UpdateMetadata(e.Photo);
			}
		}
		private void paneDetails_CropModeChanged(object sender, CropModeChangedEventArgs e)
		{
			this.panePhotos.CropMode = e.CropMode;
		}
		private void paneDetails_Action(object sender, ActionEventArgs e)
		{
			Cursor.set_Current(Cursors.get_WaitCursor());
			bool flag = this.panePhotos.ApplyPhotoAction(e.ActionItem);
			Cursor.set_Current(Cursors.get_Default());
			this.UpdateToolbar();
			if (flag)
			{
				this.UpdateThumbnails();
			}
			this.UpdateDetailCommandButtons();
		}
		private void paneDetails_ActionCommand(object sender, CommandButtonClickedEventArgs e)
		{
			switch (e.Button)
			{
			case DetailsCommandButton.Save:
				this.menuSave_Click(this, EventArgs.Empty);
				break;
			case DetailsCommandButton.Reset:
				this.menuDiscardChanges_Click(this, EventArgs.Empty);
				break;
			case DetailsCommandButton.Undo:
				this.menuUndo_Click(this, EventArgs.Empty);
				break;
			case DetailsCommandButton.Redo:
				this.menuRedo_Click(this, EventArgs.Empty);
				break;
			case DetailsCommandButton.ClearCrop:
				this.panePhotos.ClearCrop();
				break;
			}
		}
		private void paneDetails_AlbumMetadataChanged(object sender, AlbumMetadataChangedEventArgs e)
		{
			bool flag = this.paneAlbums.UpdateAlbum(e.OldName, e.Album);
			if (StringType.StrCmp(e.OldName, e.Album.Name, false) != 0)
			{
				if (flag)
				{
					this.panePhotos.UpdateThumbnails(e.Album.Name);
					this.paneDetails.AlbumName = e.Album.Name;
					this.paneAlbums.UpdateAlbumCount(this.paneAlbums.SelectedAlbum);
					this.UpdateStatusbar();
				}
				if (!flag)
				{
					this.paneDetails.AlbumName = this.paneAlbums.SelectedAlbum;
				}
			}
		}
		private void menuUploadAllChanges_Click(object sender, EventArgs e)
		{
			this.paneDetails.Save();
			if (this.panePhotos.PhotoDirty)
			{
				this.ClosingCurrentPhoto(false);
				this.menuDiscardChanges_Click(this, EventArgs.Empty);
			}
			if (this._uploadForm == null)
			{
				this._uploadForm = new UploadForm();
				UploadForm arg_82_0 = this._uploadForm;
				Point initialLocation = checked(new Point(this.get_Left() + (this.get_Width() - this._uploadForm.get_Width()) / 2, this.get_Top() + (this.get_Height() - this._uploadForm.get_Height()) / 2));
				arg_82_0.InitialLocation = initialLocation;
			}
			this._uploadForm.Publish(this.paneAlbums.GetPublishList());
			this._uploadForm.set_Owner(this);
			this.set_Cursor(Cursors.get_WaitCursor());
			this.toolBar.set_Enabled(false);
			Global.PublishingFiles = true;
			this._allowUpload = false;
			this.UpdateToolbar();
		}
		private void uploadForm_PublishComplete(object sender, UploadCompleteEventArgs e)
		{
			this.toolBar.set_Enabled(true);
			this._uploadForm.set_Owner(null);
			this.set_Cursor(Cursors.get_Default());
			Global.PublishingFiles = false;
			if (e.ErrorOccurred)
			{
				this._allowUpload = true;
				this.UpdateToolbar();
			}
			else
			{
				this._uploadForm.Upload();
			}
		}
		private void uploadForm_UploadComplete(object sender, UploadCompleteEventArgs e)
		{
			this._allowUpload = true;
			this.UpdateToolbar();
		}
	}
}
