using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace FotoVision
{
	public class DetailsPane : BasePane
	{
		private class Consts
		{
			public const string CaptionAlbum = "Album Description";
			public const string CaptionPhoto = "Photo Description";
			public const string CaptionActions = "";
		}
		public delegate void PhotoMetadataChangedEventHandler(object sender, PhotoMetadataChangedEventArgs e);
		public delegate void AlbumMetadataChangedEventHandler(object sender, AlbumMetadataChangedEventArgs e);
		public delegate void CropModeChangedEventHandler(object sender, CropModeChangedEventArgs e);
		public delegate void ActionEventHandler(object sender, ActionEventArgs e);
		public delegate void CommandButtonClickedEventHandler(object sender, CommandButtonClickedEventArgs e);
		[AccessedThroughProperty("photoDetails")]
		private DetailsPhotos _photoDetails;
		private DetailsPane.CommandButtonClickedEventHandler CommandButtonClickedEvent;
		private DetailsPane.ActionEventHandler ActionEvent;
		private DetailsPane.AlbumMetadataChangedEventHandler AlbumMetadataChangedEvent;
		[AccessedThroughProperty("albumDetails")]
		private DetailsAlbum _albumDetails;
		private DetailsPane.CropModeChangedEventHandler CropModeChangedEvent;
		[AccessedThroughProperty("photoActions")]
		private DetailsActions _photoActions;
		private DetailsPane.PhotoMetadataChangedEventHandler PhotoMetadataChangedEvent;
		private DetailsMode _mode;
		public const int MaximumWidth = 312;
		public event DetailsPane.PhotoMetadataChangedEventHandler PhotoMetadataChanged
		{
			[MethodImpl(32)]
			add
			{
				this.PhotoMetadataChangedEvent = (DetailsPane.PhotoMetadataChangedEventHandler)Delegate.Combine(this.PhotoMetadataChangedEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.PhotoMetadataChangedEvent = (DetailsPane.PhotoMetadataChangedEventHandler)Delegate.Remove(this.PhotoMetadataChangedEvent, value);
			}
		}
		public event DetailsPane.AlbumMetadataChangedEventHandler AlbumMetadataChanged
		{
			[MethodImpl(32)]
			add
			{
				this.AlbumMetadataChangedEvent = (DetailsPane.AlbumMetadataChangedEventHandler)Delegate.Combine(this.AlbumMetadataChangedEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.AlbumMetadataChangedEvent = (DetailsPane.AlbumMetadataChangedEventHandler)Delegate.Remove(this.AlbumMetadataChangedEvent, value);
			}
		}
		public event DetailsPane.CropModeChangedEventHandler CropModeChanged
		{
			[MethodImpl(32)]
			add
			{
				this.CropModeChangedEvent = (DetailsPane.CropModeChangedEventHandler)Delegate.Combine(this.CropModeChangedEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.CropModeChangedEvent = (DetailsPane.CropModeChangedEventHandler)Delegate.Remove(this.CropModeChangedEvent, value);
			}
		}
		public event DetailsPane.ActionEventHandler Action
		{
			[MethodImpl(32)]
			add
			{
				this.ActionEvent = (DetailsPane.ActionEventHandler)Delegate.Combine(this.ActionEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.ActionEvent = (DetailsPane.ActionEventHandler)Delegate.Remove(this.ActionEvent, value);
			}
		}
		public event DetailsPane.CommandButtonClickedEventHandler CommandButtonClicked
		{
			[MethodImpl(32)]
			add
			{
				this.CommandButtonClickedEvent = (DetailsPane.CommandButtonClickedEventHandler)Delegate.Combine(this.CommandButtonClickedEvent, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.CommandButtonClickedEvent = (DetailsPane.CommandButtonClickedEventHandler)Delegate.Remove(this.CommandButtonClickedEvent, value);
			}
		}
		private DetailsPhotos photoDetails
		{
			get
			{
				return this._photoDetails;
			}
			[MethodImpl(32)]
			set
			{
				if (this._photoDetails != null)
				{
					this._photoDetails.PhotoMetadataChanged -= new DetailsPhotos.PhotoMetadataChangedEventHandler(this.photoDetails_PhotoMetadataChanged);
				}
				this._photoDetails = value;
				if (this._photoDetails != null)
				{
					this._photoDetails.PhotoMetadataChanged += new DetailsPhotos.PhotoMetadataChangedEventHandler(this.photoDetails_PhotoMetadataChanged);
				}
			}
		}
		private DetailsAlbum albumDetails
		{
			get
			{
				return this._albumDetails;
			}
			[MethodImpl(32)]
			set
			{
				if (this._albumDetails != null)
				{
					this._albumDetails.AlbumMetadataChanged -= new DetailsAlbum.AlbumMetadataChangedEventHandler(this.albumDetails_AlbumMetadataChanged);
				}
				this._albumDetails = value;
				if (this._albumDetails != null)
				{
					this._albumDetails.AlbumMetadataChanged += new DetailsAlbum.AlbumMetadataChangedEventHandler(this.albumDetails_AlbumMetadataChanged);
				}
			}
		}
		[Browsable(false)]
		public DetailsMode Mode
		{
			get
			{
				return this._mode;
			}
			set
			{
				this._mode = value;
				switch (this._mode)
				{
				case DetailsMode.None:
					this.CaptionText = "";
					this.set_Visible(false);
					break;
				case DetailsMode.PhotoDetails:
					this.CaptionText = "Photo Description";
					this.albumDetails.set_Visible(false);
					this.photoDetails.set_Visible(true);
					this.photoActions.set_Visible(false);
					this.set_Visible(true);
					break;
				case DetailsMode.PhotoActions:
					this.SetActionValues(0, 0, 0, 0);
					this.CaptionText = "";
					this.albumDetails.set_Visible(false);
					this.photoDetails.set_Visible(false);
					this.photoActions.set_Visible(true);
					this.set_Visible(true);
					break;
				case DetailsMode.AlbumDetails:
					this.CaptionText = "Album Description";
					this.albumDetails.set_Visible(true);
					this.photoDetails.set_Visible(false);
					this.photoActions.set_Visible(false);
					this.set_Visible(true);
					break;
				}
			}
		}
		[Browsable(false)]
		public string AlbumName
		{
			get
			{
				return this.albumDetails.AlbumName;
			}
			set
			{
				this.albumDetails.AlbumName = value;
			}
		}
		private DetailsActions photoActions
		{
			get
			{
				return this._photoActions;
			}
			[MethodImpl(32)]
			set
			{
				if (this._photoActions != null)
				{
					this._photoActions.CommandButtonClicked -= new DetailsActions.CommandButtonClickedEventHandler(this.photoActions_ActionCommand);
					this._photoActions.Action -= new DetailsActions.ActionEventHandler(this.photoActions_Action);
					this._photoActions.CropModeChanged -= new DetailsActions.CropModeChangedEventHandler(this.photoActions_CropModeChanged);
				}
				this._photoActions = value;
				if (this._photoActions != null)
				{
					this._photoActions.CommandButtonClicked += new DetailsActions.CommandButtonClickedEventHandler(this.photoActions_ActionCommand);
					this._photoActions.Action += new DetailsActions.ActionEventHandler(this.photoActions_Action);
					this._photoActions.CropModeChanged += new DetailsActions.CropModeChangedEventHandler(this.photoActions_CropModeChanged);
				}
			}
		}
		public DetailsPane()
		{
			this.InitializeComponent();
		}
		private void InitializeComponent()
		{
			this.photoDetails = new DetailsPhotos();
			this.photoActions = new DetailsActions();
			this.albumDetails = new DetailsAlbum();
			this.SuspendLayout();
			this.photoDetails.set_BackColor(SystemColors.Control);
			this.photoDetails.set_BorderStyle(0);
			this.photoDetails.set_Dock(5);
			this.photoDetails.set_DrawMode(1);
			this.photoDetails.set_IntegralHeight(false);
			this.photoDetails.set_ItemHeight(158);
			Control arg_88_0 = this.photoDetails;
			Point location = new Point(1, 21);
			arg_88_0.set_Location(location);
			this.photoDetails.set_Name("photoDetails");
			Control arg_B5_0 = this.photoDetails;
			Size size = new Size(310, 346);
			arg_B5_0.set_Size(size);
			this.photoDetails.set_TabIndex(1);
			this.photoDetails.set_TabStop(false);
			this.photoActions.set_Dock(5);
			this.photoActions.DockPadding.set_All(2);
			Control arg_100_0 = this.photoActions;
			location = new Point(1, 21);
			arg_100_0.set_Location(location);
			this.photoActions.set_Name("photoActions");
			Control arg_12D_0 = this.photoActions;
			size = new Size(310, 346);
			arg_12D_0.set_Size(size);
			this.photoActions.set_TabIndex(1);
			this.albumDetails.AlbumName = null;
			this.albumDetails.set_Dock(5);
			this.albumDetails.set_Enabled(false);
			Control arg_173_0 = this.albumDetails;
			location = new Point(1, 21);
			arg_173_0.set_Location(location);
			this.albumDetails.set_Name("albumDetails");
			Control arg_1A0_0 = this.albumDetails;
			size = new Size(310, 346);
			arg_1A0_0.set_Size(size);
			this.albumDetails.set_TabIndex(2);
			this.Controls.Add(this.albumDetails);
			this.Controls.Add(this.photoActions);
			this.Controls.Add(this.photoDetails);
			this.DockPadding.set_All(1);
			this.set_Name("DetailsPane");
			size = new Size(312, 368);
			this.set_Size(size);
			this.Controls.SetChildIndex(this.photoDetails, 0);
			this.Controls.SetChildIndex(this.photoActions, 0);
			this.Controls.SetChildIndex(this.albumDetails, 0);
			this.ResumeLayout(false);
		}
		public void SetActionValues(int contrast, int brightness, int gamma, int saturation)
		{
			this.photoActions.SetActionValues(contrast, brightness, gamma, saturation);
		}
		public void EnableCommandButtons(bool photoDirty)
		{
			this.photoActions.EnableCommandButtons(photoDirty);
		}
		public void CropDataChanged(Size orgSize, Size newSize, Rectangle cropBounds)
		{
			this.photoActions.CropDataChanged(orgSize, newSize, cropBounds);
		}
		public void SetPhotoList(Photo photo)
		{
			this.SetPhotoList(new Photo[]
			{
				photo
			});
		}
		public void SetPhotoList(Photo[] photos)
		{
			this.photoDetails.SetPhotos(photos);
		}
		public void UpdateMetadata(Photo photo)
		{
			this.photoDetails.Invalidate();
		}
		public void PublishAlbum(string albumName, bool publish)
		{
			this.albumDetails.PublishAlbum(albumName, publish);
		}
		public void Save()
		{
			if (this.Mode == DetailsMode.AlbumDetails)
			{
				this.albumDetails.Save();
				return;
			}
			if (this.Mode == DetailsMode.PhotoDetails)
			{
				this.photoDetails.Save();
				return;
			}
		}
		private void photoActions_CropModeChanged(object sender, CropModeChangedEventArgs e)
		{
			if (this.CropModeChangedEvent != null)
			{
				this.CropModeChangedEvent(this, e);
			}
		}
		private void photoActions_Action(object sender, ActionEventArgs e)
		{
			if (this.ActionEvent != null)
			{
				this.ActionEvent(this, e);
			}
		}
		private void photoActions_ActionCommand(object sender, CommandButtonClickedEventArgs e)
		{
			if (this.CommandButtonClickedEvent != null)
			{
				this.CommandButtonClickedEvent(this, e);
			}
		}
		private void photoDetails_PhotoMetadataChanged(object sender, PhotoMetadataChangedEventArgs e)
		{
			if (this.PhotoMetadataChangedEvent != null)
			{
				this.PhotoMetadataChangedEvent(this, e);
			}
		}
		private void albumDetails_AlbumMetadataChanged(object sender, AlbumMetadataChangedEventArgs e)
		{
			if (this.AlbumMetadataChangedEvent != null)
			{
				this.AlbumMetadataChangedEvent(this, e);
			}
		}
	}
}
